using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    - This class will handle the unit stats and calculations for damage, healing, buffs, and status effects
    - Each unit prefab will have this script attached to it to define its stats and functions
    - Functions will be called by the battleSystem class to handle the actual battle mechanics
    while this class will just do the calculations and stat changes
*/
public class Unit : MonoBehaviour
{
    public int unitSpd;
    public int unitAcc; // accuracy
    public String unitName;
    public String playerName;
    public int unitHp;
    public int unitMaxHp;
    public int unitAtk;
    public int unitSpAtk; // special attack
    public int unitDef;
    public int unitSpDef; // special defense
    public int statusCondition;

    public int specialization; // 0 for physical attack and 1 for magicial attack

    public int energy = 0;
    public int maxEnergy = 6;

    private void Awake()
    {
        unitName = GameManager.Instance.currentData.playerName ?? "Coyote";
        playerName = GameManager.Instance.currentData.playerName ?? "Coyote";
    }
    public bool takeDamage(int damage, int type) // type of damage here will be either 0 or 1, 0 for physcial, 1 for special 
    {
        statusEffect();
        if (type == 0) // for physical damage
        {
            damage -= unitDef; // reduce damage by defense
            if (damage < 0)
            {
                damage = 0; // prevent negative damage
            }

            unitHp -= damage; // just accounts the damage taken, not updated
            if (unitHp <= 0)
            {
                unitHp = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        else // for special damage
        {
            damage -= unitSpDef;
            if (damage < 0)
            {    
                damage = 0; // prevent negative damage
            }
            
            unitHp -= damage; // just accounts the damage taken, not updated
            if (unitHp <= 0)
            {
                unitHp = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool takeStatusDamage(int damage)
    {
        unitHp -= damage;
        if (unitHp <= 0)
        {
            unitHp = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void healDamage(int heal)
    {
        unitHp += heal;
        if (unitHp > unitMaxHp)
        {
            unitHp = unitMaxHp;
        }
        else
        {
            // do nothing
        }
    }

    public void buffStats(List<int> list)
    {
        if (list == null)
        {
            Debug.LogWarning("buffStats called with null list!");
            return;
        }
        
        int chance = UnityEngine.Random.Range(1,11);
        if (chance <= 5)
        {
            unitAtk += list[0];
            unitSpAtk += list[0];
            unitDef += list[1];
            unitSpDef += list[1];
            unitSpd += list[2];
            unitAcc += list[3];
            statusEffect();
            Debug.Log("Stats Increased");
        }
        else
        {
            // do nothing
        }
    }
    public void buffStatsNoChance(List<int> list)
    {
        unitAtk += list[0];
        unitSpAtk += list[0];
        unitDef += list[1];
        unitSpDef += list[1];
        unitSpd += list[2];
        unitAcc += list[3];
        if (list[4] == 1)
        {
            statusCondition = 1;
        }
        if (list[4] == 2)
        {
            statusCondition = 2;
        }
        if (list[4] == 3)
        {
            statusCondition = 3;
        }
        if (list[4] == 4)
        {
            statusCondition = 4;
        }
        statusEffect();
        Debug.Log("Stats Increased");
    }

    public void statusEffect()
    {
        if (statusCondition == 1)// burn
        {
            Debug.Log(unitName + " is burned!");
            int burnDamage = unitMaxHp / 10; // 10% of max HP
            // ^^ should be 1/10 of max HP but will be adjusted to whatever values we settle on
            takeStatusDamage(burnDamage);
        }
        else if (statusCondition == 2) // freeze
        {
            Debug.Log(unitName + " is frozen!");
            System.Random rand = new System.Random();
            int freezeChance = rand.Next(10); // 0-9
            if (freezeChance < 3) // 30% chance to thaw out
            {
                statusCondition = 0; // remove freeze
                Debug.Log(unitName + " has thawed out!");
            }
            else
            {

                Debug.Log(unitName + " is still frozen solid!");
            }


        }
        else if (statusCondition == 3) // paralyze
        {
            Debug.Log(unitName + " is paralyzed!");
        }
        else
        {
            // no status effect
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}