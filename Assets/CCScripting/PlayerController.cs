using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public UnityEvent OnWallJumpExecuted;
    public UnityEvent OnEmitSystem;

    private Rigidbody m_controller;

    private RaycastHit m_data;
    private Vector3 m_wallNormal;
    private Vector2 m_moveInput;
    private bool m_isMovePressed;
    private bool m_isJumpPressed;
    private bool m_isGrounded;
    private bool m_wasGroundedPrior;
    private bool m_againstWall;
    private int m_numWallJumps;
    private int m_numAirJumps;
    private float m_jumpForce;

    [SerializeField, Tooltip("What object is the viewing perspective of this character? (Probably the Camera).")]
    private Transform perspective;

    [SerializeField, Tooltip("What's our fastest movement speed we can reach with just movement inputs?")] 
    private float maxMoveSpeed;

    [SerializeField]
    private float maxUpwardsVelocity;

    [SerializeField, Tooltip("How fast do we reach the max movement speed?")] 
    private float accelerationRate;

    [SerializeField, Tooltip("How fast do we reduce our move speed?")]
    private float decelerationRate; // 25

    [SerializeField, Tooltip("What counts as a significant difference dot product?"), Range(-1f, 1f)]
    private float significantDifference;

    [SerializeField, 
        Tooltip("If new input is in a significantly different direction than our current velocity, " +
        "how hard should we boost it? This makes turning on a dime much smoother.")]
    private float dotScalar = 2f;

    [SerializeField, Tooltip("How height (roughly) should the player jump?")]
    private float jumpHeight = 5f;

    [SerializeField]
    private float wallBounceForce = 3f;

    [SerializeField]
    private float fallingGravityBoost = 6f;

    [SerializeField]
    private int numAirJumps = 1;

    [SerializeField]
    private float airJumpBoost = 2.5f;

    public AudioSource jumpAudio;

    private void Awake()
    {
        m_controller = GetComponent<Rigidbody>();
        m_jumpForce = Mathf.Sqrt(jumpHeight * 2f * 9.81f);
        m_isGrounded = true;
        m_numWallJumps = 0;
    }

    #region Hooks
    public void GatherMoveInput(InputAction.CallbackContext context)
    {
        m_moveInput = context.ReadValue<Vector2>(); // auto-normalized by the InputAction.asset

        if (context.performed)
        {
            m_isMovePressed = true;
        }

        if (context.canceled)
        {
            m_isMovePressed = false;
        }
    }

    public void GatherJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            m_isJumpPressed = true; // set to false once jump is read; technically not great design but whatever.
        }

        if (context.canceled)
        {
            m_isJumpPressed = false;
        }
    }
    #endregion

    private void FixedUpdate()
    {
        m_wasGroundedPrior = m_isGrounded;
        m_isGrounded = Physics.Raycast(transform.position, Vector3.down, out m_data, 1.1f);

        if (m_isGrounded)
        {
            m_numAirJumps = numAirJumps;
            m_numWallJumps = 0;
        }

        if (m_wasGroundedPrior != m_isGrounded)
            OnEmitSystem.Invoke();

        Vector3 relative_dir = GetRelativeDirection(new Vector3(m_moveInput.x, 0f, m_moveInput.y));

        Vector3 lateral_velo = m_controller.velocity;
        lateral_velo.y = 0f;
        Vector3 normalized_lateral_velo = lateral_velo.normalized;

        float dir_scalar = Vector3.Dot(normalized_lateral_velo, relative_dir); // how similar is this input to our current velocity?

        bool is_different = (dir_scalar < significantDifference) && !m_isGrounded; // is_different should be always false if we aren't on the ground.

        if (m_isMovePressed && (is_different || lateral_velo.sqrMagnitude < maxMoveSpeed * maxMoveSpeed))
            m_controller.AddForce(relative_dir * (is_different ? accelerationRate * dotScalar : accelerationRate));
        
        // fake drag
        m_controller.AddForce(normalized_lateral_velo * -decelerationRate);

        HandleJump();

        ApplyFakeGravity();
    }

    private void ApplyFakeGravity()
    {
        float y = m_controller.velocity.y;

        if (y < 0f || y > maxUpwardsVelocity)
        {
            m_controller.AddForce(Vector3.down * fallingGravityBoost);
        }
    }

    private Vector3 GetRelativeDirection(Vector3 fromDir)
    {
        // better transform that uses quat multiplication
        Quaternion perpective_quat = Quaternion.Euler(0f, perspective.eulerAngles.y, 0f);
        return perpective_quat * fromDir; // transforms our current movement vector so that it is relative to camera y rot
    }

    private void HandleJump()
    {
        bool has_air_jumps = m_numAirJumps > 0;

        if (m_isJumpPressed && (m_isGrounded || m_againstWall || has_air_jumps))
        {
            if (jumpAudio != null && jumpAudio.clip != null)
            {
                jumpAudio.Play();
            }

            m_isJumpPressed = false;

            bool do_boost = false;

            if (has_air_jumps && !m_isGrounded && !m_againstWall)
            {
                m_numAirJumps -= 1;
                do_boost = true;
            }

            else if (m_againstWall)
                OnWallJumpExecuted.Invoke();

            m_isGrounded = false;
            OnEmitSystem.Invoke();

            m_controller.AddForce(GetJumpForceVector(do_boost), ForceMode.Impulse);
        }
    }

    private Vector3 GetJumpForceVector(bool doBoost)
    {
        RemoveYVelo();

        Vector3 resultant = Vector3.up * m_jumpForce * (doBoost ? airJumpBoost : 1);

        if (m_againstWall)
        {
            m_numWallJumps += 1;
            resultant += m_wallNormal * wallBounceForce * (2 - Vector3.Dot(m_wallNormal, transform.forward));
        }

        return resultant;
    }

    private void RemoveYVelo()
    {
        Vector3 velo = m_controller.velocity;
        velo.y = 0f;
        m_controller.velocity = velo;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // polish: Make the walljump hitbox bigger than the player so you dont have to be flush with the wall to bounce

        if (collision.gameObject.CompareTag("Wall") && Vector3.Dot(collision.GetContact(0).normal, Vector3.up) < 0.5f)
        {
            m_againstWall = true;
            m_wallNormal = collision.GetContact(0).normal;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_againstWall = false;
    }
}
