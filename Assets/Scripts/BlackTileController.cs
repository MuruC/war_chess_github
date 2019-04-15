using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackTileController : MonoBehaviour
{
    Unit selectedUnit;
    GameObject selectedUnitObject;
    public GameObject mouseController;
    MouseController mouseControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) {
            return;
        }
        GameManager.Instance.setPlayer1BlackTilesActive();
        GameManager.Instance.setPlayer2BlackTilesActive();

        mouseControllerScript = mouseController.GetComponent<MouseController>();

        if (mouseControllerScript.selectedUnitObject == null) { return; }

        selectedUnitObject = mouseControllerScript.selectedUnitObject;
        selectedUnit = mouseControllerScript.selectedUnitObject.GetComponent<Unit>();

        if (selectedUnit != null) {
            tilesAroundUnit();
        }
    }

    void tilesAroundUnit() {
        int a = GameManager.Instance.getUnitPosXIndex(selectedUnitObject.name);
        int b = GameManager.Instance.getUnitPosYIndex(selectedUnitObject.name);
        SoldierType.Soldiers mySoldier = selectedUnitObject.GetComponent<Unit>().m_pSoldier;

        if (mySoldier.getView() == 1) {
            if (b % 2 == 1)
            {
                removeTiles(a + 1, b);
                removeTiles(a, b + 1);
                removeTiles(a + 1, b + 1);
                removeTiles(a + 1, b - 1);
                removeTiles(a, b - 1);
                removeTiles(a, b);
                removeTiles(a - 1, b);
            }
            else {
                removeTiles(a, b + 1);
                removeTiles(a + 1, b);
                removeTiles(a - 1, b + 1);
                removeTiles(a - 1, b);
                removeTiles(a - 1, b - 1);
                removeTiles(a, b - 1);
                removeTiles(a, b);
            }
        } else if (mySoldier.getView() == 2) {
            if (b % 2 == 1)
            {
                removeTiles(a + 1, b);
                removeTiles(a, b + 1);
                removeTiles(a + 1, b + 1);
                removeTiles(a + 1, b - 1);
                removeTiles(a, b - 1);
                removeTiles(a, b);
                removeTiles(a - 1, b);
                removeTiles(a + 2, b);
                removeTiles(a + 2, b - 1);
                removeTiles(a + 1, b - 2);
                removeTiles(a, b - 2);
                removeTiles(a - 1, b - 2);
                removeTiles(a - 1, b - 1);
                removeTiles(a - 2, b);
                removeTiles(a - 1, b + 1);
                removeTiles(a - 1, b + 2);
                removeTiles(a, b + 2);
                removeTiles(a + 1, b + 2);
                removeTiles(a + 2, b + 1);

            }
            else
            {
                removeTiles(a, b + 1);
                removeTiles(a + 1, b);
                removeTiles(a - 1, b + 1);
                removeTiles(a - 1, b);
                removeTiles(a - 1, b - 1);
                removeTiles(a, b - 1);
                removeTiles(a, b);
                removeTiles(a + 2, b);
                removeTiles(a + 1, b - 1);
                removeTiles(a + 1, b - 2);
                removeTiles(a, b - 2);
                removeTiles(a - 1, b - 2);
                removeTiles(a - 2, b - 1);
                removeTiles(a - 2, b);
                removeTiles(a - 2, b + 1);
                removeTiles(a - 1, b + 2);
                removeTiles(a, b + 2);
                removeTiles(a + 1, b + 2);
                removeTiles(a + 1, b + 1);
            }
        }
    }

    void removeTiles(int x, int y) {
        if (GameManager.Instance.getTurn() == 0 && selectedUnit.unitAlign == 0) {
            GameManager.Instance.removePlayer1BlackTile(x,y);
        }
        if (GameManager.Instance.getTurn() == 1 && selectedUnit.unitAlign == 1) {
            GameManager.Instance.removePlayer2BlackTile(x, y);
        }
    }
}
