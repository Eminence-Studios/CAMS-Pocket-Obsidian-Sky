using System;
using UnityEngine;
using UnityEngine.UI;
/* 
    Doesn't do much but act as a script for the HP sliders to work in unity
*/
public class BattleHUD : MonoBehaviour
{
    public Slider hpSlider;
    public Slider energySlider;
    public void setHUD(Unit unit)
    {
        hpSlider.maxValue = unit.unitMaxHp;
        hpSlider.value = unit.unitMaxHp; // should be their max, might change later
    }

    public bool setHP(int hp)
    {
        if (hpSlider.value != hp) // matches the calculated changed hp to the visual slider value
        {
            hpSlider.value = hp;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setEnergyHUD(Unit unit) // serves to initialize and just update the visual
    {
        energySlider.maxValue = unit.maxEnergy;
        energySlider.value = unit.energy;
    }

    public bool setEnergy(int energy)
    {
        if (energy <= energySlider.value)
        {
            energySlider.value -= energy;
            return true;
        }
        else
        {
            Debug.Log("There is not enough energy to perform this move");
            return false;
        }
    }

        public bool setEnergy(int energy, Unit unit)
    {
        if (energy <= unit.energy)
        {
            unit.energy -= energy;
            energySlider.value = unit.energy;
            return true;
        }
        else
        {
            Debug.Log("There is not enough energy to perform this move");
            return false;
        }
    }
}