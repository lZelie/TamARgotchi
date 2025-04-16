using System.Collections;
using UnityEngine;

public class PoopClean : MonoBehaviour
{
    private PetController _petController;
    private ButtonSelector _buttonSelector;

    [SerializeField] private int coinValue = 1;
    [SerializeField] private RandomPitchPlayer poopSound; // assign in inspector

    [SerializeField] private float scaleTime = 0.3f;
    [SerializeField] private float destroyDelay = 0.5f;

    private void Start()
    {
        var petManager = GameObject.Find("PetManager");
        _petController = petManager.GetComponent<PetController>();

        var buttons = GameObject.Find("Buttons");
        _buttonSelector = buttons.GetComponent<ButtonSelector>();
    }

    public void CleanPoop()
    {
        if (_buttonSelector.SelectedButtonIndex != 1) return;

        CoinManager.Instance.AddCoins(coinValue);
        _petController.Clean();

        if (poopSound != null)
            poopSound.Play();

        StartCoroutine(ScaleDownThenDestroy());
    }

    private IEnumerator ScaleDownThenDestroy()
    {
        Vector3 originalScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < scaleTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / scaleTime;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            yield return null;
        }

        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
