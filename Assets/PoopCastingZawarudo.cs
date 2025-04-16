using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopCastingZawarudo : MonoBehaviour
{
    Transform pikachu;
    bool ceScriptEnabled = false;
    [SerializeField] private float seuil = 5;

    public void setTransform(Transform t, float tresh){
        pikachu = t;
        seuil = tresh;
        ceScriptEnabled = true;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ceScriptEnabled) {
            if(Vector3.Distance(transform.position, pikachu.position) > seuil){
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }
    }
}
