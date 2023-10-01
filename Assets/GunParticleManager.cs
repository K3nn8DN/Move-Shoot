using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParticleManager : MonoBehaviour
{
    private ParticleSystem m_system;
    private ParticleSystem.EmissionModule m_emmod;
    private ParticleSystem.ColorOverLifetimeModule m_cmod;
    private ParticleSystem.RotationOverLifetimeModule m_rmod;

    [SerializeField] private int particlesPerCharge = 5;
    [SerializeField] private List<ParticleSystem.MinMaxGradient> gradientsPerCharge;
    [SerializeField] private ParticleSystem.MinMaxCurve yRotRange;
    [SerializeField] private float yRotScalar = 0.5f;

    private void Awake()
    {
        m_system = GetComponent<ParticleSystem>();
        m_emmod = m_system.emission;
        m_cmod = m_system.colorOverLifetime;
        m_rmod = m_system.rotationOverLifetime;
    }

    public void HandleSystem(int gun_charges)
    {
        if (gun_charges == 0)
        {
            m_system.Stop();

            return;
        }

        if (!m_system.isPlaying)
            m_system.Play();

        m_emmod.rateOverTime = gun_charges * particlesPerCharge;
        m_cmod.color = gradientsPerCharge[gun_charges <= gradientsPerCharge.Count && gun_charges > 0 ? gun_charges - 1 : gradientsPerCharge.Count - 1];
        m_rmod.y = gun_charges * Random.Range(yRotRange.constantMin, yRotRange.constantMax) * yRotScalar;
    }
}
