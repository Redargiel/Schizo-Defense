using UnityEngine;
using UnityEngine.UI;

public class TowerButtonHandler : MonoBehaviour
{
    public GameObject towerPrefab; // Prefab v�e, kterou tla��tko reprezentuje

    private UIInterface uiInterface; // Reference na existuj�c� skript pro pokl�d�n� v��

    void Start()
    {
        // Z�sk�n� reference na UIInterface skript p�ipojen� k stejn�mu GameObjectu
        uiInterface = GetComponentInParent<UIInterface>();

        // P�ipojen� funkce k ud�losti kliknut� na tla��tko
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(SelectTower);
        }
        else
        {
            Debug.LogError("Tla��tko nebylo nalezeno.");
        }
    }

    // Funkce volan� p�i kliknut� na tla��tko
    public void SelectTower()
    {
        // Zavol�n� funkce pro pokl�d�n� v�e z UIInterface skriptu a p�ed�n� prefabu v�e
        uiInterface.PlaceTower(towerPrefab);
    }
}


