using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PetNeeds
{
    [Range(0f, 100f)] public float poopLevel = 0f;
    [Range(0f, 100f)] public float sadnessLevel = 0f;
    [Range(0f, 100f)] public float karmaLevel = 0f;

    public float poopIncreaseRate = 5f;
    public float sadnessIncreaseRate = 3f;
    public float karmaDecreaseRate = 2f;

    public float poopCriticalThreshold = 80f;
    public float sadnessCriticalThreshold = 75f;
    public float karmaCriticalThreshold = 25f;
}
