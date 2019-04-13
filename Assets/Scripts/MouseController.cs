using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
    Unit selectedUnit;
    public GameObject selectedUnitObject;
    bool clickUnit = false;
    GameObject ourHitObject;
    string selectedUnitName;

    SoldierActions soldierActionScript;
    public GameObject soldierActionController;

    // Start is called before the first frame update
    void Start()
    {
        selectedUnitObject = new GameObject();
        selectedUnitName = " ";
        soldierActionScript = soldierActionController.GetComponent<SoldierActions>();
    }

    public bool CheckIfHasSoldierInGrid()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit != null && Input.GetMouseButton(0))
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider != null)
                {
                    GameObject pObj = hit[i].collider.transform.gameObject;
                    if(pObj.GetComponent<Unit>() != null)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit != null && Input.GetMouseButton(0)) {
            // GameObject ourHitObject = new GameObject();
            clickUnit = false;
            for (int i = 0; i < hit.Length; i++) {
                if (hit[i].collider != null)
                {
                    //GameObject ourHitObject = hit[i].collider.transform.gameObject;

                    ourHitObject = hit[i].collider.transform.gameObject;
                    //and do what you want
                    if (ourHitObject.GetComponent<Unit>() != null)
                    {
                        if (ourHitObject.GetComponent<Unit>().unitAlign == GameManager.Instance.getTurn()) {
                            mouseClickUnit(ourHitObject);
                            clickUnit = true;
                        }
                    }
                    else if (ourHitObject.GetComponent<TileScript>() != null)
                    {
                        mouseClickHex(ourHitObject);
                        
                    }
                }
            }
            //当点击的格子为非士兵单位所在格子时
            if (clickUnit == false && selectedUnit != null)
            {
                //点击非绿色格子，所有格子的复原
                if (selectedUnit.greenTiles.Contains(ourHitObject) == false) {
                    GameManager.Instance.restoreAllHexColor();
                    selectedUnit.greenTiles.Clear();
                }
                selectedUnit.thisUnitHasBeenClicked = false;
            }
        }

    }

    void mouseClickHex(GameObject pTile) {
        if (Input.GetMouseButton(0))
        {
            pTile.GetComponent<SpriteRenderer>().color = Color.red;
            if (selectedUnit != null && clickUnit == false && !CheckIfHasSoldierInGrid() && soldierActionScript.actionModeName != "Attack")
            {
                selectedUnit.destination = pTile.transform.position;
                int x = GameManager.Instance.getTileIndexXDic(pTile.name);
                GameManager.Instance.setUnitPosXIndex(selectedUnitObject.name, x);
                int y = GameManager.Instance.getTileIndexYDic(pTile.name);
                GameManager.Instance.setUnitPosYIndex(selectedUnitObject.name, y);
                Debug.Log("Destination: "+"xIndex: " + x + " yIndex: " + y);
            }
        }

    }

    void mouseClickUnit(GameObject pUnit)
    {
        if (Input.GetMouseButton(0))
        {
            UIManager.Instance.soldierActionPanel.SetActive(true);
            pUnit.GetComponent<SpriteRenderer>().color = Color.blue;
            selectedUnit = pUnit.GetComponent<Unit>();
            selectedUnit.thisUnitHasBeenClicked = true;
            selectedUnitObject = pUnit;
            string unitName = pUnit.name;
            //*
            if (unitName != selectedUnitName) {
                Debug.Log("unitName != selectedUnitName"+" unitName="+unitName+" selectedUnitName: "+ selectedUnitName);
                if (GameObject.Find(selectedUnitName) != null) {
                    for (int i = 0; i < GameObject.Find(selectedUnitName).GetComponent<Unit>().greenTiles.Count; i++)
                    {
                        GameObject.Find(selectedUnitName).GetComponent<Unit>().greenTiles[i].GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    GameObject.Find(selectedUnitName).GetComponent<Unit>().greenTiles.Clear();
                    GameObject.Find(selectedUnitName).GetComponent<Unit>().thisUnitHasBeenClicked = false;
                    if (GameObject.Find(selectedUnitName).GetComponent<Unit>().unitAlign == 0)
                    {
                        GameObject.Find(selectedUnitName).GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
                    }
                    else {
                        GameObject.Find(selectedUnitName).GetComponent<SpriteRenderer>().color = new Color(253, 0, 255);
                    }
                   
                }
                selectedUnitName = unitName;
            }
            //*/
            showUnitInformation(true);
            // change tiles around the unit 改变周围六边形的颜色
            changeHexColor();
            //testIndex();

        }


    }

    void testIndex() {
        int a = GameManager.Instance.getUnitPosXIndex(selectedUnitObject.name);
        int b = GameManager.Instance.getUnitPosYIndex(selectedUnitObject.name);
        Debug.Log("selectedUnit x index: " + a + " selectedUnit y index: " + b);
    }

    //改变周围六边形颜色
    public void changeHexColor() {
        int a = GameManager.Instance.getUnitPosXIndex(selectedUnitObject.name);
        int b = GameManager.Instance.getUnitPosYIndex(selectedUnitObject.name);
        Debug.Log("selectedUnit x index: " + a + " selectedUnit y index: " + b);

        //当y为奇数列，边上各缺一个

        if (b % 2 == 1)
        {

            if (a == 0) {
                if (b == 0)
                {
                    /*
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + b.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + a.ToString() + "yIndex_" + (b + 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + (b + 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    */
                    checkTerrain(a + 1,b);
                    checkTerrain(a, b + 1);
                    checkTerrain(a + 1, b + 1);
                }
                if (b > 0 && b < GameManager.Instance.height-1)
                {
                    Debug.Log("b > 0 && b < GameManager.Instance.height-1");
                    /*
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + b.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + a.ToString() + "yIndex_" + (b + 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + (b + 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + (b - 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + a.ToString() + "yIndex_" + (b - 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    */
                    checkTerrain(a + 1, b);
                    checkTerrain(a, b + 1);
                    checkTerrain(a + 1, b + 1);
                    checkTerrain(a + 1, b - 1);
                    checkTerrain(a, b - 1);
                }
                if (b == GameManager.Instance.height-1)
                {
                    /*
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + b.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + (b - 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + a.ToString() + "yIndex_" + (b - 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    */
                    checkTerrain(a + 1, b);
                    checkTerrain(a + 1, b - 1);
                    checkTerrain(a, b - 1);
                }

            }
            if (a >= 1 && a < GameManager.Instance.width - 1) {
                if (b == 0) {
                    /*
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + b.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + a.ToString() + "yIndex_" + (b + 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + (b + 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a - 1).ToString() + "yIndex_" + b.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    */
                    checkTerrain(a + 1, b);
                    checkTerrain(a, b + 1);
                    checkTerrain(a + 1, b + 1);
                    checkTerrain(a - 1, b);
                }
                if (b > 0 && b < GameManager.Instance.height - 1) {
                    /*
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + b.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + a.ToString() + "yIndex_" + (b + 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + (b + 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a - 1).ToString() + "yIndex_" + b.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + (b - 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + a.ToString() + "yIndex_" + (b - 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    */
                    checkTerrain(a + 1, b);
                    checkTerrain(a, b + 1);
                    checkTerrain(a + 1, b + 1);
                    checkTerrain(a + 1, b - 1);
                    checkTerrain(a, b - 1);
                    checkTerrain(a - 1, b);
                }
                if (b == GameManager.Instance.height-1) {
                    /*
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + b.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a + 1).ToString() + "yIndex_" + (b - 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + a.ToString() + "yIndex_" + (b - 1).ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    GameManager.Instance.getTileObjectByIndex("xIndex_" + (a - 1).ToString() + "yIndex_" + b.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
                    */
                    checkTerrain(a + 1, b);
                    checkTerrain(a + 1, b - 1);
                    checkTerrain(a, b - 1);
                    checkTerrain(a - 1, b);
                }
            }
            if (a == GameManager.Instance.width - 1) {
                if (b == 0)
                {
                    checkTerrain(a, b + 1);
                    checkTerrain(a - 1, b);
                }
                if (b > 0 && b < GameManager.Instance.height-1)
                {
                    checkTerrain(a, b + 1);
                    checkTerrain(a, b - 1);
                    checkTerrain(a - 1, b);
                }
                if (b == GameManager.Instance.height-1)
                {
                    checkTerrain(a, b - 1);
                    checkTerrain(a - 1, b);
                }
            }

        }

        //当y为偶数列
        else {
            if (a == 0)
            {
                if (b == 0)
                {
                    checkTerrain(a,b+1);
                    checkTerrain(a+1, b);
                }
                if (b > 0 && b < GameManager.Instance.height-1)
                {
                    checkTerrain(a, b + 1);
                    checkTerrain(a + 1, b);
                    checkTerrain(a,b-1);
                }
                if (b == GameManager.Instance.height-1)
                {
                    checkTerrain(a + 1, b);
                    checkTerrain(a, b - 1);
                }
            }
            if (a >= 1 && a < GameManager.Instance.width - 1)
            {
                if (b == 0)
                {
                    checkTerrain(a, b + 1);
                    checkTerrain(a + 1, b);
                    checkTerrain(a-1, b + 1);
                    checkTerrain(a-1, b);
                }
                if (b > 0 && b < GameManager.Instance.height-1)
                {
                    checkTerrain(a, b + 1);
                    checkTerrain(a + 1, b);
                    checkTerrain(a - 1, b + 1);
                    checkTerrain(a - 1, b);
                    checkTerrain(a - 1, b - 1);
                    checkTerrain(a, b - 1);
                }
                if (b == GameManager.Instance.height-1)
                {
                    checkTerrain(a + 1, b);
                    checkTerrain(a - 1, b);
                    checkTerrain(a - 1, b - 1);
                    checkTerrain(a, b - 1);
                }
            }
            if (a == GameManager.Instance.width - 1)
            {
                if (b == 0)
                {
                    checkTerrain(a-1,b);
                    checkTerrain(a-1,b+1);
                    checkTerrain(a,b+1);
                }
                if (b > 0 && b < GameManager.Instance.height-1)
                {
                    checkTerrain(a - 1, b);
                    checkTerrain(a - 1, b + 1);
                    checkTerrain(a - 1, b-1);
                    checkTerrain(a, b + 1);
                    checkTerrain(a, b - 1);
                }
                if (b == GameManager.Instance.height-1)
                {
                    checkTerrain(a - 1, b);
                    checkTerrain(a - 1, b - 1);
                    checkTerrain(a,b-1);
                }
            }
        }
    }
//特殊兵种无法进入特殊地形
    void checkTerrain(int indexX,int IndexY)
    {
        selectedUnit.greenTiles.Add(GameManager.Instance.getTileObjectByIndex("xIndex_" + indexX.ToString() + "yIndex_" + IndexY.ToString()));
        GameManager.Instance.getTileObjectByIndex("xIndex_" + indexX.ToString() + "yIndex_" + IndexY.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
    }


    //显示士兵信息
    public  void showUnitInformation(bool bShowInformation) {
        if (bShowInformation == true)
        {
            SoldierType.Soldiers thisUnit = new SoldierType.Soldiers(GameManager.Instance.getSoldierTypeIndex(selectedUnitObject.name), GameManager.Instance.getSoldierAlign(selectedUnitObject.name));
            int maxHP = thisUnit.getMaxHealth();
            UIManager.Instance.maxHP.GetComponent<Text>().text = maxHP.ToString();
            int attack = thisUnit.getBasicAttack();
            UIManager.Instance.attackValue.GetComponent<Text>().text = attack.ToString();
            int maxStep = thisUnit.getMaxMoveStep();
            UIManager.Instance.maxMoveStep.GetComponent<Text>().text = maxStep.ToString();
            UIManager.Instance.soldierTypeName.GetComponent<Text>().text = thisUnit.returnTypeName();
            int currentHP = thisUnit.getCurrentHP();
            UIManager.Instance.currentHP.GetComponent<Text>().text = currentHP.ToString();
            int currentMoveStep = thisUnit.getCurrentMoveStep();
            UIManager.Instance.currentMoveStep.GetComponent<Text>().text = currentMoveStep.ToString();
            UIManager.Instance.soldierTypeImage.sprite = UIManager.Instance.unitSprites[thisUnit.returnSpriteIndex()];
        }
        else {
            UIManager.Instance.maxHP.GetComponent<Text>().text = null;
            UIManager.Instance.attackValue.GetComponent<Text>().text = null;
            UIManager.Instance.maxMoveStep.GetComponent<Text>().text = null;
            UIManager.Instance.soldierTypeName.GetComponent<Text>().text = null;
            UIManager.Instance.soldierTypeImage.sprite = UIManager.Instance.noSprite;
            UIManager.Instance.currentMoveStep.GetComponent<Text>().text = null;
            UIManager.Instance.currentHP.GetComponent<Text>().text = null;
        }
    }


}
