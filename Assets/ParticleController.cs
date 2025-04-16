using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;
    
    // For cases where you want to find particle systems automatically
    private bool hasInitialized = false;
    
    private void Awake()
    {
        InitializeIfNeeded();
    }
    
    private void InitializeIfNeeded()
    {
        if (!hasInitialized)
        {
            // If no particle systems were assigned in the inspector,
            // try to find them on this game object and its children
            if (particleSystems == null || particleSystems.Length == 0)
            {
                particleSystems = GetComponentsInChildren<ParticleSystem>(true);
                Debug.Log($"Auto-found {particleSystems.Length} particle systems");
            }
            
            // Ensure they're all set to not play on awake
            foreach (var ps in particleSystems)
            {
                if (ps != null)
                {
                    var main = ps.main;
                    main.playOnAwake = false;
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
            
            hasInitialized = true;
        }
    }
    
    public void PlayParticles()
    {
        InitializeIfNeeded();
        
        foreach (var ps in particleSystems)
        {
            if (ps != null)
            {
                ps.Play(true);
            }
        }
    }
    
    public void StopParticles()
    {
        if (particleSystems == null) return;
        
        foreach (var ps in particleSystems)
        {
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
    
    public void StopAndClearParticles()
    {
        if (particleSystems == null) return;
        
        foreach (var ps in particleSystems)
        {
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }
}