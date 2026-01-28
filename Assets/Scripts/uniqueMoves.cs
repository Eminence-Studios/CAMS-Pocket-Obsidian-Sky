using UnityEngine;
using System;
using System.Collections.Generic;

/*
    - Handles unique moves that units can use in battle by assigning each one a number that
    can be referenced in the prefabs we make for player and enemy units.
    - Each unique move will have different parameters that will be returned to the unit class
    which will account for the buffs and calculations like it does with the battleSystem class
    - Currently only three unique moves 
*/
public class uniqueMoves : MonoBehaviour
{
    public int moveNumber; // moves will be identified by numbers
    List<int> moveParameters = new List<int>(); // list to hold parameters for each unique move
    // the list of parameters will be structured as follows vv
    // [atk, def, spd, acc, status effect]
    public List<int> determineUniqueMove() // will return the list of parameters to be used in general buff/debuff procedure
    {
        unit unitComponent = GetComponent<unit>();
        if (moveNumber == 1) // sonic boom
        {
            moveParameters = new List<int> {1, 5, 2, 0, 0};
            Debug.Log(unitComponent.unitName + " used Sonic Boom! Defense increased by 5, Speed increased by 2!");
            return moveParameters;
        }
        if (moveNumber == 2) // thunderbolt
        {
            moveParameters = new List<int> {0, 2, 2, 2, 0};
            Debug.Log(unitComponent.unitName + " used Thunderbolt! Defense increased by 2, Speed increased by 2, Accuracy increased by 2!");
            return moveParameters;
        }
        if (moveNumber == 3) // wall of foliage
        {
            moveParameters = new List<int> {0, 2, 0, 0, 0};
            Debug.Log(unitComponent.unitName + " used Wall of Foliage! Defense increased by 2");
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
}
