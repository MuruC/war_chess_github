using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobaDef {
    public static int eState_Normal = 0;
    public static int eState_Attack = 1;
    public static int eState_Defense = 2;
    public static int eState_LevelUP = 3;
    public static int eState_Sell = 4;
}

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
        private int maxHP;
        private int attack;
        private int currentDefense;
        private int currentState;
        public Soldiers(int nType,int nAlign) {
            nEntityType = nType;
            nAlignType = nAlign;
            currentHP = getMaxHealth();
            currentMoveStep = getMaxMoveStep();
            currentDefense = 0;
            currentState = GlobaDef.eState_Normal;
        }

        //士兵当前状态
        public int getState() {
            return currentState;
        }

        public void setState(int nState) {
            currentState = nState;
        }

        //重置士兵状态
        public void resetState() {
            currentState = GlobaDef.eState_Normal;
            setDefense(0);
            setCurrentMoveStep(getMaxMoveStep());
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
                attack = 22;
                return 22;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                attack = 8;
                return 8;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                attack = 27;
                return 27;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                attack = 17;
                return 17;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                attack = 42;
                return 42;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                attack = 35;
                return 35;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                attack = 55;
                return 55;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                attack = 47;
                return 47;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 0;
            }
            return 0;
        }

        public int getView() {
            if (nEntityType == eEntityType_Warrior)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                return 2;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 1;
            }
            return 0;
        }

        public int getAttackDistance() {
            if (nEntityType == eEntityType_Warrior)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                return 1;
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
                return 1;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                return 2;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                return 1;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                return 1;
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
                return 6;
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
                maxHP = 40;
                return 40;
            }
            else if (nEntityType == eEntityType_Scout)
            {
                maxHP = 25;
                return 25;
            }
            else if (nEntityType == eEntityType_Knight)
            {
                maxHP = 60;
                return 60;
            }
            else if (nEntityType == eEntityType_Archer)
            {
                maxHP = 35;
                return 35;
            }
            else if (nEntityType == eEntityType_Cannon)
            {
                maxHP = 50;
                return 50;
            }
            else if (nEntityType == eEntityType_Musketeer)
            {
                maxHP = 60;
                return 60;
            }
            else if (nEntityType == eEntityType_Tank)
            {
                maxHP = 65;
                return 65;
            }
            else if (nEntityType == eEntityType_Infantry)
            {
                maxHP = 68;
                return 68;
            }
            else if (nEntityType == eEntityType_Medical)
            {
                maxHP = 25;
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

        public void setHP(int value) {
            currentHP = value;
        }

        public void modifyHP(int value) {
            currentHP += value;
            currentHP = Mathf.Clamp(currentHP,0,maxHP);
        }

        public int getCurrentHP() {
            return currentHP;
        }
        public void modifyMaxHP(int value) {
            maxHP += value;
        }
        public int getMaxHP() {
            return maxHP;
        }
        public void setCurrentMoveStep(int move) {
            currentMoveStep = move;
        }

        public int getCurrentMoveStep() {
            return currentMoveStep;
        }

        public void setDefense(int defense) {
            currentDefense = defense;
        }
        public int getDefense() {
            return currentDefense; 
        }
        public void modifyDefense(int value) {
            currentDefense += value;
            currentDefense = Mathf.Clamp(currentDefense,0,getMaxMoveStep());
        }
        public void modifyAttack(int value) {
            attack += value;
        }
        public int getAttack() {
            return attack;
        }
    }


}
