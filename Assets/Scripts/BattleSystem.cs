using UnityEngine;
using System;
using System.Collections;
using System.Data.Common;
using System.Collections.Generic;
/*
    - Deals with the main fuctions of the battle mechaincs
    - Has functions for each of the four button actions the user can do 
        these functions then call the coroutines that handle the delays and actual logic
    - Is also responsible for the instiation of the player and enemy prefabs although I am 
    not exactly all that familiar with the code as I followed a youtube tutorial
    - Enemy turns are currently very basic and just do a physical attack every turn
        will likely be expanded upon later for more complex or even specific bosses/enemies
    - Most of the functions will use the unit class to handle calculations
    - What DOESN'T work/ in the works
        - Speed buffs MID-battle that affect turn order
        - Multiple calls of the button functions due to spam clicking buttons

*/
public enum BattleState
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    NEUTRAL,
    WON,
    LOST
}
public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    // player sprites ^^
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    // where the sprites will spawn (not currently being used)

    public BattleState state;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    Unit playerUnit;
    Unit enemyUnit;
    UniqueMoves playerMoveset;
    UniqueMoves enemyMoveset;
    public int turnCount = 1;
    // unit references that will be used across other scripts for stats 
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(setupBattle());

    }

    IEnumerator setupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        GameObject enemyGO = Instantiate(enemyPrefab);
        // reference to the player and enemy game objects that are being spawned 

        playerUnit = playerGO.GetComponent<Unit>();
        enemyUnit = enemyGO.GetComponent<Unit>();
        playerMoveset = playerGO.GetComponent<UniqueMoves>();
        enemyMoveset = enemyGO.GetComponent<UniqueMoves>();

        playerHUD.setHUD(playerUnit);
        enemyHUD.setHUD(enemyUnit);

        // speed deetermin function
        //state = BattleState.PLAYERTURN;
        //playerTurn();
        determineTurnOrder();

        yield return new WaitForSeconds(2f);
    }

    void determineTurnOrder()
    {
        if (playerUnit.unitSpd >= enemyUnit.unitSpd)
        {
            state = BattleState.PLAYERTURN;
            playerTurn();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(enemyTurn());
        }
    }

    void considerSpeedBuffs(Unit unit)
    {
        int Threshold = unit.unitSpd + unit.unitSpd / 2; // something similar to 1.5x their speed

        if (unit.unitSpd >= Threshold)
        {
            Debug.Log(unit.unitName + " is so fast they get an extra turn!");
            if (unit.playerName != null)
            {
                state = BattleState.PLAYERTURN;
                playerTurn();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(enemyTurn());
            }

        }
    }

    void playerTurn()
    {
        if (neutralTurn())
        {
            // do nothing
        }
        else
        {
            turnCount += 1;
            Debug.Log("What will you do?");
        }
    }

    public void onAttackButton()
    {
        bool attackStarted = false; // made so that mutliple clicks don't trigger delayed attacks
        if (state != BattleState.PLAYERTURN)
            return; // do nothing

        StartCoroutine(playerAttack(attackStarted));
        attackStarted = true;
    }

    public void onSpecialAttackButton()
    {
        bool attackStarted = false;
        if (state != BattleState.PLAYERTURN)
            return; // do nothing

        StartCoroutine(playerSpecialAttack(attackStarted));
        attackStarted = true;
    }

    public void onHealButton() // heal
    {
        bool attackStarted = false;
        if (state != BattleState.PLAYERTURN)
            return; // do nothing

        StartCoroutine(playerHealAttack(attackStarted));
        attackStarted = true;
    }

    public void onBuffButton() // buff/debuff
    {
        bool attackStarted = false;
        if (state != BattleState.PLAYERTURN)
            return; // do nothing

        StartCoroutine(playerUniqueMove(attackStarted));
        attackStarted = true;
    }
    IEnumerator playerAttack(bool attackStarted)
    {
        if (attackStarted)
            yield break; // exit the coroutine if attack has already started

        if (playerUnit.statusCondition == 2) // if frozen will skip turn
        {
            Debug.Log(playerUnit.unitName + " is frozen and cannot attack!");
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(enemyTurn());
            yield return new WaitForSeconds(2f);
            yield return null;
        }
        else // proceed with attack if not frozen
        {
            bool isDead = enemyUnit.takePhysicalDamage(playerUnit.unitAtk);

            bool success = enemyHUD.setHP(enemyUnit.unitHp);
            if (success)
            {
                Debug.Log("The attack is successful!");
            }
            else
            {
                Debug.Log("The attack couldn't break through the defense!");
            }
            yield return new WaitForSeconds(1f);
            // check if the enemy is dead
            if (isDead)
            {
                state = BattleState.WON;
                endBattle();
                yield return new WaitForSeconds(2f);
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(enemyTurn());
                yield return new WaitForSeconds(2f);
            }

            yield return null;
        }
    }

    IEnumerator playerSpecialAttack(bool attackStarted)
    {
        if (attackStarted)
            yield break; // exit the coroutine if attack has already started

        if (playerUnit.statusCondition == 2) // if frozen will skip turn
        {
            Debug.Log(playerUnit.unitName + " is frozen and cannot attack!");
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(enemyTurn());
            yield return new WaitForSeconds(2f);
            yield return null;
        }
        else // proceed with attack if not frozen
        {
            bool isDead = enemyUnit.takeSpecialDamage(playerUnit.unitSpAtk);
            bool success = enemyHUD.setHP(enemyUnit.unitHp);

            if (success)
            {
                Debug.Log("The special attack is successful!");
            }
            else
            {
                Debug.Log("The special attack couldn't break through the defense!");
            }
            yield return new WaitForSeconds(1f);
            // check if the enemy is dead
            if (isDead)
            {
                state = BattleState.WON;
                endBattle();
                yield return new WaitForSeconds(2f);
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(enemyTurn());
                yield return new WaitForSeconds(2f);
            }
            yield return null;
        }
    }

    IEnumerator playerHealAttack(bool attackStarted) // will be used as buff/debuff moves
    {
        if (attackStarted)
            yield break; // exit the coroutine if attack has already started
        playerUnit.healDamage(5); // 5 for now, should be specific later with full character kits
        playerHUD.setHP(playerUnit.unitHp);
        Debug.Log("You healed yourself!");

        state = BattleState.ENEMYTURN;
        StartCoroutine(enemyTurn());
        yield return new WaitForSeconds(2f);
    }

    IEnumerator playerUniqueMove(bool attackStarted)
    {
        if (attackStarted)
            yield break; // exit the coroutine if attack has already started

        List<int> buffParameters = playerMoveset.determineUniqueMove();
        playerUnit.buffStats(buffParameters);
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(enemyTurn());
        yield return new WaitForSeconds(2f);
    }
    void endBattle()
    {
        if (state == BattleState.WON)
        {
            Debug.Log("You won the battle!");
        }
        else if (state == BattleState.LOST)
        {
            Debug.Log("You were defeated.");
        }
    }

    IEnumerator enemyTurn()
    {
        if (neutralTurn())
        {
            // do nothing
        }
        else
        {
            turnCount += 1;
            Debug.Log("Enemy's turn!");

            yield return new WaitForSeconds(2f);

            bool isDead = playerUnit.takePhysicalDamage(enemyUnit.unitAtk);
            playerHUD.setHP(playerUnit.unitHp);

            if (isDead)
            {
                state = BattleState.LOST;
                endBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                playerTurn();
            }
        }
    }
    bool neutralTurn()
    {
        if (turnCount % 4 == 0)
        {
            turnCount += 1;
            determineTurnOrder();
            return true;
        }
        turnCount += 1;
        return false;
    }
}