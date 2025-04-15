using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PetNeeds
{
    [Range(0f, 100f)] public float poop = 0f;
    [Range(0f, 100f)] public float sad = 0f;
    [Range(0f, 100f)] public float karma = 0f;

    public float poopIncreaseRate = 5f;
    public float sadIncreaseRate = 3f;
    public float karmaDecreaseRate = 2f;

    public float poopCriticalThreshold = 80f;
    public float sadCriticalThreshold = 75f;
    public float karmaCriticalThreshold = 25f;
}
