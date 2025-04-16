using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikAnimAtionController : MonoBehaviour
{
    [SerializeField] ParticleSystem ps_aura;
    [SerializeField] ParticleSystem ps_sad;
    
    // Start is called before the first frame update
    public void launchGhost()
    {
        ps_sad.Play();
    }

    public void launchAura(){
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
