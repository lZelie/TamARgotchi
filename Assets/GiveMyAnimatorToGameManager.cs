using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveMyAnimatorToGameManager : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        
        // Find GameManager at scene root
        GameObject petManager = GameObject.Find("PetManager");
        
        if (petManager != null)
        {
            // Get the GameManager component and call Init
            PetController managerScript = petManager.GetComponent<PetController>();
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
}
