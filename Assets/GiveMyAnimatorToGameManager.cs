using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMyAnimatorToGameManager : MonoBehaviour
{
    private Animator animator;
    PetController managerScript;

    public RandomSoundPlayer rdsplayer;

    void Awake()
    {
        
        // Find GameManager at scene root
        GameObject petManager = GameObject.Find("PetManager");
        
        if (petManager != null)
        {
            // Get the GameManager component and call Init
            managerScript = petManager.GetComponent<PetController>();
            if (managerScript != null)
            {
                managerScript.commencerLeGameplay(gameObject);
            }
            else
            {
                Debug.LogWarning("wsh il manque le script");
            }
        }
        else
        {
            Debug.LogWarning("PetManager pas trouvé dans le scène");
        }
    }

    public void callClickOnMeInPetManager(){
        managerScript.ClickedOnPikachu();
    }
}
