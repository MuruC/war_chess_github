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
    // Start is called before the first frame update
    void Start()
    {
        myMouseControllerScript = myMouseController.GetComponent<MouseController>();
    }

    // Update is called once per frame
    void Update()
    {
        selectedSoldier = myMouseControllerScript.selectedUnitObject;
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
        SoldierType.Soldiers thisEnemyObj = new SoldierType.Soldiers(enemyObj.GetComponent<Unit>().entityType, enemyObj.GetComponent<Unit>().unitAlign);
        SoldierType.Soldiers mySoldier = new SoldierType.Soldiers(selectedSoldier.GetComponent<Unit>().entityType, selectedSoldier.GetComponent<Unit>().unitAlign);
        int attack = mySoldier.getBasicAttack();
        thisEnemyObj.setCurrentHP(attack);
        Debug.Log(thisEnemyObj.getCurrentHP());
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
                    break;
                case "LevelUP":
                    break;
                case "Sell":
                    break;
            }
        }
    }
}
