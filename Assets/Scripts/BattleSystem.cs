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
    public static BattleSystem instance;

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
        GameObject playerGO = playerPrefab;
        GameObject enemyGO = Instantiate(enemyPrefab);
        // reference to the player and enemy game objects that are being spawned 

        playerUnit = playerGO.GetComponent<Unit>();
        enemyUnit = enemyGO.GetComponent<Unit>();
        playerMoveset = playerPrefab.GetComponent<UniqueMoves>();
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
            NextTurn();
            enemyUnit.energy = 2;
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
        // even though the player will get the +1 energy, for now this punishes exhausting all energy
        if (playerUnit.energy <= 0)
        {
            Debug.Log("No energy available – skipping player's turn."); 
            playerUnit.energy += 1; // still recharge for next cycle
            playerHUD.setEnergyHUD(playerUnit);

            NextTurn();
            return;
        }

        playerUnit.energy += 1;
        playerHUD.setEnergyHUD(playerUnit);
        Debug.Log("What will you do?");
    }

    public void onFirstButton()
    {
        bool attackStarted = false; // made so that mutliple clicks don't trigger delayed attacks
        if (state != BattleState.PLAYERTURN)
            return; // do nothing

        if (playerHUD.setEnergy(1, playerUnit)) // checks if the player has enough energy to perform the move, if so it will also update the energy slider and player energy
        {
            StartCoroutine(playerFirstAttack(attackStarted));
            attackStarted = true;
        }
        else
        {
            attackStarted = true;
            StartCoroutine(playerFirstAttack(attackStarted));
        }
    }

    public void onSecondButton()
    {
        bool attackStarted = false;
        if (state != BattleState.PLAYERTURN)
            return; // do nothing

        if (playerHUD.setEnergy(2, playerUnit))
        {
            StartCoroutine(playerSecondAttack(attackStarted));
            attackStarted = true;
        }
        else
        {
            attackStarted = true;
            StartCoroutine(playerSecondAttack(attackStarted));
        }
    }

    public void onThirdButton() // designated to third button for 3 energy moves
    {
        bool attackStarted = false;
        if (state != BattleState.PLAYERTURN)
            return; // do nothing

        if (playerHUD.setEnergy(3, playerUnit))
        {
            StartCoroutine(playerThirdAttack(attackStarted));
            attackStarted = true;
        }
        else
        {
            attackStarted = true;
            StartCoroutine(playerThirdAttack(attackStarted));
        }
    }

    public void onFourthButton() // fourth button: unique move (buff + attack)
    {
        bool attackStarted = false;
        if (state != BattleState.PLAYERTURN)
            return; // do nothing

        // cost is 4 energy
        if (playerHUD.setEnergy(4, playerUnit))
        {
            StartCoroutine(playerFourthAttack(attackStarted));
            attackStarted = true;
        }
        else
        {
            attackStarted = true;
            StartCoroutine(playerFourthAttack(attackStarted));
        }
    }

    public void onSkipButton()
    {
        if (state != BattleState.PLAYERTURN)
            return; // do nothing

        StartCoroutine(playerSkipTurn());
    }
    IEnumerator playerFirstAttack(bool attackStarted)
    {
        if (attackStarted)
            yield break; // exit the coroutine if attack has already started

        // skip turn if player is frozen
        if (playerUnit.statusCondition == 2)
        {
            Debug.Log(playerUnit.unitName + " is frozen and cannot act!");
            yield return new WaitForSeconds(2f);
            NextTurn();
            yield return new WaitForSeconds(2f);
            yield return null;
        }
        else
        {
            // determine damage based on specialization field (0 = physical, 1 = special)
            int damage;
            if (playerUnit.specialization == 0)
            {
                damage = playerUnit.unitAtk;
                Debug.Log("Performing first move as physical attack"); //temp
            }
            else
            {
                damage = playerUnit.unitSpAtk;
                Debug.Log("Performing first move as special attack");//temp
            }

            bool isDead = enemyUnit.takeDamage(damage, playerUnit.specialization);
            bool success = enemyHUD.setHP(enemyUnit.unitHp);
            
            List<int> buffParameters = playerMoveset.determineUniqueMove();
            Debug.Log(playerMoveset.moveBeingUsed);
            if (buffParameters != null)
            {
                playerUnit.buffStats(buffParameters);
            }
            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                endBattle();
                yield return new WaitForSeconds(2f);
            }
            else
            {
                NextTurn();
                yield return new WaitForSeconds(2f);
            }

            yield return null;
        }
    }

    IEnumerator playerSecondAttack(bool attackStarted)
    {
        if (attackStarted)
            yield break; // exit the coroutine if attack has already started

        // skip turn if player is frozen
        if (playerUnit.statusCondition == 2)
        {
            Debug.Log(playerUnit.unitName + " is frozen and cannot act!");
            yield return new WaitForSeconds(2f);
            NextTurn();
            yield return new WaitForSeconds(2f);
            yield return null;
        }
        else
        {
            // determine damage based on specialization field (0 = physical, 1 = special)
            int damage;
            if (playerUnit.specialization == 0)
            {
                damage = playerUnit.unitAtk;
                Debug.Log("Performing second move as physical attack"); //temp
            }
            else
            {
                damage = playerUnit.unitSpAtk;
                Debug.Log("Performing second move as special attack");//temp
            }

            bool isDead = enemyUnit.takeDamage(damage, playerUnit.specialization);
            bool success = enemyHUD.setHP(enemyUnit.unitHp);
            
            List<int> buffParameters = playerMoveset.determineUniqueMove();
            Debug.Log(playerMoveset.moveBeingUsed);
            if (buffParameters != null)
            {
                playerUnit.buffStats(buffParameters);
            }
            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                endBattle();
                yield return new WaitForSeconds(2f);
            }
            else
            {
                NextTurn();
                yield return new WaitForSeconds(2f);
            }

            yield return null;
        }
    }

    IEnumerator playerThirdAttack(bool attackStarted) // a third attack whose type is determined by the player's specialization
    {
        if (attackStarted)
            yield break; // exit the coroutine if attack has already started

        // skip turn if player is frozen
        if (playerUnit.statusCondition == 2)
        {
            Debug.Log(playerUnit.unitName + " is frozen and cannot act!");
            yield return new WaitForSeconds(2f);
            NextTurn();
            yield return new WaitForSeconds(2f);
            yield return null;
        }
        else
        {
            // determine damage based on specialization field (0 = physical, 1 = special)
            int damage;
            if (playerUnit.specialization == 0)
            {
                damage = playerUnit.unitAtk;
                Debug.Log("Performing third move as physical attack"); //temp
            }
            else
            {
                damage = playerUnit.unitSpAtk;
                Debug.Log("Performing third move as special attack");//temp
            }

            bool isDead = enemyUnit.takeDamage(damage, playerUnit.specialization);
            bool success = enemyHUD.setHP(enemyUnit.unitHp);
            
            List<int> buffParameters = playerMoveset.determineUniqueMove();
            Debug.Log(playerMoveset.moveBeingUsed);
            if (buffParameters != null)
            {
                playerUnit.buffStats(buffParameters);
            }
            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                endBattle();
                yield return new WaitForSeconds(2f);
            }
            else
            {
                NextTurn();
                yield return new WaitForSeconds(2f);
            }

            yield return null;
        }
    }

    IEnumerator playerFourthAttack(bool attackStarted)
    {
        if (attackStarted)
            yield break; // exit the coroutine if attack has already started

        // skip turn if player is frozen
        if (playerUnit.statusCondition == 2)
        {
            Debug.Log(playerUnit.unitName + " is frozen and cannot act!");
            yield return new WaitForSeconds(2f);
            NextTurn();
            yield return new WaitForSeconds(2f);
            yield return null;
        }
        else
        {
            // determine damage based on specialization field (0 = physical, 1 = special)
            int damage;
            if (playerUnit.specialization == 0)
            {
                damage = playerUnit.unitAtk;
                Debug.Log("Performing fourth move as physical attack"); //temp
            }
            else
            {
                damage = playerUnit.unitSpAtk;
                Debug.Log("Performing fourth move as special attack");//temp
            }

            bool isDead = enemyUnit.takeDamage(damage, playerUnit.specialization);
            bool success = enemyHUD.setHP(enemyUnit.unitHp);
            
            List<int> buffParameters = playerMoveset.determineUniqueMove();
            Debug.Log(playerMoveset.moveBeingUsed);
            if (buffParameters != null)
            {
                playerUnit.buffStats(buffParameters);
            }
            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                endBattle();
                yield return new WaitForSeconds(2f);
            }
            else
            {
                NextTurn();
                yield return new WaitForSeconds(2f);
            }

            yield return null;
        }
    }

    IEnumerator playerSkipTurn()
    {
        Debug.Log("Player skips turn to preserve and gain energy.");
        yield return new WaitForSeconds(2f);

        // Gain additional energy for skipping (in addition to the +1 from playerTurn)
        playerUnit.energy += 1;
        playerHUD.setEnergyHUD(playerUnit);

        NextTurn();
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
        state = BattleState.ENEMYTURN;
        enemyUnit.energy += 1;

        Debug.Log("Enemy's turn!");

        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.takeDamage(enemyUnit.unitAtk, enemyUnit.specialization);
        enemyMoveset.changeUniqueMove(false);
        List<int> buffParameters = enemyMoveset.determineUniqueMove();
        if (buffParameters != null)
        {
            enemyUnit.buffStats(buffParameters); // will proc the chance for it's special move to buff stats
        }

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

    IEnumerator bossEnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        Debug.Log(enemyUnit.unitName + "'s Boss Turn");

        yield return new WaitForSeconds(2f);

        int choice = bossDecision(); // will be 0 if the boss decides not to
        Debug.Log(choice);
        bool isDead = false;
        if (choice != 0)
        {
            isDead = playerUnit.takeDamage(choice,enemyUnit.specialization);
            // player shouldn't take damage otherwise if choice IS zero
        }

        if ((playerUnit.unitHp == playerHUD.hpSlider.value) && (choice == 0))
        {
            // do nothing
            // skipping the buff parameters portion since the boss decided to skip
        }
        else if ((playerUnit.unitHp == playerHUD.hpSlider.value) && (choice != 0))
        {
            // still goes through with the buffs despite no damage being done
            enemyMoveset.changeUniqueMove(true);
            List<int> buffParameters = enemyMoveset.determineUniqueMove();
            if (buffParameters != null)
            {
                enemyUnit.buffStats(buffParameters); // will proc the chance for it's special move to buff stats
            }
            yield return new WaitForSeconds(2f);
            Debug.Log("The attack couldn't break through the defense!");
        
            playerHUD.setHP(playerUnit.unitHp);

        }
        else
        {
            enemyMoveset.changeUniqueMove(true);
            List<int> buffParameters = enemyMoveset.determineUniqueMove();
            if (buffParameters != null)
            {
                enemyUnit.buffStats(buffParameters); // will proc the chance for it's special move to buff stats
            }
            yield return new WaitForSeconds(2f);
        
            playerHUD.setHP(playerUnit.unitHp);
        }
        if (enemyUnit.statusCondition == 4)
        {
            playerUnit.buffStatsNoChance(new List<int>{0, -99, 0, 0, 0});
            enemyUnit.statusCondition = 0;
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
        int decision = UnityEngine.Random.Range(1, 11);
        if (decision < 8) 
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
    void NextTurn()
    {
        turnCount++;
        // Boss/neutral turn check FIRST
        if (CheckNeutralTurn())
            return;

        if (state == BattleState.PLAYERTURN)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(basicEnemyTurn());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            playerTurn();
        }
    }
    bool CheckNeutralTurn()
    {
        if ((turnCount % 6 == 0) || (turnCount % 8 == 0))
        {
            state = BattleState.ENEMYTURN;
            if (enemyUnit.statusCondition == 4)
            {
                playerUnit.buffStatsNoChance(new List<int>{0, -99, 0, 0, 0});
            }
            StartCoroutine(bossEnemyTurn());
            return true;
        }
        else
        {
            state = BattleState.ENEMYTURN;
            if (enemyUnit.statusCondition == 4)
            {
                playerUnit.buffStatsNoChance(new List<int>{0, -99, 0, 0, 0});
            }
            StartCoroutine(basicEnemyTurn());
            return true;
        }
    }
}