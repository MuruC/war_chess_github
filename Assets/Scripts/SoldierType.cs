using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierType : MonoBehaviour
{
    static int eEntityType_Warrior = 1;
    static int eEntityType_Scout = 2;
    static int eEntityType_Knight = 3;
    static int eEntityType_Archer = 4;
    static int eEntityType_Cannon = 5;
    static int eEntityType_Musketeer = 6;
    static int eEntityType_Tank = 7;
    static int eEntityType_Infantry = 8;
    static int eEntityType_Medical = 9;

    static int redAlign = 0;
    static int purpleAlign = 1;

    

    public class Soldiers {
        private int nEntityType;
        private int nAlignType;
        int currentHP;
        int currentMoveStep;
        public Soldiers(int nType,int nAlign) {
            nEntityType = nType;
            nAlignType = nAlign;
            currentHP = getMaxHealth();
            currentMoveStep = getMaxMoveStep();
        }
        //最大行动数
        public int getMaxMoveStep() {
            if (nEntityType == eEntityType_Warrior) {
                return 2;
            } else if (nEntityType == eEntityType_Scout) {
                return 3;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return 3;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                return 2;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                return 2;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                return 2;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return 3;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return 2;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 1;
            }
            return 0;
        }
        //基础攻击力
        public int getBasicAttack()
        {
            if (nEntityType == eEntityType_Warrior)
            {
                return 22;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                return 8;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return 27;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                return 17;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                return 42;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                return 35;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return 55;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return 47;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 0;
            }
            return 0;
        }

        //购买所需金钱
        public int getMoneyToBuyEntity()
        {
            if (nEntityType == eEntityType_Warrior)
            {
                return 6;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                return 3;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return 15;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                return 8;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                return 18;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                return 25;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return 45;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return 35;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 40;
            }
            return 0;
        }

        //贩卖所得金钱
        public int getMoneyBySellingEntity()
        {
            if (nEntityType == eEntityType_Warrior)
            {
                return 3;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return 6;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                return 4;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                return 9;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                return 12;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return 22;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return 17;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 20;
            }
            return 0;
        }

        //击杀敌方单位获得经验
        public int getXPByKillingEnemy() {
            if (nEntityType == eEntityType_Warrior)
            {
                return 2;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return 4;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                return 3;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                return 6;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                return 5;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return 8;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return 7;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 5;
            }
            return 0;
        }

        //升级所需经验
        public int getXPToLevelUp() {
            if (nEntityType == eEntityType_Warrior)
            {
                return 3;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                return 2;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return 7;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                return 5;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                return 10;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                return 9;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return 14;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return 12;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 8;
            }
            return 0;
        }

        //击杀敌方获得金钱
        public int getMoneyByKillingEnemy() {
            if (nEntityType == eEntityType_Warrior)
            {
                return 9;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                return 5;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return 18;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                return 12;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                return 25;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                return 30;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return 60;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return 45;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 55;
            }
            return 0;
        }

        //最高血量
        public int getMaxHealth() {
            if (nEntityType == eEntityType_Warrior)
            {
                return 40;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                return 25;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return 60;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                return 35;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                return 50;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                return 60;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return 65;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return 68;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 25;
            }
            return 0;
        }

        //设置sprite
        public int returnSpriteIndex()
        {
            if (nEntityType == eEntityType_Warrior)
            {
                return 0;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return 2;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                return 3;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                return 4;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                return 5;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return 6;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return 7;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 8;
            }
            return 0;
        }

        //get name
        public string returnTypeName()
        {
            if (nEntityType == eEntityType_Warrior)
            {
                return "Warrior";
            }
            else if (nEntityType == eEntityType_Scout)
            {
                return "Scout";
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return "Knight";
            }
            else if (nEntityType == eEntityType_Archer)
            {
                return "Archer";
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                return "Cannon";
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                return "Musketeer";
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return "Tank";
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return "Infantry";
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return "Medical";
            }
            return " ";
        }

        public void setCurrentHP(int attack) {
            currentHP -= attack;
        }

        public int getCurrentHP() {
            return currentHP;
        }

        public void setCurrentMoveStep(int move) {
            currentMoveStep = move;
        }

        public int getCurrentMoveStep() {
            return currentMoveStep;
        }
    }


}
