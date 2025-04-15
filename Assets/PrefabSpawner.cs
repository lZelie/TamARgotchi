using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject prefab;
    public Transform spawnPoint;
    public float impulseForce = 10f;
    public Vector3 direction = Vector3.forward;

    [Header("Spawn Control")]
    public KeyCode spawnKey = KeyCode.F; // Press 'F' to spawn

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            SpawnWithImpulse();
        }
    }

    public void SpawnWithImpulse()
    {
        if (prefab == null || spawnPoint == null)
        {
            Debug.LogWarning("Missing prefab or spawn point.");
            return;
        }

        // Instantiate the prefab
        GameObject obj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        // Add Rigidbody if missing
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody>();
        }

        // Apply impulse force
        rb.AddForce(direction.normalized * impulseForce, ForceMode.Impulse);
    }
}
