using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    public void SetText(string newText)
    {
        if (textMesh != null)
        {
            textMesh.text = newText;
        }
    }
}
