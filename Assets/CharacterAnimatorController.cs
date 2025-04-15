using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class CharacterAnimatorController : MonoBehaviour
{
    public Animator animator;
    public InputActionAsset inputActions; // 👈 Drag your .inputactions asset here in the Inspector

    private InputAction idleAction;
    private InputAction walkAction;
    private InputAction jumpAction;
    private InputAction danceAction;
    private InputAction poopingAction;

    void OnEnable()
    {
        if (inputActions == null)
        {
            Debug.LogError("InputActionAsset is not assigned!");
            return;
        }

        // 🧭 Look up actions by map and name (case-sensitive)
        idleAction = inputActions.FindActionMap("Tamagochi").FindAction("Idle");
        walkAction = inputActions.FindActionMap("Tamagochi").FindAction("Walk");
        jumpAction = inputActions.FindActionMap("Tamagochi").FindAction("Jump");
        danceAction = inputActions.FindActionMap("Tamagochi").FindAction("Dance");
        poopingAction = inputActions.FindActionMap("Tamagochi").FindAction("Pooping");

        idleAction.performed += ctx => SetIdle();
        walkAction.performed += ctx => SetWalk();
        jumpAction.performed += ctx => SetJump();
        danceAction.performed += ctx => SetDance();
        poopingAction.performed += ctx => SetPooping();

        idleAction.Enable();
        walkAction.Enable();
        jumpAction.Enable();
        danceAction.Enable();
        poopingAction.Enable();
    }

    void OnDisable()
    {
        idleAction?.Disable();
        walkAction?.Disable();
        jumpAction?.Disable();
        danceAction?.Disable();
        poopingAction?.Disable();
    }

    void SetWalk()
    {
        SetTrigger("Walk", true);
    }

    void SetDance()
    {
        SetTrigger("Dance", true);
    }

    void SetIdle()
    {
        SetTrigger("Idle");
    }

    void SetJump()
    {
        SetTrigger("Jump", true);
    }
    void SetPooping()
    {
        SetTrigger("Pooping", true);
    }

    void SetTrigger(string triggerName, bool tryIdleBefore = false)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(triggerName))
        {
            if (tryIdleBefore && !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                SetTrigger("Idle");
            }

            animator.SetTrigger(triggerName);
        }
    }


    public void delayedSetJump(float delay){
        Invoke("SetJump", delay);
    }
    public void delayedSetPooping(float delay){
        Invoke("SetPooping", delay);
    }
    public void delayedSetIdle(float delay){
        Invoke("SetIdle", delay);
    }
}
