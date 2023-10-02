using UnityEngine;

public class GunAnimationScript : MonoBehaviour
{
    private Animator m_animator;

    private int m_basicHash;
    private int m_chargeHash;
    private int m_funnyHash;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();

        m_basicHash = Animator.StringToHash("BasicShoot");
        m_chargeHash = Animator.StringToHash("ChargeShoot");
        m_funnyHash = Animator.StringToHash("FunShoot");
    }

    public void PlayCharged()
    {
        m_animator.Play(Random.Range(0f, 1f) > 0.5f ? m_funnyHash : m_chargeHash, 0, 0f);
    }

    public void PlayRegular()
    {
        m_animator.Play(m_basicHash, 0, 0f);
    }
}
