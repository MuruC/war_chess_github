using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Sprite blackTile;
    public List<Sprite> unitSprites;
    public Sprite noSprite;
    [Header("Recruit menu")]
    public GameObject exitRecruitMenu;
    public GameObject recruitMenu;
    public List<GameObject> recruitSoldierTypeButton_off;

    [Header("Solider Information")]
    public Text soldierTypeName;
    public Text currentHP;
    public Text maxHP;
    public Text currentMoveStep;
    public Text maxMoveStep;
    public Text attackValue;
    public Image soldierTypeImage;
    [Header("Player Information")]
    public Text money;
    public Text level;
    public Text xp;
    public Image alignImage;
    public List<Sprite> AlignImages;

    [Header("Soldier Action")]
    public GameObject soldierActionPanel;
    public GameObject attackButton;
    public GameObject defenseButton;

    [Header("levelUpMenu")]
    public GameObject levelUpButton;
    public GameObject levelUpMenu;
    public GameObject addhealthButton;
    public GameObject addhealthText;
    public GameObject addAttackButton;
    public GameObject addAttackText;

    [Header("Ending")]
    public GameObject endingScenePanel;
    public Sprite orangeWin;
    public Sprite purpleWin;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null) {
            return;
        }
        resetUIManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resetUIManager() {
        Instance = this;
        maxHP.GetComponent<Text>().text = null;
        attackValue.GetComponent<Text>().text = null;
        maxMoveStep.GetComponent<Text>().text = null;
        soldierTypeName.GetComponent<Text>().text = null;
        soldierTypeImage.sprite = noSprite;
    }
}
