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
    
    private void ReportMove(string message)
    {
        if (BattleSystem.instance != null)
            BattleSystem.instance.UpdateConsoleText(message);
        else
            Debug.Log(message);
    }
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
        if (moveBeingUsed == 1) // Punch
        {
            moveParameters = new List<int> { 0, 0, 0, 0, 0 };
            ReportMove(unitComponent.unitName + " used Punch!");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 2) // Rock Throw
        {
            moveParameters = new List<int> { 0, 0, 0, 0, 0 };
            ReportMove(unitComponent.unitName + " used Rock Throw!");
            return moveParameters;
        }
        if (moveBeingUsed == 3) // Guard
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0 }; //+1 def
            ReportMove(unitComponent.unitName + " used Guard! Defense may increase by 1");
            return moveParameters;
        }
        if (moveBeingUsed == 4) // Dirt Wall
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; // +1 def
            ReportMove(unitComponent.unitName + " used Dirt Wall! Defense may increase by 1");
            return moveParameters;
        }
        if (moveBeingUsed == 5) //flaming shuriken
        {
            moveParameters = new List<int> { 1, 0, 0, 0, 0}; //+1 atk
            ReportMove(unitComponent.unitName + " used Flaming Shuriken! Attack may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 6) //ankle breaker
        {
            moveParameters = new List<int> { 0, 0, 1, 0, 0}; //+1 speed, DODGE
            ReportMove(unitComponent.unitName + " used Ankle Breaker! Speed may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 7) //coin toss
        {
            moveParameters = new List<int> { -1, 0, 0, 0, 0}; //-1 atk
            ReportMove(unitComponent.unitName + " used Coin Toss! Attack may decrease by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 8) //printed shield
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; //+1 def
            ReportMove(unitComponent.unitName + " used Printed Shield! Defense may increase by 1");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 9) //frisbee
        {
            moveParameters = new List<int> { -1, 0, 0, 0, 0}; //-1 atk
            ReportMove(unitComponent.unitName + " used Frisbee! Attack may decrease by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 10) //wall of foliage
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; //+1 def
            ReportMove(unitComponent.unitName + " used Wall of Foliage! Defense may increase by 1");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 11) //flame thrower
        {
            moveParameters = new List<int> { 3, 0, 0, 0, 0}; //+3 atk
            ReportMove(unitComponent.unitName + " used Flame Thrower! Attack may increase by 3");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 12) //scales
        {
            moveParameters = new List<int> { 0, 2, 0, 0, 0}; //+2 def
            ReportMove(unitComponent.unitName + " used Scales! Defense may increase by 2");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 13) //sweet melodies
        {
            moveParameters = new List<int> { 2, 0, 0, 0, 0}; //+2 atk
            ReportMove(unitComponent.unitName + " used Sweet Melodies! Attack may increase by 2");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 14) //Lullaby
        {
            moveParameters = new List<int> { 0, 3, 0, 0, 0}; //+3 def 
            ReportMove(unitComponent.unitName + " used Lullaby! Defense may increase by 3");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 15) //fiery strike
        {
            moveParameters = new List<int> { 2, 0, 0, 0, 0}; //+2 atk
            ReportMove(unitComponent.unitName + " used Fiery Strike! Attack may increase by 2");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 16) //thermoblast
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; //+1 def 
            ReportMove(unitComponent.unitName + " used Thermoblast! Defense may increase by 1");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 17) //stempede
        {
            moveParameters = new List<int> { 2, -1, 0, 0, 0}; //+2 atk -1 def
            ReportMove(unitComponent.unitName + " used Stampede! Attack may increase by 2, decrease def by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 18) //herd behavior
        {
            moveParameters = new List<int> { 0, 2, 0, 0, 0}; //+2 def
            ReportMove(unitComponent.unitName + " used Herd Behavior! Defense may increase by 2");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 19) //whistling wind now earthen core
        {
            moveParameters = new List<int> { 1, 1, 1, 1, 1}; //+1 everything
            ReportMove(unitComponent.unitName + " used Earthen Core! Every stat may increase by 1");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 20) //bamboo shoot
        {
            moveParameters = new List<int> { 1, 0, 0, 0, 0}; //+1 atk
            ReportMove(unitComponent.unitName + " used Bamboo Shoot! Attack may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 21) //parabolic shield
        {
            moveParameters = new List<int> { 0, 2, 0, 0, 0}; //+2 def
            ReportMove(unitComponent.unitName + " used Parabolic Shield! Defense may increase by 2");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 22) //tsunami
        {
            moveParameters = new List<int> { 2, 0, 0, 0, 0}; //+2 atk
            ReportMove(unitComponent.unitName + " used Tsunami! Attack may increase by 2");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 23) //hypnosis
        {
            moveParameters = new List<int> { 0, 2, 0, 0, 0}; //+2 def
            ReportMove(unitComponent.unitName + " used Hypnosis! Defense may increase by 2");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 24) //chance of the cell
        {
            moveParameters = new List<int> { 1, 1, 1, 1, 1}; //+1 everything
            ReportMove(unitComponent.unitName + " used Chance of the Cell! Every stat may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 25) //herb ball
        {
            moveParameters = new List<int> { 2, 2, 2, 2, 2}; //+2 everything
            ReportMove(unitComponent.unitName + " used Herb Ball! Every stat may increase by 2");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 26) //tempest
        {
            moveParameters = new List<int> { 2, 0, 0, 0, 0}; //+2 atk
            ReportMove(unitComponent.unitName + " used Tempest! Attack may increase by 2");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 27) //dust storm
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; //+1 def
            ReportMove(unitComponent.unitName + " used Dust Storm! Defense may increase by 1");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 28) //octoslap
        {
            moveParameters = new List<int> { 1, 0, 0, 0, 0}; //+1 atk
            ReportMove(unitComponent.unitName + " used Octoslap! Attack may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 29) //camouflage
        {
            moveParameters = new List<int> { 0, 2, 0, 0, 0}; //+2 def
            ReportMove(unitComponent.unitName + " used Camouflage! Defense may increase by 2");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 30) //dad jokes
        {
            moveParameters = new List<int> { 1, 0, 0, 0, 0}; //+1 atk
            ReportMove(unitComponent.unitName + " used Dad Jokes! Attack may increase by 1");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 31) //landslide
        {
            moveParameters = new List<int> { 0, 2, 0, 0, 0}; //+2 def
            ReportMove(unitComponent.unitName + " used Landslide! Defense may increase by 2");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 32) //aqua beam
        {
            moveParameters = new List<int> { 1, 0, 0, 0, 0}; //+1 atk
            ReportMove(unitComponent.unitName + " used Aqua Beam! Attack may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 33) //bubble barrier
        {
            moveParameters = new List<int> { 0, 2, 0, 0, 0}; //+2 def
            ReportMove(unitComponent.unitName + " used ! Defense may increase by 2");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 34) //circuit shock
        {
            moveParameters = new List<int> { -1, 0, 0, 0, 0}; //-1 atk
            ReportMove(unitComponent.unitName + " used Circuit Shock! Attack may decrease by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 35) //plasmic barrier
        {
            moveParameters = new List<int> { 0, 2, 0, 0, 0}; //+2 def
            ReportMove(unitComponent.unitName + " used Plasmic Barrier! Defense may increase by 2");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 36) //solar beam
        {
            moveParameters = new List<int> { -1, 0, 0, 0, 0}; //-1 atk
            ReportMove(unitComponent.unitName + " used Solar Beam! Attack may decrease by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 37) //vineyard
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; //+1 def
            ReportMove(unitComponent.unitName + " used ! Defense may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 38) //roll of the dice
        {
            int roll = UnityEngine.Random.Range(1,3);
            if (roll == 1)
            {
                moveParameters = new List<int> { -6, -6, -6, -6, 0}; //
                ReportMove(unitComponent.unitName + " used Roll of the Dice! All your stats were lowered by 6!");
                moveType.specialization = 1;
                return moveParameters;
            }
            else
            {
                moveParameters = new List<int> { 6, 6, 6, 6, 0}; //
                ReportMove(unitComponent.unitName + " used Roll of the Dice! All your stats were raised by 6!");
                moveType.specialization = 1;
                return moveParameters;
            }
        }
        if (moveBeingUsed == 39) //ice wall
        {
            moveParameters = new List<int> { 0, 0, 0, 0, 4}; //shield
            ReportMove(unitComponent.unitName + " used Ice Armor!");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 40) //eroding blade
        {
            moveParameters = new List<int> { -1, 0, 0, 0, 0}; //-1 atk
            ReportMove(unitComponent.unitName + " used Eroded Blade! Attack may decrease by 1");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 41) //web trap
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; //+1 def
            ReportMove(unitComponent.unitName + " used Web Trap! Defense may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 42) //ionized beam
        {
            moveParameters = new List<int> { -1, 0, 0, 0, 0}; //-1 atk
            ReportMove(unitComponent.unitName + " used Ionized Beam! Attack may decrease by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 43) //dig
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; //+1 def
            ReportMove(unitComponent.unitName + " used Dig! Defense may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 44) //slug
        {
            moveParameters = new List<int> { 1, 0, 0, 0, 0}; //+1 atk
            ReportMove(unitComponent.unitName + " used Slug! Attack may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 45) //Syrupy sweet
        {
            moveParameters = new List<int> { 0, -1, 0, 0, 0}; //-1 def
            ReportMove(unitComponent.unitName + " used Syrupy Sweet! Defense may decrease by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 46) //tusk tackle
        {
            moveParameters = new List<int> { 1, 0, 0, 0, 0}; //+1 atk
            ReportMove(unitComponent.unitName + " used Tusk Tackle! Attack may increase by 1");
            moveType.specialization = 1;
            return moveParameters;
        }
        if (moveBeingUsed == 47) //venom hide
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; //+1 def
            ReportMove(unitComponent.unitName + " used Venom Hide! Defense may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 48) //tidal flip
        {
            moveParameters = new List<int> { 2, 0, 0, 0, 0}; //+2 atk
            ReportMove(unitComponent.unitName + " used Tidal Flip! Attack may increase by 2");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 49) //ocean calm
        {
            moveParameters = new List<int> { 0, 1, 0, 1, 0}; //+1 def, +1 acc
            ReportMove(unitComponent.unitName + "used Ocean Calm! Defense may increase by 1, Accuracy may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 50) //light wave
        {
            moveParameters = new List<int> { 1, 0, 0, 0, 0}; //+1 atk
            ReportMove(unitComponent.unitName + " used Light Wave! Attack may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 51) //photon surf
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; //+1 def
            ReportMove(unitComponent.unitName + " used Photon Surf! Defense may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 52) //seismic roll
        {
            moveParameters = new List<int> { 1, 0, 1, 0, 0}; //+1 atk, +1 spd
            ReportMove(unitComponent.unitName + " used Seismic Roll! Attack may increase by 1, Speed may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 53) //stone focus
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; //+1 def
            ReportMove(unitComponent.unitName + " used Stone Focus! Defense may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 54) //lightning dash
        {
            moveParameters = new List<int> { 1, 0, 1, 0, 0}; //+1 spd, atk
            ReportMove(unitComponent.unitName + " used Lightning Dash! Attack may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 55) //static shield
        {
            moveParameters = new List<int> { 0, 1, 0, 0, 0}; //+1 def
            ReportMove(unitComponent.unitName + " used Static Shield! Defense may increase by 1");
            moveType.specialization = 0;
            return moveParameters;
        }
        if (moveBeingUsed == 56) //headbutt
        {
            moveParameters = new List<int> { 2, -1, 0, 0, 0 }; //+2 atk, -1 def
            ReportMove(unitComponent.unitName + " used Headbutt! Attack may increase by 2, Defense may decrease by 1");
            moveType.specialization = 0;
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
        Unit unitSpecialization = GetComponent<Unit>();
        UniqueMoves unitComponent = GetComponent<Unit>().GetComponent<UniqueMoves>();
        moveBeingUsed = newMoveNumber;
    }

    public void changeSpecialization(int newSpecialization) // will be called by the button methods to change the unit's specialization
    {
        Unit unitSpecialization = GetComponent<Unit>();
        unitSpecialization.specialization = newSpecialization;
    }
    public void changeUniqueMove(bool isBossTurn) // overloaded method for bosses to switch between normal and boss turns
    {
        Unit unitSpecialization = GetComponent<Unit>();
        UniqueMoves unitComponent = GetComponent<Unit>().GetComponent<UniqueMoves>();  
        if (isBossTurn)
        {
            moveBeingUsed = moveNumber; // will use main move on boss turns
        }
        else
        {
            moveBeingUsed = basicMoveNumber; // will switch back to the original move on normal turns
        }
    }
}