using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class RandomMovementWithAnimation : MonoBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");

    [Header("Movement Settings")]
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 5.0f;
    public float minWaitTime = 1.0f;
    public float maxWaitTime = 5.0f;
    
    [Header("Range Settings")]
    public Vector3 centerPoint;
    public float maxRange = 5.0f;
    public bool useInitialPositionAsCenter = true;
    
    [Header("Animation")]
    public Animator animator;
    public string walkParameterName = "IsWalking";
    public string idleParameterName = "IsIdle";
    
    // Movement variables
    private Vector3 targetPosition;
    private float waitTimer;
    private bool isMoving = false;
    
    private ObjectSpawner objectSpawner;
    private Vector3 lastPosition;
    
    void Start()
    {
        // If using initial position as center point
        if (useInitialPositionAsCenter)
        {
            centerPoint = transform.position;
        }
        
        // Initialize with a random wait time
        waitTimer = Random.Range(minWaitTime, maxWaitTime);
        
        // Get the animator component if not assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
        // Start in idle state
        if (animator != null)
        {
            animator.SetTrigger(Idle);
        }
        
        objectSpawner = GameObject.Find("Object Spawner").GetComponent<ObjectSpawner>();
    }
    
    void Update()
    {
        centerPoint = objectSpawner.spawn;
        if (isMoving)
        {
            // Calculate direction to target
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0; // Keep rotation level with ground
            
            // Only rotate if we're not at the target yet
            if (direction.magnitude > 0.1f)
            {
                // Create rotation to face the target position
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                
                // Smoothly rotate towards the target
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, 
                    targetRotation, 
                    rotationSpeed * Time.deltaTime
                );
            }
            
            // Move towards target position
            transform.position = Vector3.MoveTowards(
                transform.position, 
                targetPosition, 
                moveSpeed * Time.deltaTime
            );
            
            // Check if we've reached the destination
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f || Vector3.Distance(transform.position, lastPosition) < 0.001f)
            {
                isMoving = false;
                waitTimer = Random.Range(minWaitTime, maxWaitTime);
                
                // Switch to idle animation
                if (animator != null)
                {
                    animator.SetTrigger(Idle);
                }
            }
            lastPosition = transform.position;
        }
        else
        {
            // Wait before moving again
            waitTimer -= Time.deltaTime;
            
            if (waitTimer <= 0)
            {
                SetNewRandomDestination();
                isMoving = true;
                
                // Switch to walk animation
                if (animator != null)
                {
                    animator.SetTrigger(Walk);
                }
            }
        }
        
        // If we somehow go out of range, get back in range
        if (Vector3.Distance(transform.position, centerPoint) > maxRange + 1.0f)
        {
            SetNewRandomDestination();
            isMoving = true;
            
            // Switch to walk animation
            if (animator != null)
            {
                animator.SetTrigger(Walk);
            }
        }
    }
    
    void SetNewRandomDestination()
    {
        // Generate a random point within the allowed range
        Vector3 randomDirection = Random.insideUnitSphere * maxRange;
        randomDirection.y = 0; // Keep at same height - remove this line for full 3D movement
        
        targetPosition = centerPoint + randomDirection;
    }
    
    // Method to manually set a new destination
    public void SetDestination(Vector3 newTarget)
    {
        // Make sure the target is within range
        if (Vector3.Distance(newTarget, centerPoint) > maxRange)
        {
            // If beyond range, clamp to the max range
            Vector3 direction = (newTarget - centerPoint).normalized;
            newTarget = centerPoint + (direction * maxRange);
        }
        
        targetPosition = newTarget;
        isMoving = true;
        
        // Switch to walk animation
        if (animator != null)
        {
            animator.SetTrigger(Walk);
        }
    }
    
    // Visualize the movement range in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(useInitialPositionAsCenter ? transform.position : centerPoint, maxRange);
    }
}