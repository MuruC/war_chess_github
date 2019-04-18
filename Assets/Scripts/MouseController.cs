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
    PlayerAlign.Players player1;
    PlayerAlign.Players player2;
    PlayerAlign playerScript;

    // Start is called before the first frame update
    void Start()
    {
        selectedUnitObject = new GameObject();
        selectedUnitName = " ";
        soldierActionScript = soldierActionController.GetComponent<SoldierActions>();
        playerScript = GameObject.Find("Players").GetComponent<PlayerAlign>();
        player1 = playerScript.m_Player1;
        player2 = playerScript.m_Player2;
    }
    public void ReserStatuOnChangeTurn() {
        soldierActionScript.Reset();
        if (soldierActionScript.preEnemyObject)
        {
            if (GameManager.Instance.getTurn() == 0)
            {
                soldierActionScript.preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
            }
            else
            {
                soldierActionScript.preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(253, 0, 255);
            }
        }
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
        if (selectedUnitObject == null)
        {
            showUnitInformation(false);
        }
        else {
            showUnitInformation(true);
        }
    }

    void mouseClickHex(GameObject pTile) {
        if (Input.GetMouseButton(0))
        {
            //pTile.GetComponent<SpriteRenderer>().color = Color.red;
            if(selectedUnit == null)
            {
                return;
            }

            SoldierType.Soldiers thisSelectedUnitType = selectedUnit.m_pSoldier;
            if (clickUnit == false && !CheckIfHasSoldierInGrid() && soldierActionScript.actionModeName != "Attack"
               && thisSelectedUnitType.getCurrentMoveStep() > 0)
                
            {
               
                if (selectedUnit.greenTiles.Contains(pTile)) {
                    if (selectedUnitObject == null) { return; }
                    selectedUnit.destination = pTile.transform.position;
                    int x = GameManager.Instance.getTileIndexXDic(pTile.name);
                    GameManager.Instance.setUnitPosXIndex(selectedUnitObject.name, x);
                    int y = GameManager.Instance.getTileIndexYDic(pTile.name);
                    GameManager.Instance.setUnitPosYIndex(selectedUnitObject.name, y);
                    selectedUnit.destinationXIndex = x;
                    selectedUnit.destinationYIndex = y;
                    selectedUnit.indexX = x;
                    selectedUnit.indexY = y;
                    Debug.Log("Destination: " + "xIndex: " + x + " yIndex: " + y);
                if (selectedUnit.entityType == 1 || selectedUnit.entityType == 2 || selectedUnit.entityType == 4
                    || selectedUnit.entityType == 6 || selectedUnit.entityType == 8) {
                    SoundEffectManager.Instance.playAudio(4);
                } else if (selectedUnit.entityType == 3)
                {
                    SoundEffectManager.Instance.playAudio(11);
                } else if (selectedUnit.entityType == 7 || selectedUnit.entityType == 5)
                {
                    SoundEffectManager.Instance.playAudio(7);
                }
                    //thisSelectedUnitType.setCurrentMoveStep(1);
                }
            }
        }

    }

    void mouseClickUnit(GameObject pUnit)
    {
        if (pUnit.GetComponent<Unit>().unitAlign != GameManager.Instance.getTurn()) {
            return;
        }
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
                    if (GameObject.Find(selectedUnitName).GetComponent<Unit>().unitAlign == 0 && GameManager.Instance.getTurn() == 0)
                    {
                       GameObject.Find(selectedUnitName).GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
                       // GameObject.Find(selectedUnitName).GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    else if(GameObject.Find(selectedUnitName).GetComponent<Unit>().unitAlign == 1 && GameManager.Instance.getTurn() == 1){
                        GameObject.Find(selectedUnitName).GetComponent<SpriteRenderer>().color = new Color(253, 0, 255);
                        //GameObject.Find(selectedUnitName).GetComponent<SpriteRenderer>().color = Color.white;
                    }

                }
                selectedUnitName = unitName;
            }
            //*/
            showUnitInformation(true);
            soldierActionScript.destroyAttackArrow();
            ReserStatuOnChangeTurn();
            resetBeenAttackedEnemyColor();
            // change tiles around the unit 改变周围六边形的颜色
            if (selectedUnit.m_pSoldier.getCurrentMoveStep() > 0) {
                changeHexColor();
            }

        }


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
        GameObject terrain = GameManager.Instance.getTileObjectByIndex("xIndex_" + indexX.ToString() + "yIndex_" + IndexY.ToString());
        //骑兵和坦克不能进入湖水地形
        if (terrain.GetComponent<TileScript>().spriteType == 1) {
            if (selectedUnit.entityType != 3 && selectedUnit.entityType != 7) {
                selectedUnit.greenTiles.Add(terrain);
                GameManager.Instance.getTileObjectByIndex("xIndex_" + indexX.ToString() + "yIndex_" + IndexY.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
        //骑兵坦克和火炮不能进入山地
        if (terrain.GetComponent<TileScript>().spriteType == 2)
        {
            if (selectedUnit.entityType != 3 && selectedUnit.entityType != 7 && selectedUnit.entityType != 5)
            {
                selectedUnit.greenTiles.Add(terrain);
                GameManager.Instance.getTileObjectByIndex("xIndex_" + indexX.ToString() + "yIndex_" + IndexY.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
            }
        }

        if (terrain.GetComponent<TileScript>().spriteType != 2 && terrain.GetComponent<TileScript>().spriteType != 1) {
            selectedUnit.greenTiles.Add(terrain);
            GameManager.Instance.getTileObjectByIndex("xIndex_" + indexX.ToString() + "yIndex_" + IndexY.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    //显示士兵信息
    public  void showUnitInformation(bool bShowInformation) {
        if (selectedUnit == null) {
            return;
        }

        if (bShowInformation == true)
        {
            SoldierType.Soldiers thisUnit = selectedUnit.m_pSoldier;
            int maxHP = thisUnit.getMaxHP();
            UIManager.Instance.maxHP.GetComponent<Text>().text = maxHP.ToString();
            int attack = thisUnit.getAttack();
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

    //使攻击状态下红色的人物返回原来的颜色
    void resetBeenAttackedEnemyColor() {
        if (soldierActionScript.preEnemyObject != null) {
            if (GameManager.Instance.getTurn() == 0)
            {
                soldierActionScript.preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(253, 0, 255);
            }
            else
            {
                soldierActionScript.preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
            }
        }
    }
}
