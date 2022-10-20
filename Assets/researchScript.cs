using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchScript : MonoBehaviour
{
    private int researchCost = 10;
    private int cooldown = 0;
    private int cureLvl=0;
    public void onCureResearch()
    {
        if (Controller.instance.researchPoints >= researchCost&&cureLvl<6)
        {
            Controller.instance.addCureChance(10);
            cureLvl += 1;
        }
    }
    public int getCooldown()
    {
        return cooldown;
    }
}
