using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonSelector : MonoBehaviour
{
    [Header("Réglages")]
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color defaultColor = Color.white;

    [Header("Liste des boutons")]
    [SerializeField] private List<Button> buttons;

    private Button currentSelected;

    private void Start()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // Important pour capturer l'index correct dans la boucle
            buttons[i].onClick.AddListener(() => OnButtonClicked(buttons[index], index));
        }

        currentSelected = null;
    }

    private void OnButtonClicked(Button clicked, int index)
    {
        // Si on clique sur le bouton déjà sélectionné → on le désélectionne
        if (currentSelected == clicked)
        {
            clicked.image.color = defaultColor;
            currentSelected = null;
            return;
        }

        foreach (Button btn in buttons)
        {
            btn.image.color = defaultColor;
        }

        clicked.image.color = selectedColor;
        currentSelected = clicked;

        LaunchProgram(index);
    }

    private void LaunchProgram(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("KARMA");
                break;
            case 1:
                Debug.Log("PQ");
                break;
            case 2:
                Debug.Log("LOVE");
                break;
            default:
                Debug.Log("Aucune action définie pour ce bouton.");
                break;
        }
    }
}
