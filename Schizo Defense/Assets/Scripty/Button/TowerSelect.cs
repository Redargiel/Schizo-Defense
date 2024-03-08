using UnityEngine;
using UnityEngine.UI;

public class TowerButtonHandler : MonoBehaviour
{
    public GameObject towerPrefab; // Prefab vìže, kterou tlaèítko reprezentuje

    private UIInterface uiInterface; // Reference na existující skript pro pokládání vìží

    void Start()
    {
        // Získání reference na UIInterface skript pøipojený k stejnému GameObjectu
        uiInterface = GetComponentInParent<UIInterface>();

        // Pøipojení funkce k události kliknutí na tlaèítko
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(SelectTower);
        }
        else
        {
            Debug.LogError("Tlaèítko nebylo nalezeno.");
        }
    }

    // Funkce volaná pøi kliknutí na tlaèítko
    public void SelectTower()
    {
        // Zavolání funkce pro pokládání vìže z UIInterface skriptu a pøedání prefabu vìže
        uiInterface.PlaceTower(towerPrefab);
    }
}


