using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceMissle : MonoBehaviour
{
    private UIInterface uiInterface;

    void Start()
    {
        // Získáme odkaz na UIInterface skript
        uiInterface = FindObjectOfType<UIInterface>();

        // Pøidáme metodu OnButtonClick() na kliknutí na tlaèítko
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Zavoláme metodu SelectLaserTower v UIInterface
        uiInterface.SelectMissileTower();
    }
}
