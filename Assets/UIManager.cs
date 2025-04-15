using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ProgressBarManager poop;
    [SerializeField] private ProgressBarManager sad;
    [SerializeField] private ProgressBarManager stamina;

    private void Update()
    {
        // Juste pour test
        if (Input.GetKeyDown(KeyCode.H))
        {
            poop.SetValue(Random.Range(0, 9));
            Debug.Log("poop");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            sad.SetValue(Random.Range(0, 9));
            Debug.Log("sad");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            stamina.SetValue(Random.Range(0, 9));
            Debug.Log("stamina");

        }
    }
}
