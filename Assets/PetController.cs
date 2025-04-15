using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PetController : MonoBehaviour
{
    public PetNeeds needs;
    private Animator animator;
    
    // Pet states
    private bool isPooping = false;
    private bool isSad = false;
    
    public UnityEvent onPoopCritical;
    public UnityEvent onSadCritical;
    public UnityEvent onKamikazeCritical;
    
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void UpdateNeeds()
    {
        // Increase poop over time
        needs.poopLevel += needs.poopIncreaseRate / 60f;
        
        // Increase sadness if poop is high
        if (needs.poopLevel > needs.poopCriticalThreshold)
        {
            needs.sadnessLevel += needs.sadnessIncreaseRate / 30f; // Faster increase
        }
        else
        {
            needs.sadnessLevel += needs.sadnessIncreaseRate / 60f; // Normal increase
        }
        
        // Decrease karma if needs are neglected
        if (needs.poopLevel > needs.poopCriticalThreshold || 
            needs.sadnessLevel > needs.sadnessCriticalThreshold)
        {
            needs.karmaLevel -= needs.karmaDecreaseRate / 60f;
        }
        
        // Clamp values
        needs.poopLevel = Mathf.Clamp(needs.poopLevel, 0f, 100f);
        needs.sadnessLevel = Mathf.Clamp(needs.sadnessLevel, 0f, 100f);
        needs.karmaLevel = Mathf.Clamp(needs.karmaLevel, 0f, 100f);
        
        // Update animations and visuals based on states
        UpdatePetState();
    }
    
    void UpdatePetState()
    {
        // Update poop state
        if (needs.poopLevel > needs.poopCriticalThreshold && !isPooping)
        {
            isPooping = true;
            animator.SetBool("IsPooping", true);
            
        }
        
        // Update sadness state
        if (needs.sadnessLevel > needs.sadnessCriticalThreshold && !isSad)
        {
            isSad = true;
            animator.SetBool("IsSad", true);
        }
        else if (needs.sadnessLevel < needs.sadnessCriticalThreshold && isSad)
        {
            isSad = false;
            animator.SetBool("IsSad", false);
        }
    }
    
    // Interaction methods called by touch gestures
    public void Clean()
    {
        // Remove poop objects
        // Reset poop level
        needs.poopLevel = 0f;
        isPooping = false;
        animator.SetBool("IsPooping", false);
        needs.karmaLevel += 5f; // Reward for cleaning
    }
    
    public void Play()
    {
        // Reduce sadness
        needs.sadnessLevel -= 20f;
        needs.karmaLevel += 3f;
        animator.SetTrigger("Play");
    }
    
    public void Feed()
    {
        // Reduce sadness but increase poop
        needs.sadnessLevel -= 10f;
        needs.poopLevel += 15f;
        needs.karmaLevel += 2f;
        animator.SetTrigger("Eat");
    }
}
