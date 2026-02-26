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
        playerHUD.setEnergyHUD(playerUnit);
        enemyHUD.setEnergyHUD(enemyUnit);

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
            playerUnit.energy = 2;
            playerTurn();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            enemyUnit.energy = 2;
            StartCoroutine(basicEnemyTurn());
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
                StartCoroutine(basicEnemyTurn());
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
            playerUnit.energy +=1;
            Debug.Log("What will you do?");
        }
    }

    public void onAttackButton()
    {
        bool attackStarted = false; // made so that mutliple clicks don't trigger delayed attacks
        if (state != BattleState.PLAYERTURN)
            return; // do nothing

        if (playerHUD.setEnergy(1))
        {
            StartCoroutine(playerAttack(attackStarted));
            attackStarted = true;
        }
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
            StartCoroutine(basicEnemyTurn());
            yield return new WaitForSeconds(2f);
            yield return null;
        }
        else // proceed with attack if not frozen
        {
            bool isDead = enemyUnit.takeDamage(playerUnit.unitAtk, 0);

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
                StartCoroutine(basicEnemyTurn());
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
            StartCoroutine(basicEnemyTurn());
            yield return new WaitForSeconds(2f);
            yield return null;
        }
        else // proceed with attack if not frozen
        {
            bool isDead = enemyUnit.takeDamage(playerUnit.unitSpAtk, 1);
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
                StartCoroutine(basicEnemyTurn());
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
        StartCoroutine(basicEnemyTurn());
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
        StartCoroutine(basicEnemyTurn());
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

    IEnumerator basicEnemyTurn()
    {
        turnCount += 1;
        enemyUnit.energy += 1;
        if (neutralTurn())
        {
            // do nothing
        }
        else
        {
            Debug.Log("Enemy's turn!");

            yield return new WaitForSeconds(2f);

            bool isDead = playerUnit.takeDamage(enemyUnit.unitAtk, 0);
            if (playerUnit.unitHp == playerHUD.hpSlider.value)
            {
                Debug.Log("The attack couldn't break through the defense!");
            }
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

    IEnumerator bossEnemyTurn()
    {
        turnCount += 1;
        Debug.Log(enemyUnit.unitName + "'s Boss Turn");

        yield return new WaitForSeconds(2f);

        int choice = bossDecision(); // will be 0 if the boss decides not to
        bool isDead = playerUnit.takeDamage(choice,enemyUnit.specialization);
        if (playerUnit.unitHp == playerHUD.hpSlider.value && choice == 0)
        {
            // do nothing
            // skipping the buff parameters portion since the boss decided to skip
        }
        else if (playerUnit.unitHp == playerHUD.hpSlider.value && choice != 0)
        {
            // still goes through with the buffs despite no damage being done
            List<int> buffParameters = enemyMoveset.determineUniqueMove();
            enemyUnit.buffStats(buffParameters); // will proc the chance for it's special move to buff stats
            yield return new WaitForSeconds(2f);
            Debug.Log("The attack couldn't break through the defense!");
        
            playerHUD.setHP(playerUnit.unitHp);

        }
        else
        {
            List<int> buffParameters = enemyMoveset.determineUniqueMove();
            enemyUnit.buffStats(buffParameters); // will proc the chance for it's special move to buff stats
            yield return new WaitForSeconds(2f);
        
            playerHUD.setHP(playerUnit.unitHp);
        }

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
        // bossDecision() will return the specific atk or spAtk value depending on the boss's unique move
        
    }

    int bossDecision()
    {
        int decision = UnityEngine.Random.Range(1, 3);
        if (decision == 1)
        {
            if (enemyUnit.specialization == 0)
            {
                Debug.Log("Powerful Physical Attack");
                return enemyUnit.unitAtk;
            }
            else
            {
                Debug.Log("Powerful Magical Attack");
                return enemyUnit.unitSpAtk;
            }
        }
        else
        {   Debug.Log(enemyUnit.unitName + " decides not to attack");
            return 0; // boss will do no damage, essentially skipping turn
        }
    }
    bool neutralTurn()
    {
        if (turnCount % 4 == 0 || turnCount % 3 == 0)
        {
            StartCoroutine(bossEnemyTurn());
            return true;
        }
        else
        {
            return false;
        }
    }
}