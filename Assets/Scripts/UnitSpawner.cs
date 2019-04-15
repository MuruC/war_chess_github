using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitSpawner : MonoBehaviour
{
    public GameObject soldierUnit;
    int totalUnitNum;
    // Start is called before the first frame update
    void Start()
    {
        totalUnitNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(totalUnitNum);

    }

    public void instantiateUnit(int nType) {
        int nAlign = GameManager.Instance.getTurn();
        int posXIndex = (GameManager.Instance.width - 1)/2;
        int posYIndex = 0;
        if (nAlign == 0)
        {
            posYIndex = 0;
        }
        else {
            posYIndex = GameManager.Instance.height - 1;
        }
        Vector2 pos = GameManager.Instance.getTerrainPos(posXIndex,posYIndex);
        //GameObject pSoldierUnit = soldierUnit;
        GameObject pSoldierUnit = Instantiate(soldierUnit, pos, Quaternion.identity);
        //Instantiate(soldierUnit, pos, Quaternion.identity);
        pSoldierUnit.name = "nType_"+nType+ "nAlign_"+nAlign+"Index_"+totalUnitNum;
        pSoldierUnit.GetComponent<Unit>().setEntity(nType,nAlign);
        if (nAlign == 1)
        {
            pSoldierUnit.GetComponent<SpriteRenderer>().color = new Color(253, 0, 255);
        }
        else{
            pSoldierUnit.GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
        }
        if (GameManager.Instance.getTurn() == 0)
        {
            GameManager.Instance.setSoldierObjectDic1(pSoldierUnit.name, pSoldierUnit);
        }
        else {
            GameManager.Instance.setSoldierObjectDic2(pSoldierUnit.name, pSoldierUnit);
        }
        GameManager.Instance.setUnitPosXIndex(pSoldierUnit.name,posXIndex);
        GameManager.Instance.setUnitPosYIndex(pSoldierUnit.name, posYIndex);
        GameManager.Instance.setSoldierAlign(pSoldierUnit.name,nAlign);
        GameManager.Instance.setSoldierType(pSoldierUnit.name,nType);
        //Debug.Log("pSoldierUnit.name: "+ pSoldierUnit.name+ " pSoldierUnit posXIndex: "+ GameManager.Instance.getUnitPosXIndex(pSoldierUnit.name) + " pSoldierUnit posYIndex: " + GameManager.Instance.getUnitPosYIndex(pSoldierUnit.name));
        totalUnitNum++;
    }
}
