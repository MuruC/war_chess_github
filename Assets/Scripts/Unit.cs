using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Unit : MonoBehaviour
{
    public Vector2 destination;
    public int destinationXIndex;
    public int destinationYIndex;
    public SoldierType.Soldiers m_pSoldier;
    public int entityType;
    public int unitAlign;
    public int spriteIndex;
    public float speed;
    public bool thisUnitHasBeenClicked;
    public GameObject mouseController;
    public List<GameObject> greenTiles;
    public bool bMoving = false;
    GameObject myPlayerAlign;
    PlayerAlign.Players m_player1;
    PlayerAlign.Players m_player2;
    public int indexX;
    public int indexY;
    public bool beenUpgraded;
    public GameObject floatingText;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance == null) {
            return;
        }
        destination = transform.position;
        greenTiles = new List<GameObject>();
        myPlayerAlign = GameObject.Find("Players");
        m_player1 = myPlayerAlign.GetComponent<PlayerAlign>().m_Player1;
        m_player2 = myPlayerAlign.GetComponent<PlayerAlign>().m_Player2;
        //unitAlign = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x == destination.x && transform.position.y == destination.y)
        {
            if(bMoving)
            {
                bMoving = false;
                whenUnitFirstArriveDestination();
            }
        }
        else
        {
            bMoving = true;
        }

       
        Vector2 dir = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);
        Vector2 velocity = dir.normalized * speed * Time.deltaTime;

        velocity = Vector2.ClampMagnitude(velocity, dir.magnitude);

        transform.Translate(velocity);
       
        gameObject.GetComponent<SpriteRenderer>().sprite = UIManager.Instance.unitSprites[spriteIndex];

        thisUnitHasBeenClicked = false;

        if (m_pSoldier != null && m_pSoldier.getCurrentHP() <= 0) {
            int money = m_pSoldier.getMoneyByKillingEnemy();
            int xp = m_pSoldier.getXPByKillingEnemy();
            if (unitAlign == 0) {
                m_player2.setMoney(money + m_player2.getMoney());
                m_player2.setXP(xp+m_player2.getXP());
            } else if (unitAlign == 1) {
                m_player1.setMoney(money + m_player1.getMoney());
                m_player1.setXP(xp + m_player1.getXP());
            }
            GameManager.Instance.modifyUnitDic(unitAlign,gameObject.name);
            Destroy(gameObject);
        }
    }

    //当士兵到达目的地之后，绿色消失
    void whenUnitFirstArriveDestination() {
        if (thisUnitHasBeenClicked == false)
        {
            for (int i = 0; i < greenTiles.Count; i++)
            {
                greenTiles[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
            greenTiles.Clear();
        }
        //thisUnitHasBeenClicked = false;
        GameObject pTile = GameObject.Find("Terrain_" + destinationXIndex.ToString() + "_" + destinationYIndex.ToString());
        int spriteType = pTile.GetComponent<TileScript>().spriteType;
        //碰到金矿图，金钱+15
        if (spriteType == 4)
        {
            if (GameManager.Instance.getTurn() == 0)
            {
                m_player1.modifyMoney(15);
            }
            else if (GameManager.Instance.getTurn() == 1)
            {
                m_player2.modifyMoney(15);
            }
            pTile.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.terrainSprites[3];
            pTile.GetComponent<TileScript>().spriteType = 3;
            GameObject popText = Instantiate(floatingText, transform.position, Quaternion.identity);
            popText.GetComponent<TextMesh>().text = "Money+$15";
            popText.GetComponent<TextMesh>().color = Color.yellow;
            SoundEffectManager.Instance.playAudio(0);
        }
        //碰到奶牛图，血量+10
        if (spriteType == 5)
        {
            m_pSoldier.modifyHP(10);
            pTile.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.terrainSprites[0];
            pTile.GetComponent<TileScript>().spriteType = 0;
            GameObject popText = Instantiate(floatingText, transform.position, Quaternion.identity);
            popText.GetComponent<TextMesh>().text = "HP+10";
            popText.GetComponent<TextMesh>().color = Color.green;
            SoundEffectManager.Instance.playAudio(1);
        }
        m_pSoldier.setCurrentMoveStep(m_pSoldier.getCurrentMoveStep() - 1);

        //到达终点
        if (GameManager.Instance.getTurn() == 0) {
            if (destinationXIndex == 7 && destinationYIndex == 11) {
                UIManager.Instance.endingScenePanel.SetActive(true);
                UIManager.Instance.endingScenePanel.GetComponent<Image>().sprite = UIManager.Instance.orangeWin;
            }
        } else if (GameManager.Instance.getTurn() == 1) {
            if (destinationXIndex == 7 && destinationYIndex == 0) {
                UIManager.Instance.endingScenePanel.SetActive(true);
                UIManager.Instance.endingScenePanel.GetComponent<Image>().sprite = UIManager.Instance.purpleWin;
            }
        }
    }

    public void setEntity(int nType,int nAlign) {
        entityType = nType;
        unitAlign = GameManager.Instance.getTurn();
        m_pSoldier = new SoldierType.Soldiers(entityType, nAlign);
        spriteIndex = m_pSoldier.returnSpriteIndex();
       // gameObject.GetComponent<SpriteRenderer>().sprite = UIManager.Instance.unitSprites[spriteIndex];
    }

    /*
    public void testSwitchSprite() {
        gameObject.GetComponent<SpriteRenderer>().sprite = UIManager.Instance.unitSprites[(int)Random.Range(0,8)];
    }
    */
    
}
