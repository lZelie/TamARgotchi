using UnityEngine;

public class ProgressBarManager : MonoBehaviour
{
    [Header("Réglages de la progression")]
    [SerializeField] private int maxValue = 10;
    [SerializeField] private int currentValue = 5;

    [Header("Références")]
    [SerializeField] private Transform container; // Container parent des éléments
    [SerializeField] private GameObject elementPrefab; // Prefab de l'élément à instancier

    private void Start()
    {
        UpdateBar();
    }

    // Met à jour la barre en fonction de currentValue
    public void UpdateBar()
    {
        // Supprime les anciens éléments
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        // Crée les nouveaux éléments selon currentValue
        for (int i = 0; i < currentValue && i < maxValue; i++)
        {
            Instantiate(elementPrefab, container);
        }
    }

    // Appelable via d'autres scripts pour modifier la progression
    public void SetValue(int value)
    {
        currentValue = Mathf.Clamp(value, 0, maxValue);
        UpdateBar();
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            UpdateBar();
        }
    }   


}