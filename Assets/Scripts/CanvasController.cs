using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasController : MonoBehaviour
{
    public GameObject mouseControllerObj;
    MouseController mouseControllerScript;
    GameObject selectedUnitObj;
    Unit selectedUnit;
    SoldierType.Soldiers m_pSoldier;
    // Start is called before the first frame update
    void Start()
    {
        mouseControllerScript = mouseControllerObj.GetComponent<MouseController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseControllerScript.selectedUnitObject == null) {
            return;
        }
        selectedUnitObj = mouseControllerScript.selectedUnitObject;
        selectedUnit = selectedUnitObj.GetComponent<Unit>();
        inactiveLevelUpMenu();
    }

    public void openRecruitMenu() {
        UIManager.Instance.recruitMenu.SetActive(true);
    }

    public void exitRecruitMenu() {
        UIManager.Instance.recruitMenu.SetActive(false);
    }

    public void exitLevelUpMenu() {
        UIManager.Instance.levelUpMenu.SetActive(false);
    }

    public void openLevelUpMenu() {
        if (selectedUnit == null) {
            return;
        }
        if (selectedUnit.beenUpgraded == false) {
            UIManager.Instance.levelUpMenu.SetActive(true);
        }
    }

    void inactiveLevelUpMenu() {
        if (selectedUnit == null) {
            return;
        }
        if (selectedUnit.beenUpgraded == true)
        {
            UIManager.Instance.levelUpButton.GetComponent<Image>().color = Color.grey;
            UIManager.Instance.addAttackButton.GetComponent<Image>().color = Color.grey;
            UIManager.Instance.addAttackText.GetComponent<Text>().color = Color.grey;
            UIManager.Instance.addhealthButton.GetComponent<Image>().color = Color.grey;
            UIManager.Instance.addhealthText.GetComponent<Text>().color = Color.grey;
        }
        else {
            UIManager.Instance.levelUpButton.GetComponent<Image>().color = Color.white;
            UIManager.Instance.addAttackButton.GetComponent<Image>().color = Color.white;
            UIManager.Instance.addAttackText.GetComponent<Text>().color = Color.white;
            UIManager.Instance.addhealthButton.GetComponent<Image>().color = Color.white;
            UIManager.Instance.addhealthText.GetComponent<Text>().color = Color.white;
        }
    }
}
