using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Scriptable Objects/Spell")]
public class Spell : ScriptableObject
{
    // public Boolean isSpell;
    public string spellName;
    public int spellNumber;
    public string spellDescription;
    public Sprite spellIcon;
    public enum spellType { Attack, Defense, Healing, Energy }
    public spellType chosenType;
    public int damage;


}
