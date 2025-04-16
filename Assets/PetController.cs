using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PetController : MonoBehaviour
{
    public PetNeeds needs;
    private CharacterAnimatorController animatorController;
    private PikAnimAtionController animAtionController;
    //private ParticleController particleController; //doublons du truc au dessus
    private PrefabSpawner CacaSpawner;
    private AudioSource bruitCaca;
    private PrefabSpawner lightningSpawner;

    [Tooltip("multiplie every increase by a random number in 1: ...")]
    public float randomIncreaseFactor = 3.0f;
    
    // Pet states
    private int poopCount = 0;
    private bool isSad = false;
    private bool isDancing = false;
    
    public UnityEvent onPoopCritical;
    public UnityEvent onSadCritical;
    public UnityEvent onKamikazeCritical;
    
    // progress bar
    private const int PGB_MAX_VAL = 9;
    [SerializeField] private ProgressBarManager pgb_caca;
    [SerializeField] private ProgressBarManager pgb_happy;
    [SerializeField] private ProgressBarManager pgb_karma;

    //used only for debug
    private float lastTime = 0.0f;

    //hardréférence codé psk flemne de faire autrement
    [SerializeField] private EventScheduler scheduler;
    [SerializeField] private ButtonSelector buttonSelector;


    void Start()
    {
        bruitCaca = GetComponent<AudioSource>();
    }
    public void commencerLeGameplay(GameObject pikachu){
        animatorController = pikachu.GetComponent<CharacterAnimatorController>();
        animAtionController = pikachu.GetComponent<PikAnimAtionController>();
        //particleController = pikachu.GetComponent<ParticleController>();
        CacaSpawner = pikachu.GetComponent<PrefabSpawner>();
        lightningSpawner = pikachu.transform.Find("thor").GetComponent<PrefabSpawner>();
        scheduler.cestParti();
    }

    public void debugloginfo(){
        Debug.Log($"poop : {needs.poopLevel} /  sadness : {needs.sadnessLevel} / karma {needs.karmaLevel}");
    }

    public void majLesProgressBar(){
        pgb_caca.SetValue((int)(needs.poopLevel / (100/PGB_MAX_VAL)));
        pgb_happy.SetValue((int)(needs.sadnessLevel / (100/PGB_MAX_VAL)));
        pgb_karma.SetValue((int)(needs.karmaLevel / (100/PGB_MAX_VAL)));
    }
    
    public void UpdateNeeds()
    {
        float sinceLast =  Time.time - lastTime;
        lastTime =  Time.time;
        Debug.Log($"since last : {sinceLast} /  total : {lastTime}");
        debugloginfo();


        // Increase poop over time
        needs.poopLevel += needs.poopIncreaseRate * (1+Random.value*(randomIncreaseFactor-1));
        
        // Increase sadness if poop is high
        if (needs.poopLevel > needs.poopCriticalThreshold)
        {
            needs.sadnessLevel += needs.sadnessIncreaseRate * (1+Random.value*(randomIncreaseFactor-1));
        }
        else
        {
            needs.sadnessLevel += needs.sadnessIncreaseRate * (1+Random.value*(randomIncreaseFactor-1));
        }
        
        // Decrease karma if needs are neglected
        if (needs.poopLevel > needs.poopCriticalThreshold || 
            needs.sadnessLevel > needs.sadnessCriticalThreshold)
        {
            needs.karmaLevel -= needs.karmaDecreaseRate * (1+Random.value*(randomIncreaseFactor-1));
        }
        
        // Clamp values
        needs.poopLevel = Mathf.Clamp(needs.poopLevel, 0f, 100f);
        needs.sadnessLevel = Mathf.Clamp(needs.sadnessLevel, 0f, 100f);
        needs.karmaLevel = Mathf.Clamp(needs.karmaLevel, 0f, 100f);

        majLesProgressBar();
        
        // Update animations and visuals based on states
        UpdatePetState();
    }
    
    void UpdatePetState()
    {
        // Update poop state
        if (needs.poopLevel > needs.poopCriticalThreshold)
        {
            faireGrosCaca();            
        }
        
        // Update sadness state
        if (needs.sadnessLevel > needs.sadnessCriticalThreshold && !isSad)
        {
            pleurer();
        }
        else if (needs.sadnessLevel < needs.sadnessCriticalThreshold && isSad){
            isSad = false;
            //animator.SetBool("IsSad", false);
        }
        if (needs.karmaLevel < needs.karmaCriticalThreshold){
            seFaireFoudroyer();
            needs.karmaLevel += needs.karmaDecreaseRate * 3;
        }
    }

    private void faireGrosCaca(){
        Debug.Log("<color=red>g chier !</color>");

        poopCount += 1; //ne sert plus à rien

        //todo animation
        float chier_time = 1.0f;
        animatorController.delayedSetPooping(0);
        animatorController.delayedSetIdle(1.2f);
        Invoke("bruitChier", 1.0f);
        int cacaCount = Random.Range(1, 4);
        for(int numerocaca =0; numerocaca <= cacaCount; numerocaca++){
            float rddelay = Random.value / 4.0f;
            CacaSpawner.delayedSpawnedWithImmulse(chier_time+rddelay);
        }
        //animator.SetBool("IsPooping", true);
    }
    public void bruitChier(){
        bruitCaca.Play();
    }

    private void pleurer(){
        Debug.Log("<color=#00FF55>je pleure ouin ouin</color>");

        isSad = true;
        animAtionController.launchGhost();
        // particleController.PlayParticles();
        //animator.SetBool("IsSad", true);
    }
    private void seFaireFoudroyer(){
        Debug.Log("<color=yellow>Je me suis fait FOUDROYER</color>");
        lightningSpawner.SpawnWithImpulse();
    }

    public void ClickedOnPikachu(){
        switch(buttonSelector.SelectedButtonIndex){
            case 0: // karma
                Debug.Log("karma");
                KarmaGrinding();
                //todo faire qqch
                break;
            case 1: // PQ - ne rien faire
                break;
            case 2: //coeur : dance
                Debug.Log("dancing");
                Play();
                break;
            default:
                Debug.Log("rien à faire");
                break;
        }
    }
    

    // Interaction methods called by touch gestures
    [ContextMenu("Call Clean")]
    public void Clean()
    {
        // Remove poop objects
        // Reset poop level
        needs.poopLevel = 0f;
        poopCount = -1;
        //animator.SetBool("IsPooping", false);
        needs.karmaLevel += 5f; // Reward for cleaning
        majLesProgressBar();
    }
    
    [ContextMenu("Call Play")]
    public void Play()
    {
        if(!isDancing){
            // Reduce sadness
            needs.sadnessLevel -= 40f;
            needs.karmaLevel += 3f;
            isDancing = true;
            animatorController.delayedSetDance(0);
            majLesProgressBar();
            Invoke("stopDancing", 6.5f);
        } else{
            Debug.Log("<color=purple>Je m'ennuie et je ne vuex plus dancer maintenant ...</color>");
        }
    }

    void KarmaGrinding(){
        animAtionController.launchAura();
        animAtionController.delayedStopAura(4.0f);
        needs.karmaLevel += 10;
    }

    void stopDancing(){
        isDancing = false;
        Debug.Log("<color=purple>Je suis à nouveau près à danser !!! masterclasse.</color>");
        animatorController.delayedSetIdle(0);
    }
    
    [ContextMenu("Call Feed")]
    public void Feed()
    {
        // Reduce sadness but increase poop
        needs.sadnessLevel -= 10f;
        needs.poopLevel += 15f;
        needs.karmaLevel += 2f;
        //animator.SetTrigger("Eat");

        majLesProgressBar();
    }
}
