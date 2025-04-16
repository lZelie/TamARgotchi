using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopClean : MonoBehaviour
{
    private PetController _petController;
    private ButtonSelector _buttonSelector;

    [SerializeField] private int coinValue = 1;

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
        Destroy(gameObject);
    }
}
