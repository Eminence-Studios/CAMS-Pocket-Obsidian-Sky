using System;
using UnityEngine;
using UnityEngine.UI;
/* 
    Doesn't do much but act as a script for the HP sliders to work in unity
*/
public class BattleHUD : MonoBehaviour
{
    public Slider hpSlider;
    public void setHUD(Unit unit)
    {
        hpSlider.maxValue = 10;
        hpSlider.value = unit.unitHp; // should be their max, might change later
    }

    public bool setHP(int hp)
    {
        if (hpSlider.value != hp)
        {
            hpSlider.value = hp;
            return true;
        }
        else
        {
            return false;
        }
    }

}
