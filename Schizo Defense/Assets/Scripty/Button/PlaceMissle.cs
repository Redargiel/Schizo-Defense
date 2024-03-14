using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceMissle : MonoBehaviour
{
    private UIInterface uiInterface;

    void Start()
    {
        // Z�sk�me odkaz na UIInterface skript
        uiInterface = FindObjectOfType<UIInterface>();

        // P�id�me metodu OnButtonClick() na kliknut� na tla��tko
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Zavol�me metodu SelectLaserTower v UIInterface
        uiInterface.SelectMissileTower();
    }
}
