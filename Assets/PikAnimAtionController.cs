using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikAnimAtionController : MonoBehaviour
{
    [SerializeField] ParticleSystem ps_aura;
    [SerializeField] ParticleSystem ps_sad;
    [SerializeField] AudioSource as_ghost;
    [SerializeField] AudioSource as_aura;
    
    // Start is called before the first frame update
    public void launchGhost()
    {
        ps_sad.time = 0;
        as_ghost.Play();
        ps_sad.Play();
    }
    public void delayedStopGhost(float delay){
        Invoke("stopGhost", delay);
    }
    public void stopGhost(){
        ps_sad.Stop();
    }

    public void launchAura(){
        as_aura.Play();
        ps_aura.Play();
    }

    public void delayedStopAura(float delay){
        Invoke("stopAura", delay);
    }
    public void stopAura(){
        ps_aura.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
