using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int turn;
    public GameObject orangeCastle;
    public GameObject purpleCastle;
    public GameObject myMainCamera;
    [Header("Terrain")]
    public int[,] terrainType;
    public Sprite[] terrainSprites;
    List<GameObject> terrainPrefabList;
    public GameObject[,] terrains;
    public GameObject terrainPrefab;
    public int width;
    public int height;
    public float xOffset;
    public float yOffset;
    public int xBeginPos;
    public int yBeginPos;
    int terrainIndex;
    public float[,] arrPosX;
    public float[,] arrPosY;
    GameObject gridHolder;
    private Dictionary<string, int> tileIndexXDic;
    private Dictionary<string, int> tileIndexYDic;
    private Dictionary<string, GameObject> soldier1Dic;
    private Dictionary<string, GameObject> soldier2Dic;
    private Dictionary<string, int> unitPosXIndex;
    private Dictionary<string, int> unitPosYIndex;
    private Dictionary<string, GameObject> tileObjectByIndex;
    private Dictionary<string, int> soldierType;
    private Dictionary<string, int> soldierAlign;

    private Dictionary<string, GameObject> player1BlackTiles;
    private Dictionary<string, GameObject> player2BlackTiles;
    MouseController mouseControllerScript;
    public GameObject mouseController;
    public GameObject blackTile;

    public GameObject playerController;
    public PlayerAlign.Players myPlayer1;
    public PlayerAlign.Players myPlayer2;
    PlayerAlign playerAlignScript;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null) {
            return;
        }
        reset();

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(tileObjectByIndex.Count);
        //getTileObjectByIndex("xIndex_0yIndex_1").GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void setSprite(int rand, int x_, int y_) {
        // terrainType[x_,y_] = rand;
        Sprite sprite = terrainSprites[rand];
        getTerrainGameObject(x_, y_).GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public int getTerrainType(int x_, int y_) {
        return terrainType[x_, y_];
    }

    public void setTerrainGameObject(GameObject p, int x_, int y_) {
        terrains[x_, y_] = p;
    }

    public GameObject getTerrainGameObject(int x_, int y_) {
        return terrains[x_, y_];
    }

    public void setTerrainPos(int x_, int y_, float xPos, float yPos) {
        arrPosX[x_, y_] = xPos;
        arrPosY[x_, y_] = yPos;
        Vector2 terrainPos = new Vector2(xPos, yPos);
        terrains[x_, y_].transform.parent = gridHolder.transform;
        terrains[x_, y_].transform.position = terrainPos;
    }

    public Vector2 getTerrainPos(int x_, int y_) {
        return terrains[x_, y_].transform.position;
    }

    public void setTileIndexXDic(string tileName, int posIndexX) {
        tileIndexXDic.Add(tileName, posIndexX);
    }
    public int getTileIndexXDic(string tileName) {
        return tileIndexXDic[tileName];
    }

    public void setTileIndexYDic(string tileName, int posIndexY)
    {
        tileIndexYDic.Add(tileName, posIndexY);
    }
    public int getTileIndexYDic(string tileName)
    {
        return tileIndexYDic[tileName];
    }

    public void setSoldierObjectDic1(string soldierName, GameObject soldierUnit) {
        soldier1Dic.Add(soldierName, soldierUnit);
    }

    public GameObject getSoldier1Unit1(string soldierName) {
        return soldier1Dic[soldierName];
    }

    public void setSoldierObjectDic2(string soldierName, GameObject soldierUnit)
    {
        soldier2Dic.Add(soldierName, soldierUnit);
    }

    public GameObject getSoldierUnit2(string soldierName)
    {
        return soldier2Dic[soldierName];
    }

    public void setUnitPosXIndex(string soldierName, int posXIndex) {
        unitPosXIndex[soldierName] = posXIndex;
    }
    public int getUnitPosXIndex(string soldierName) {
        return unitPosXIndex[soldierName];
    }

    public void setUnitPosYIndex(string soldierName, int posYIndex)
    {
        unitPosYIndex[soldierName] = posYIndex;
    }
    public int getUnitPosYIndex(string soldierName)
    {
        return unitPosYIndex[soldierName];
    }

    public void setTileObjectByIndex(string indexToString, GameObject ptile) {
        tileObjectByIndex[indexToString] = ptile;
    }

    public GameObject getTileObjectByIndex(string indexToString) {
        return tileObjectByIndex[indexToString];
    }

    public void setSoldierType(string unitName, int soldierTypeIndex) {
        soldierType[unitName] = soldierTypeIndex;
    }

    public int getSoldierTypeIndex(string unitName) { return soldierType[unitName]; }

    public void setSoldierAlign(string unitName, int soldierAlignIndex)
    {
        soldierType[unitName] = soldierAlignIndex;
    }

    public int getSoldierAlign(string unitName) { return soldierType[unitName]; }
    //改变回合
    public void setTurn() {
        if (mouseControllerScript.selectedUnitObject != null)
        {
            mouseControllerScript.selectedUnitObject = null;
        }
 
        UIManager.Instance.soldierActionPanel.SetActive(false);
        mouseControllerScript.showUnitInformation(false);

        UIManager.Instance.maxHP.GetComponent<Text>().text = null;
        UIManager.Instance.attackValue.GetComponent<Text>().text = null;
        UIManager.Instance.maxMoveStep.GetComponent<Text>().text = null;
        UIManager.Instance.soldierTypeName.GetComponent<Text>().text = null;
        UIManager.Instance.soldierTypeImage.sprite = UIManager.Instance.noSprite;
        UIManager.Instance.currentMoveStep.GetComponent<Text>().text = null;
        UIManager.Instance.currentHP.GetComponent<Text>().text = null;
        //当现在的回合是橙色方，变成紫色方
        if (turn == 0)
        {
            turn = 1;
            myMainCamera.transform.position = new Vector3(0, 12, -10);
            //玩家金钱自然增长2
            myPlayer2.modifyMoney(2);
            ///*
            if (soldier2Dic.Count > 0)
            {
                for (int i = 0; i < soldier2Dic.Count; i++)
                {
                    if (soldier2Dic.ElementAt(i).Value == null)
                    {
                        return;
                    }
                    soldier2Dic.ElementAt(i).Value.GetComponent<Unit>().m_pSoldier.resetState();
                    soldier2Dic.ElementAt(i).Value.GetComponent<SpriteRenderer>().color = new Color(253, 0, 255);
                }
            }
            //*/
            if (soldier1Dic.Count > 0)
            {
                for (int i = 0; i < soldier1Dic.Count; i++)
                {
                    if (soldier1Dic.ElementAt(i).Value == null) {
                        return;
                    }
                    soldier1Dic.ElementAt(i).Value.GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
                    
                }
            }

        }
        else {
            turn = 0;
            //玩家金钱自然增长2
            myPlayer1.modifyMoney(2);
            myMainCamera.transform.position = new Vector3(0, 0, -10);
            ///*
            if (soldier1Dic.Count > 0)
            {
                for (int i = 0; i < soldier1Dic.Count; i++)
                {
                    if (soldier1Dic.ElementAt(i).Value == null) { return; }
                    soldier1Dic.ElementAt(i).Value.GetComponent<Unit>().m_pSoldier.resetState();
                }
            }
           // */
            if (soldier2Dic.Count > 0)
            {
                for (int i = 0; i < soldier1Dic.Count; i++)
                {
                    if (soldier2Dic.ElementAt(i).Value == null) { return; }
                    soldier2Dic.ElementAt(i).Value.GetComponent<SpriteRenderer>().color = new Color(253, 0, 255);
                }
            }
        }
        restoreAllHexColor();
    }

    public int getTurn() {
        return turn;
    }
    //把所有的六边形变回原来的颜色
    public void restoreAllHexColor() {
        for (int i = 0; i < tileObjectByIndex.Count; i++)
        {
            tileObjectByIndex.ElementAt(i).Value.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void setPlayer1BlackTiles(string name, GameObject newtiles) {
        player1BlackTiles[name] = newtiles;
    }

    public void destroyPlayersBlackTiles(string name, GameObject destroyTiles) {
        Destroy(destroyTiles);
        if (getTurn() == 0)
        {
            player1BlackTiles.Remove(name);
        }
        else {
            player2BlackTiles.Remove(name);
        }
    }

    public void setPlayer1BlackTilesActive() {
        if (player1BlackTiles != null) {
            if (getTurn() == 0) {
                for (int i = 0; i < player1BlackTiles.Count; i++)
                {
                    if (player1BlackTiles.ElementAt(i).Value == null)
                    {
                        return;
                    }
                    player1BlackTiles.ElementAt(i).Value.SetActive(true);
                }

            } else if (getTurn() == 1) {
                for (int i = 0; i < player1BlackTiles.Count; i++)
                {
                    if (player1BlackTiles.ElementAt(i).Value == null)
                    {
                        return;
                    }
                    player1BlackTiles.ElementAt(i).Value.SetActive(false);
                }
            }
        }
    }

    public void removePlayer1BlackTile(int x, int y) {
        string name = "player1black_" + x + "_" + y;
        if (player1BlackTiles.ContainsKey(name) == false) {
            return;
        }
        if (player1BlackTiles[name] != null) {
            Destroy(player1BlackTiles[name]);
            player1BlackTiles.Remove(name);
        }
    }

    public void setPlayer2BlackTiles(string name, GameObject newtiles)
    {
        player2BlackTiles[name] = newtiles;
    }

    public void setPlayer2BlackTilesActive()
    {
        if (player2BlackTiles != null)
        {
            if (getTurn() == 1)
            {
                for (int i = 0; i < player1BlackTiles.Count; i++)
                {
                    if (player2BlackTiles.ElementAt(i).Value == null) {
                        return;
                    }
                    player2BlackTiles.ElementAt(i).Value.SetActive(true);
                }

            }
            else if (getTurn() == 0)
            {
                for (int i = 0; i < player2BlackTiles.Count; i++)
                {
                    if (player2BlackTiles.ElementAt(i).Value == null)
                    {
                        return;
                    }
                    player2BlackTiles.ElementAt(i).Value.SetActive(false);
                }
            }
        }
    }

    public void removePlayer2BlackTile(int x, int y)
    {
        string name = "player2black_" + x + "_" + y;
        if (player2BlackTiles.ContainsKey(name) == false)
        {
            return;
        }

        if (player2BlackTiles[name] != null)
        {
            Destroy(player2BlackTiles[name]);
            player2BlackTiles.Remove(name);
        }
    }

    public void reset() {
        Instance = this;
        terrainPrefabList = new List<GameObject>();
        terrainType = new int[width, height];
        arrPosX = new float[width, height];
        arrPosY = new float[width, height];
        terrains = new GameObject[width,height];
        gridHolder = new GameObject();
        gridHolder.transform.position = new Vector3(-1f, 0.5f, 0);
        tileIndexXDic = new Dictionary<string, int>();
        tileIndexYDic = new Dictionary<string, int>();
        soldier1Dic = new Dictionary<string, GameObject>();
        soldier2Dic = new Dictionary<string, GameObject>();
        unitPosXIndex = new Dictionary<string, int>();
        unitPosYIndex = new Dictionary<string, int>();
        tileObjectByIndex = new Dictionary<string, GameObject>();
        soldierAlign = new Dictionary<string, int>();
        soldierType = new Dictionary<string, int>();
        turn = 0;

        player1BlackTiles = new Dictionary<string, GameObject>();
        player2BlackTiles = new Dictionary<string, GameObject>();
        playerAlignScript = playerController.GetComponent<PlayerAlign>();

        mouseControllerScript = mouseController.GetComponent<MouseController>();
    }
}
