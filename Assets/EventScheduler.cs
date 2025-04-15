using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventScheduler : MonoBehaviour{
    public UnityEvent processEvent; // Callback à appeler
    private float lastTime =0;
    
    [Tooltip("delay en seconde avant le call du premier schedule pour offset le temps de start de l'appli")]
    [SerializeField] private float delayBeforeInit = 3.0f;

    [Tooltip("durée min avant un autre event")]
    [SerializeField] private float minDelay = 3.0f;
    [Tooltip("durée max avant un autre event")]
    [SerializeField] private float maxDelay = 5.0f;
    private void Start()
    {
        // Lancer le premier événement après un délai initial
        Invoke("ScheduleNextEvent", delayBeforeInit);
    }

    private void ScheduleNextEvent()
    {
        // Générer un intervalle selon une loi exponentielle
        //float nextInterval = -Mathf.Log(1f - UnityEngine.Random.value) * meanInterval;
        //changement de plan, loi uniforme
        float nextInterval = minDelay + Random.value * (maxDelay-minDelay);

        // Planifier le prochain événement
        Invoke("TriggerEvent", nextInterval);
    }

    private void TriggerEvent()
    {
        // Appeler la callback
        processEvent?.Invoke();
        // Planifier le prochain événement
        ScheduleNextEvent();
    }

    public void doSomething(){
        //ackshually args are just some global variable
        float sinceLast =  Time.time - lastTime;
        lastTime =  Time.time;
        Debug.Log($"since last : {sinceLast} /  total : {lastTime}");

        int randomCase = Random.Range(0, 3); // Génère 0, 1 ou 2
        switch (randomCase)
        {
            case 0: //happyness 
                Debug.Log("Cas 1 : le bonheur");
                break;
            case 1: // caca
                Debug.Log("Cas 2 : le caca");
                break;
            case 2: // karma
                Debug.Log("Cas 3 : le karma");
                break;
        }
    }
}
