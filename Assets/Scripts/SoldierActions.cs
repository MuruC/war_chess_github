using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoldierActions : MonoBehaviour
{
    public GameObject attackArrow;
    public string actionModeName;
    MouseController myMouseControllerScript;
    public GameObject myMouseController;
    GameObject selectedSoldier;
    GameObject preEnemyObject;
    public GameObject myCam;
    CameraShake myCamShakeScript;
    SoldierType.Soldiers m_pUnit;
    public GameObject PlayerController;
    PlayerAlign.Players m_player1;
    PlayerAlign.Players m_player2;
    // Start is called before the first frame update
    void Start()
    {
        myMouseControllerScript = myMouseController.GetComponent<MouseController>();
        myCamShakeScript = myCam.GetComponent<CameraShake>();

        m_player1 = PlayerController.GetComponent<PlayerAlign>().m_Player1;
        m_player2 = PlayerController.GetComponent<PlayerAlign>().m_Player2;
    }

    // Update is called once per frame
    void Update()
    {
        selectedSoldier = myMouseControllerScript.selectedUnitObject;
        if (selectedSoldier == null || selectedSoldier.GetComponent<Unit>() == null) {
            return;
        }
        m_pUnit = selectedSoldier.GetComponent<Unit>().m_pSoldier;
        attackMode();


        if (actionModeName != "Attack") {
            if (GameObject.Find(selectedSoldier.name + "_attackArrow") != null) {
                Destroy(GameObject.Find(selectedSoldier.name + "_attackArrow"));
            }
        }
    }

    //当处于攻击模式下

    void attackMode() {
          if (actionModeName == "Attack") {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit != null) {
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider != null && hit[i].collider.transform.gameObject.GetComponent<Unit>() != null)
                    {
                        GameObject enemyObj = hit[i].collider.transform.gameObject;
                        if (enemyObj.GetComponent<Unit>() != null && selectedSoldier.GetComponent<Unit>() != null) {
                            if (enemyObj.GetComponent<Unit>().unitAlign != selectedSoldier.GetComponent<Unit>().unitAlign) {

                                //鼠标移到敌人身上，敌人变红
                                enemyObj.GetComponent<SpriteRenderer>().color = Color.red;
                                //鼠标移开后敌人颜色复原
                                    if (preEnemyObject != null && preEnemyObject != enemyObj) {
                                        if (GameManager.Instance.getTurn() == 0)
                                        {
                                            preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(253, 0, 255);
                                        }
                                        else {
                                            preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
                                        }
                                    }
                                pressLeftMouseButtonToAttack(enemyObj);
                                if (enemyObj != null) {
                                    preEnemyObject = enemyObj;
                                }
                            }
                        }
                    }
                }
            }

            pressRightMouseButtoncancelAttackMode();
        }
    }
    //左键攻击
    void pressLeftMouseButtonToAttack(GameObject enemyObj) {
        if (Input.GetMouseButton(0)) {
            if (enemyObj.GetComponent<Unit>() == null || selectedSoldier.GetComponent<Unit>() == null) {
                return;
            }
            SoldierType.Soldiers thisEnemyObj = enemyObj.GetComponent<Unit>().m_pSoldier;
            SoldierType.Soldiers mySoldier = selectedSoldier.GetComponent<Unit>().m_pSoldier;
            int attack = mySoldier.getBasicAttack();
            thisEnemyObj.setCurrentHP(attack);
            Debug.Log(thisEnemyObj.getCurrentHP());
            myCamShakeScript.shake(0.15f, 0.3f);
            actionModeName = null;
        }
    }
    //右键取消攻击
    void pressRightMouseButtoncancelAttackMode() {
        if (Input.GetMouseButton(1))
        {
            actionModeName = null;
            if (GameManager.Instance.getTurn() == 0)
            {
                preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(253, 0, 255);
            }
            else
            {
                preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
            }
        }
    }

    public void chooseActionMode(string actionMode) {
        actionModeName = actionMode;
        if (selectedSoldier != null) {
            switch (actionModeName) {
                case "Attack":
                    actionModeName = "Attack";
                    GameObject newAttackArrow = Instantiate(attackArrow,selectedSoldier.transform.position,Quaternion.identity);
                    newAttackArrow.name = selectedSoldier.name + "_attackArrow";
                    break;
                case "Defense":
                    actionModeName = "Defense";
                    int blockAttack = m_pUnit.getCurrentMoveStep();
                    m_pUnit.setCurrentMoveStep(0);
                    break;
                case "LevelUP":
                    break;
                case "Sell":
                    if (GameManager.Instance.getTurn() == 0)
                    {
                        m_player1.setMoney(m_player1.getMoney()+m_pUnit.getMoneyBySellingEntity());
                    }
                    else {
                        m_player2.setMoney(m_player2.getMoney() + m_pUnit.getMoneyBySellingEntity());
                    }
                    Destroy(selectedSoldier);
                    break;
            }
        }
    }
}
