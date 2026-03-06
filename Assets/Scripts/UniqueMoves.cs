using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

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
    List<int> moveParameters = new List<int>(); // list to hold parameters for each unique move
    // the list of parameters will be structured as follows vv
    // [atk, def, spd, acc, status effect]
    public List<int> determineUniqueMove() // will return the list of parameters to be used in general buff/debuff procedure
    {
        Unit unitComponent = GetComponent<Unit>();
        if (moveNumber == 1) // sonic boom
        {
            moveParameters = new List<int> { 0, 5, 2, 0, 0 };
            Debug.Log(unitComponent.unitName + " used Sonic Boom! Chance to increase defense by 5 and speed by 2!");
            return moveParameters;
        }
        if (moveNumber == 2) // thunderbolt
        {
            moveParameters = new List<int> { 0, 2, 2, 2, 0 };
            Debug.Log(unitComponent.unitName + " used Thunderbolt! Chance to increase defense by 2, speed by 2, and accuracy by 2!");
            return moveParameters;
        }
        if (moveNumber == 3) // wall of foliage
        {
            moveParameters = new List<int> { 0, 2, 0, 0, 0 };
            Debug.Log(unitComponent.unitName + " used Wall of Foliage! Chance to increase defense by 2");
            return moveParameters;
        }
        if (moveNumber == 4) // flamethrower
        {
            moveParameters = new List<int> { 2, 0, 0, 0, 1 }; // +2 atk, inflicts burn status effect
            Debug.Log(unitComponent.unitName + " used Flamethrower! Attack may increase by 2");
            return moveParameters;
        }
        else
        {
            return null; // placeholder for now
        }
        // else if (moveNumber == 2)
        // {

        //  }
    }

    public void changeUniqueMove(int newMoveNumber) // will be called by the button methods
    {
        
        UniqueMoves unitComponent = GetComponent<Unit>().GetComponent<UniqueMoves>();
        unitComponent.moveNumber = newMoveNumber;
    }
}