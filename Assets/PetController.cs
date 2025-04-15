using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PetController : MonoBehaviour
{
    public PetNeeds needs;
    private Animator animator;

    [Tooltip("multiplie every increase by a random number in 1: ...")]
    public float randomIncreaseFactor = 3.0f;
    
    // Pet states
    private int poopCount = 0;
    private bool isSad = false;
    
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
    

    public void commencerLeGameplay(GameObject pikachu){
        animator = pikachu.GetComponent<Animator>();
        //rcup le controller
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
        Debug.Log("<color=red>g fait un gros caca !</color>");

        poopCount += 1;
        //animator.SetBool("IsPooping", true);

        //todo - animation + spawn poop + reserver 100 Mo de RAM
    }
    private void pleurer(){
        Debug.Log("<color=#00FF55>je pleure ouin ouin</color>");

        isSad = true;
        //animator.SetBool("IsSad", true);
        //todo - animation
    }
    private void seFaireFoudroyer(){
        Debug.Log("<color=yellow>Je me suis fait FOUDROYER</color>");
        //todo - animation
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
        // Reduce sadness
        needs.sadnessLevel -= 20f;
        needs.karmaLevel += 3f;
        //animator.SetTrigger("Play");

        majLesProgressBar();
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
