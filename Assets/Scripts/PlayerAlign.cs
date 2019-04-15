using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAlign : MonoBehaviour
{
    static int redAlign = 0;
    static int purpleAlign = 1;

    public class Players {
        private int nAlign;
        int currentMoney;
        int currentXP;
        int currentLevel;
        Sprite playerAlignSprite;
        public Players(int nAlign_){
            nAlign = nAlign_;
            currentMoney = 10;
            currentXP = 0;
            currentLevel = 1;
            playerAlignSprite = UIManager.Instance.AlignImages[0];
        }

        public void setMoney(int money) {
            currentMoney = money;
        }
        public int getMoney() {
            return currentMoney;
        }
        public void modifyMoney(int value)
        {
            currentMoney += value;
        }
        public void setXP(int XP) {
            currentXP = XP;
        }

        public int getXP() {
            return currentXP;
        }
        public void setLevel(int newLevel) {
            currentLevel = newLevel;
        }
        public int getLevel() {
            return currentLevel;
        }
        public Sprite getAlignImage() {
            if (nAlign == redAlign)
            {
                playerAlignSprite = UIManager.Instance.AlignImages[0];
            }
            else if(nAlign == purpleAlign){
                playerAlignSprite = UIManager.Instance.AlignImages[1];
            }
            return playerAlignSprite;
        }

        public void checkLevel() {
            if (getLevel() < 2 ) {
                if (getXP() >= 3) {
                    setLevel(2);
                    setXP(getXP() - 3);
                }
            }
            if (getLevel() >= 2 && getLevel() < 3) {
                if (getXP() >= 7) {
                    setLevel(3);
                    setXP(getXP() - 7);
                }
            }
            if (getLevel() >=3 && getLevel() < 4) {
                if (getXP() >= 14) {
                    setLevel(4);
                    setXP(getXP() - 14);
                }
            }
        }

        public void unlockSoldierTypes() {
            if (getLevel() == 1) {
                for (int i = 0; i < 7; i++)
                {
                    UIManager.Instance.recruitSoldierTypeButton_off[i].SetActive(true);
                }
            }
            if (getLevel() == 2) {
                UIManager.Instance.recruitSoldierTypeButton_off[0].SetActive(false);
                UIManager.Instance.recruitSoldierTypeButton_off[1].SetActive(false);
                UIManager.Instance.recruitSoldierTypeButton_off[2].SetActive(true);
                UIManager.Instance.recruitSoldierTypeButton_off[3].SetActive(true);
                UIManager.Instance.recruitSoldierTypeButton_off[4].SetActive(true);
                UIManager.Instance.recruitSoldierTypeButton_off[5].SetActive(true);
                UIManager.Instance.recruitSoldierTypeButton_off[6].SetActive(true);
            }
            if (getLevel() == 3) {
                UIManager.Instance.recruitSoldierTypeButton_off[0].SetActive(false);
                UIManager.Instance.recruitSoldierTypeButton_off[1].SetActive(false);
                UIManager.Instance.recruitSoldierTypeButton_off[2].SetActive(false);
                UIManager.Instance.recruitSoldierTypeButton_off[3].SetActive(false);
                UIManager.Instance.recruitSoldierTypeButton_off[4].SetActive(true);
                UIManager.Instance.recruitSoldierTypeButton_off[5].SetActive(true);
                UIManager.Instance.recruitSoldierTypeButton_off[6].SetActive(true);
            }
            if (getLevel() == 4) {
                for (int i = 0; i < 7; i++)
                {
                    UIManager.Instance.recruitSoldierTypeButton_off[i].SetActive(false);
                }
            }
        }
    }


    public PlayerAlign.Players m_Player1;
    public PlayerAlign.Players m_Player2;
    int nAlign;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance == null) {
            return;
        }
        setPlayer1(0);
        setPlayer2(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) {
            return;
        }
        int currentAlign = GameManager.Instance.getTurn();
        if (currentAlign == 0) {
            if (m_Player1 == null) {
                return;
            }
            UIManager.Instance.alignImage.sprite = m_Player1.getAlignImage();
            UIManager.Instance.money.text = m_Player1.getMoney().ToString();
            UIManager.Instance.level.text = m_Player1.getLevel().ToString();
            UIManager.Instance.xp.text = m_Player1.getXP().ToString();
            m_Player1.checkLevel();
            m_Player1.unlockSoldierTypes();
        } else if (currentAlign == 1) {
            if (m_Player2 == null)
            {
                return;
            }
            UIManager.Instance.alignImage.sprite = m_Player2.getAlignImage();
            UIManager.Instance.money.text = m_Player2.getMoney().ToString();
            UIManager.Instance.level.text = m_Player2.getLevel().ToString();
            UIManager.Instance.xp.text = m_Player2.getXP().ToString();

            m_Player2.checkLevel();
            m_Player2.unlockSoldierTypes();
        }
    }

    public void setPlayer1(int nAlign) {
        m_Player1 = new PlayerAlign.Players(nAlign);
        GameManager.Instance.myPlayer1 = m_Player1;
    }

    public void setPlayer2(int nAlign)
    {
        m_Player2 = new PlayerAlign.Players(nAlign);
        GameManager.Instance.myPlayer2 = m_Player2;
    }
}
