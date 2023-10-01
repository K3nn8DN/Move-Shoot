using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilHandler : MonoBehaviour
{
    [SerializeField] private float impactScalar = 5f;
    private Rigidbody m_rigidbody;
    private Transform m_perspective;

    private int m_gunType = 0; // polish, unused currently

    private int m_charges = 0;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_perspective = Camera.main.transform;
    }

    public void OnGunFired(int charges)
    {
        // if charges dropped to 0 (i.e. expended)
        if (m_charges != 0 && charges == 0)
        {
            Vector3 direction = Vector3.zero; // what direction is the recoil?

            if (m_gunType == 0)
                direction = -(m_perspective.forward + Vector3.up * 0.5f);

            // more gun types would go here; change the if to a switch

            // flatten velo vector so you get instant vertical feedback from recoil
            Vector3 velo = m_rigidbody.velocity;
            velo.y = 0f;
            m_rigidbody.velocity = velo;
            //

            m_rigidbody.AddForce(m_charges * impactScalar * direction, ForceMode.Impulse);
        }

        m_charges = charges;
    }
}
