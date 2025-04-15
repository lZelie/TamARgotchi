using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SpawnablePrefab
{
    public GameObject prefab;
    [Range(0f, 1f)] public float spawnChance; // e.g. 0.5 = 50%
}

public class PrefabSpawner : MonoBehaviour
{
    [Header("Prefab Settings")]
    public List<SpawnablePrefab> spawnables;
    public Transform spawnPoint;
    public float impulseForce = 10f;
    public Vector3 direction = Vector3.forward;

    [Header("Spawn Control")]
    public KeyCode spawnKey = KeyCode.F;

    [Header("Cone Settings")]
    public float coneAngle = 15f; // Max angle in degrees for the cone

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnWithImpulse();
        }
    }

    void SpawnWithImpulse()
    {
        if (spawnables.Count == 0 || spawnPoint == null)
        {
            Debug.LogWarning("Missing prefabs or spawn point.");
            return;
        }

        GameObject prefabToSpawn = GetRandomPrefab();
        if (prefabToSpawn == null)
        {
            Debug.LogWarning("No prefab selected based on chance.");
            return;
        }

        GameObject obj = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody>();
        }

        // Get the base direction from spawn point's orientation
        Vector3 baseDir = spawnPoint.TransformDirection(direction.normalized);

        // Randomly vary the direction within the cone
        Vector3 randomDirection = GetRandomDirectionInCone(baseDir, coneAngle);

        // Apply force with the tilted direction
        rb.AddForce(randomDirection * impulseForce, ForceMode.Impulse);
    }

    // Function to get a random direction within a cone
    Vector3 GetRandomDirectionInCone(Vector3 baseDir, float maxAngle)
    {
        // Get a random angle within the cone's range (-maxAngle to maxAngle)
        float angle = Random.Range(-maxAngle, maxAngle);

        // Get a random axis to rotate around (perpendicular to the base direction)
        Vector3 rotationAxis = Vector3.Cross(baseDir, Random.insideUnitSphere).normalized;

        // Rotate the direction by the random angle around the random axis
        Quaternion rotation = Quaternion.AngleAxis(angle, rotationAxis);

        // Apply the rotation to the base direction
        return rotation * baseDir;
    }

    GameObject GetRandomPrefab()
    {
        float total = 0f;
        foreach (var item in spawnables)
        {
            total += item.spawnChance;
        }

        float roll = Random.value * total;
        float cumulative = 0f;

        foreach (var item in spawnables)
        {
            cumulative += item.spawnChance;
            if (roll <= cumulative)
                return item.prefab;
        }

        return null; // fallback
    }
}
