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
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
        greenTiles = new List<GameObject>();
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
