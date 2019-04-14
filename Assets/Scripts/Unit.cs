using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2 destination;
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
    // Start is called before the first frame update
    void Start()
    {
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
        m_pSoldier.setCurrentMoveStep(m_pSoldier.getCurrentMoveStep() - 1);
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
