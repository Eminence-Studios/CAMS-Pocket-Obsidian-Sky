using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Rendering.Universal.Internal;

/*
    - Handles unique moves that units can use in battle by assigning each one a number that
    can be referenced in the prefabs we make for player and enemy units.
    - Each unique move will have different parameters that will be returned to the unit class
    which will account for the buffs and calculations like it does with the battleSystem class
    - Currently only three unique moves 
*/
public class UniqueMoves : MonoBehaviour
{
    public int moveNumber; // moves will be identified by numbers
    public int basicMoveNumber; // will be used to store the original move number for the unit so that it can be restored after using a unique move
    public int moveBeingUsed = 6; // Default to Guard move (safe default)
    List<int> moveParameters = new List<int>(); // list to hold parameters for each unique move
    // the list of parameters will be structured as follows vv
    // [atk, def, spd, acc, status effect]

    public void basicUniqueMove(int tempMoveNum) // is for the bosses to swtich in between boss turns and normal turns 
    {
        moveBeingUsed = moveNumber;
    }
    public List<int> determineUniqueMove() // will return the list of parameters to be used in general buff/debuff procedure
    {
        Unit moveType = GetComponent<Unit>();
        Unit unitComponent = GetComponent<Unit>();
        if (moveBeingUsed == 1) // sonic boom
        {
            moveParameters = new List<int> { 0, 5, 2, 0, 0 };
            Debug.Log(unitComponent.unitName + " used Sonic Boom! Chance to increase defense by 5 and speed by 2!");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 2) // thunderbolt
        {
            moveParameters = new List<int> { 0, 2, 2, 2, 0 };
            Debug.Log(unitComponent.unitName + " used Thunderbolt! Chance to increase defense by 2, speed by 2, and accuracy by 2!");
            return moveParameters;
        }
        if (moveBeingUsed == 3) // wall of foliage
        {
            moveParameters = new List<int> { 0, 2, 0, 0, 0 };
            Debug.Log(unitComponent.unitName + " used Wall of Foliage! Chance to increase defense by 2");
            return moveParameters;
        }
        if (moveBeingUsed == 4) // flamethrower
        {
            moveParameters = new List<int> { 2, 0, 0, 0, 1 }; // +2 atk, inflicts burn status effect
            Debug.Log(unitComponent.unitName + " used Flamethrower! Attack may increase by 2");
            return moveParameters;
        }
        if (moveBeingUsed == 5) // slam dunk
        {
            moveParameters = new List<int> { 1, 0, 0, 0, 0 }; // inflicts paralysis status effect
            Debug.Log(unitComponent.unitName + " used Slam Dunk! Chance to increase attack by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 6) // guard
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0 }; // +1 def
            Debug.Log(unitComponent.unitName + " used Guard! Chance to increase defense by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        else
        {
            // Default fallback: Guard move (safe default with +1 def)
            Debug.LogWarning("Unknown moveBeingUsed value: " + moveBeingUsed + ". Defaulting to Guard.");
            moveParameters = new List<int> { 0, 1, 0, 0, 0 };
            return moveParameters;
        }
        // else if (moveNumber == 2)
        // {

        //  }
    }

    public void changeUniqueMove(int newMoveNumber) // will be called by the button methods
    {
        Unit unitSpecialization = GetComponent<Unit>();
        UniqueMoves unitComponent = GetComponent<Unit>().GetComponent<UniqueMoves>();
        moveBeingUsed = newMoveNumber;
    }

    public void changeUniqueMove(bool isBossTurn) // overloaded method for bosses to switch between normal and boss turns
    {
        Unit unitSpecialization = GetComponent<Unit>();
        UniqueMoves unitComponent = GetComponent<Unit>().GetComponent<UniqueMoves>();  
        if (isBossTurn)
        {
            moveBeingUsed = unitComponent.moveNumber; // will use main move on boss turns
        }
        else
        {
            moveBeingUsed = basicMoveNumber; // will switch back to the original move on normal turns
        }
    }
}