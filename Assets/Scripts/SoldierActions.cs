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
    public GameObject preEnemyObject;
    public GameObject myCam;
    CameraShake myCamShakeScript;
    SoldierType.Soldiers m_pUnit;
    public GameObject PlayerController;
    PlayerAlign.Players m_player1;
    PlayerAlign.Players m_player2;
    private string strLastSelectSoldierName = "";
    public GameObject floatingText;
    public GameObject bloodPS;
    GameObject cameraMain;
    private int attackEndFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        myMouseControllerScript = myMouseController.GetComponent<MouseController>();
        myCamShakeScript = myCam.GetComponent<CameraShake>();

        m_player1 = PlayerController.GetComponent<PlayerAlign>().m_Player1;
        m_player2 = PlayerController.GetComponent<PlayerAlign>().m_Player2;

        cameraMain = GameObject.Find("Camera");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (actionModeName != "Attack") {
            destroyAttackArrow();
        }
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

        /*
        if (bCancelAttackMode == true) {
            float curTimeStamp = Time.time;
            if (curTimeStamp > nCancelAttackModeTimeStamp + 20f)
            {
                actionModeName = null;
                Reset();
            }
        }
        */
        if (attackEndFrame != 0) {
            if (Time.frameCount - attackEndFrame > 60) {
                Reset();
                attackEndFrame = 0;
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
                                //int x1 = selectedSoldier.GetComponent<Unit>().indexX;
                                //int y1 = selectedSoldier.GetComponent<Unit>().indexY;
                                //int x2 = enemyObj.GetComponent<Unit>().indexX;
                                //int y2 = enemyObj.GetComponent<Unit>().indexY;
                                Vector2 vecSrc = selectedSoldier.transform.localPosition;
                                Vector2 vecDst = enemyObj.transform.localPosition;
                                float fDist = Vector2.Distance(vecSrc,vecDst);
                                float fScale = 1.8f;
                                int nAtkDistance = selectedSoldier.GetComponent<Unit>().m_pSoldier.getAttackDistance();
                                if (fDist <= nAtkDistance * fScale) {
                                    //鼠标移到敌人身上，敌人变红
                                    enemyObj.GetComponent<SpriteRenderer>().color = Color.red;
                                    //鼠标移开后敌人颜色复原
                                    if (preEnemyObject != null && preEnemyObject != enemyObj) {
                                        if (GameManager.Instance.getTurn() == 0)
                                        {
                                            preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(253, 0, 255);
                                            //preEnemyObject.GetComponent<SpriteRenderer>().color = Color.white;
                                        }
                                        else {
                                            preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
                                            //preEnemyObject.GetComponent<SpriteRenderer>().color = Color.white;
                                        }
                                    }
                                    //pressLeftMouseButtonToAttack(enemyObj);
                                    if (enemyObj != null) {
                                        preEnemyObject = enemyObj;
                                    }
                                    pressLeftMouseButtonToAttack(enemyObj);
                                   // pressRightMouseButtoncancelAttackMode();
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
        if (Input.GetMouseButtonUp(0)) {
            if (enemyObj.GetComponent<Unit>() == null || selectedSoldier.GetComponent<Unit>() == null) {
                return;
            }
            SoldierType.Soldiers thisEnemyObj = enemyObj.GetComponent<Unit>().m_pSoldier;
            SoldierType.Soldiers mySoldier = selectedSoldier.GetComponent<Unit>().m_pSoldier;
            int attack = mySoldier.getAttack();
            Debug.Log("attack: " + attack);
            int defense = thisEnemyObj.getDefense();
            if (defense > 0) {
                if (attack > defense)
                {
                    attack -= defense;
                    thisEnemyObj.setDefense(0);
                }
                else {
                    attack = 0;
                    thisEnemyObj.modifyDefense(-attack);
                }
            }
            thisEnemyObj.modifyHP(-attack);
            GameObject floatingTextObj = Instantiate(floatingText,enemyObj.transform.position,Quaternion.identity);
            floatingTextObj.GetComponent<TextMesh>().text = "-"+attack.ToString();
            Instantiate(bloodPS, enemyObj.transform.position, Quaternion.identity);
            int selectedSoldierType = selectedSoldier.GetComponent<Unit>().entityType;
            //攻击音效
            if (selectedSoldierType == 1 || selectedSoldierType == 2 || selectedSoldierType == 3) {
                SoundEffectManager.Instance.playAudio(5);
            } else if (selectedSoldierType == 4) {
                SoundEffectManager.Instance.playAudio(8);
            } else if (selectedSoldierType == 5 || selectedSoldierType == 7) {
                SoundEffectManager.Instance.playAudio(6);
            } else if (selectedSoldierType == 6) {
                SoundEffectManager.Instance.playAudio(9);
            } else if (selectedSoldierType == 8) {
                SoundEffectManager.Instance.playAudio(10);
            }
            selectedSoldier.GetComponent<Unit>().m_pSoldier.modifyCurrentMoveStep(-1);
            myCamShakeScript.shake(0.15f, 0.3f);
            //bCancelAttackMode = true;
           // actionModeName = null;
            destroyAttackArrow();
            attackEndFrame = Time.frameCount;
            //Reset();
        }
    }
    //右键取消攻击
    void pressRightMouseButtoncancelAttackMode() {
        if (Input.GetMouseButton(1))
        {
            actionModeName = null;
            destroyAttackArrow();
            if (preEnemyObject == null) {
                return;
            }
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

    //销毁攻击箭头
    public void destroyAttackArrow() {
        if (GameObject.Find(strLastSelectSoldierName) != null) {
            Destroy(GameObject.Find(strLastSelectSoldierName));
            strLastSelectSoldierName = ""; 
        }
    }

    public void chooseActionMode(string actionMode) {
        if (strLastSelectSoldierName != "") {
            destroyAttackArrow();
        }

        actionModeName = actionMode;
        if (selectedSoldier != null) {
            switch (actionModeName) {
                case "Attack":
                    if (selectedSoldier.GetComponent<Unit>().m_pSoldier.getCurrentMoveStep() > 0) {
                        actionModeName = "Attack";
                        GameObject newAttackArrow = Instantiate(attackArrow, selectedSoldier.transform.position, Quaternion.identity);
                        newAttackArrow.name = selectedSoldier.name + "_attackArrow";
                        strLastSelectSoldierName = newAttackArrow.name;
                        m_pUnit.setState(1);
                    }
                    break;
                case "Defense":
                    actionModeName = "Defense";
                    int blockAttack = m_pUnit.getCurrentMoveStep();
                    m_pUnit.setDefense(blockAttack);
                    m_pUnit.setCurrentMoveStep(0);
                    m_pUnit.setState(2);
                    GameObject floatingTextObj = Instantiate(floatingText, selectedSoldier.transform.position, Quaternion.identity);
                    floatingTextObj.GetComponent<TextMesh>().text = "Block " + blockAttack.ToString()+" hit!";
                    floatingTextObj.GetComponent<TextMesh>().color = Color.white;
                    SoundEffectManager.Instance.playAudio(12);
                    break;
                case "LevelUP":
                    actionModeName = "Defense";
                    m_pUnit.setState(3);
                    break;
                case "Sell":
                    int value = m_pUnit.getMoneyBySellingEntity();
                    if (GameManager.Instance.getTurn() == 0)
                    {
                        m_player1.setMoney(m_player1.getMoney()+m_pUnit.getMoneyBySellingEntity());
                    }
                    else {
                        m_player2.setMoney(m_player2.getMoney() + m_pUnit.getMoneyBySellingEntity());
                    }
                    Destroy(selectedSoldier);
                    m_pUnit.setState(4);
                    Vector2 pos = new Vector2(cameraMain.transform.position.x+2, cameraMain.transform.position.y-1);
                    GameObject popText = Instantiate(floatingText,pos,Quaternion.identity);
                    popText.GetComponent<TextMesh>().text = "money+"+value.ToString();
                    SoundEffectManager.Instance.playAudio(0);
                    break;
            }
        }
    }

    /*
    public void setAttackDistanceIndex() {
        int a = GameManager.Instance.getUnitPosXIndex(selectedSoldier.name);
        int b = GameManager.Instance.getUnitPosYIndex(selectedSoldier.name);
        SoldierType.Soldiers mySoldier = selectedSoldier.GetComponent<Unit>().m_pSoldier;
        if (mySoldier.getAttackDistance() == 1) {
            if (b % 2 == 1)
            {
                checkEnemyWithinAttackDistance(a + 1, b);
                checkEnemyWithinAttackDistance(a, b + 1);
                checkEnemyWithinAttackDistance(a + 1, b + 1);
                checkEnemyWithinAttackDistance(a + 1, b - 1);
                checkEnemyWithinAttackDistance(a, b - 1);
                checkEnemyWithinAttackDistance(a, b);
                checkEnemyWithinAttackDistance(a - 1, b);
            }
            else
            {
                checkEnemyWithinAttackDistance(a, b + 1);
                checkEnemyWithinAttackDistance(a + 1, b);
                checkEnemyWithinAttackDistance(a - 1, b + 1);
                checkEnemyWithinAttackDistance(a - 1, b);
                checkEnemyWithinAttackDistance(a - 1, b - 1);
                checkEnemyWithinAttackDistance(a, b - 1);
                checkEnemyWithinAttackDistance(a, b);
            }
        } else if (mySoldier.getAttackDistance() == 2) {
            if (b % 2 == 1)
            {
                checkEnemyWithinAttackDistance(a + 1, b);
                checkEnemyWithinAttackDistance(a, b + 1);
                checkEnemyWithinAttackDistance(a + 1, b + 1);
                checkEnemyWithinAttackDistance(a + 1, b - 1);
                checkEnemyWithinAttackDistance(a, b - 1);
                checkEnemyWithinAttackDistance(a, b);
                checkEnemyWithinAttackDistance(a - 1, b);
                checkEnemyWithinAttackDistance(a + 2, b);
                checkEnemyWithinAttackDistance(a + 2,b - 1);
                checkEnemyWithinAttackDistance(a + 1, b - 2 );
                checkEnemyWithinAttackDistance(a,b-2);
                checkEnemyWithinAttackDistance(a-1,b-2 );
                checkEnemyWithinAttackDistance(a-1,b-1 );
                checkEnemyWithinAttackDistance(a-2,b );
                checkEnemyWithinAttackDistance(a-1,b+1 );
                checkEnemyWithinAttackDistance(a - 1,b+2 );
                checkEnemyWithinAttackDistance(a,b+2 );
                checkEnemyWithinAttackDistance(a + 1,b + 2);
                checkEnemyWithinAttackDistance(a + 2, b + 1);

            }
            else
            {
                checkEnemyWithinAttackDistance(a, b + 1);
                checkEnemyWithinAttackDistance(a + 1, b);
                checkEnemyWithinAttackDistance(a - 1, b + 1);
                checkEnemyWithinAttackDistance(a - 1, b);
                checkEnemyWithinAttackDistance(a - 1, b - 1);
                checkEnemyWithinAttackDistance(a, b - 1);
                checkEnemyWithinAttackDistance(a, b);
                checkEnemyWithinAttackDistance(a+2, b);
                checkEnemyWithinAttackDistance(a+1,b-1 );
                checkEnemyWithinAttackDistance(a+1,b-2 );
                checkEnemyWithinAttackDistance(a,b-2 );
                checkEnemyWithinAttackDistance(a-1,b-2 );
                checkEnemyWithinAttackDistance(a-2,b-1 );
                checkEnemyWithinAttackDistance(a-2, b);
                checkEnemyWithinAttackDistance(a-2,b+1 );
                checkEnemyWithinAttackDistance(a-1,b+2 );
                checkEnemyWithinAttackDistance(a,b+2 );
                checkEnemyWithinAttackDistance(a+1, b+2);
                checkEnemyWithinAttackDistance(a+1, b+1);
            }
        }
    }

    
    void checkEnemyWithinAttackDistance(int x, int y) {
        int enemyXindex = GameManager.Instance.getUnitPosXIndex(preEnemyObject.name);
        int enemyYindex = GameManager.Instance.getUnitPosYIndex(preEnemyObject.name);
    }
    */

    //升级，最大血量和当前血量各加5
    public void addHealth() {
        if (selectedSoldier.GetComponent<Unit>() == null || selectedSoldier == null) { return; }
        if (selectedSoldier.GetComponent<Unit>().beenUpgraded == false) {
            if (GameManager.Instance.getTurn() == 0) {
                if (selectedSoldier.GetComponent<Unit>().m_pSoldier.getXPToLevelUp() > m_player1.getXP())
                {
                    return;
                }
                else {
                    SoldierType.Soldiers mySoldier = selectedSoldier.GetComponent<Unit>().m_pSoldier;
                    mySoldier.modifyMaxHP(5);
                    mySoldier.modifyHP(5);
                    selectedSoldier.GetComponent<Unit>().beenUpgraded = true;
                    GameObject popText = Instantiate(floatingText, selectedSoldier.transform.position, Quaternion.identity);
                    popText.GetComponent<TextMesh>().text = "Max HP + 5";
                    popText.GetComponent<TextMesh>().color = Color.green;
                    SoundEffectManager.Instance.playAudio(2);
                    m_player1.modifyXP(-selectedSoldier.GetComponent<Unit>().m_pSoldier.getXPToLevelUp());
                }
            } else if (GameManager.Instance.getTurn() == 1) {
                if (selectedSoldier.GetComponent<Unit>().m_pSoldier.getXPToLevelUp() > m_player2.getXP())
                {
                    return;
                }
                else {
                    SoldierType.Soldiers mySoldier = selectedSoldier.GetComponent<Unit>().m_pSoldier;
                    mySoldier.modifyMaxHP(5);
                    mySoldier.modifyHP(5);
                    selectedSoldier.GetComponent<Unit>().beenUpgraded = true;
                    GameObject popText = Instantiate(floatingText, selectedSoldier.transform.position, Quaternion.identity);
                    popText.GetComponent<TextMesh>().text = "Max HP + 5";
                    popText.GetComponent<TextMesh>().color = Color.green;
                    SoundEffectManager.Instance.playAudio(2);
                    m_player2.modifyXP(-selectedSoldier.GetComponent<Unit>().m_pSoldier.getXPToLevelUp());
                }
            }
            
        }
    }

    //升级攻击力加5；
    public void addAttack() {
        if (selectedSoldier.GetComponent<Unit>() == null || selectedSoldier == null) { return; }
        if (selectedSoldier.GetComponent<Unit>().beenUpgraded == false)
        {
            if (GameManager.Instance.getTurn() == 0) {
                if (m_pUnit.getXPToLevelUp() > m_player1.getXP())
                {
                    return;
                }
                else
                {
                    SoldierType.Soldiers mySoldier = selectedSoldier.GetComponent<Unit>().m_pSoldier;
                    mySoldier.modifyAttack(5);
                    selectedSoldier.GetComponent<Unit>().beenUpgraded = true;
                    GameObject popText = Instantiate(floatingText, selectedSoldier.transform.position, Quaternion.identity);
                    popText.GetComponent<TextMesh>().text = "Attack + 5";
                    popText.GetComponent<TextMesh>().color = Color.white;
                    SoundEffectManager.Instance.playAudio(2);
                    m_player1.modifyXP(-m_pUnit.getXPToLevelUp());
                }
            } else if (GameManager.Instance.getTurn() == 1) {
                if (m_pUnit.getXPToLevelUp() > m_player2.getXP())
                {
                    return;
                }
                else {
                    SoldierType.Soldiers mySoldier = selectedSoldier.GetComponent<Unit>().m_pSoldier;
                    mySoldier.modifyAttack(5);
                    selectedSoldier.GetComponent<Unit>().beenUpgraded = true;
                    GameObject popText = Instantiate(floatingText, selectedSoldier.transform.position, Quaternion.identity);
                    popText.GetComponent<TextMesh>().text = "Attack + 5";
                    popText.GetComponent<TextMesh>().color = Color.white;
                    SoundEffectManager.Instance.playAudio(2);
                    m_player1.modifyXP(-m_pUnit.getXPToLevelUp());
                }
            }
        }
    }

    public void Reset() {
        actionModeName = "";
        /*
        if (preEnemyObject) {
            if (GameManager.Instance.getTurn() == 0) {
                preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(255, 185, 0);
            }
            else {
                preEnemyObject.GetComponent<SpriteRenderer>().color = new Color(253, 0, 255);
            }
        }
        */
    }
}
