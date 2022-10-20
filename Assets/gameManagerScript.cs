using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    public int round = 0;

    public GameObject canvasPrefab;
    public GameObject researchPrefab;
    public GameObject radioactivePrefab;
    public static gameManagerScript instance;
    public string[] roles;
    public Sprite[] roleImage;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public string getRoleDescription(string role)
    {
        string s = "";
        if (role.Equals("Medic"))
        {
            s = "Every 2 rounds will heal 1 spot completely.\nPassive: Will heal 2x the desired location.";
        }
        else if (role.Equals("Scientist"))
        {
            s = "Every 2 rounds you will get 1 research\nPassive: Research will only consume 1 turn instead of 2.";//researches 
        }
        else if (role.Equals("Quarantine Master"))
        {
            s = "Where you go no infection can trigger\nPassive: Viruses added on turn is reduced by half.";//researches 
        }
        else if (role.Equals("Builder"))
        {
            s = "Research stations can be upgraded\nPassive: Every 5 rounds will upgrade research station.";//researches 
        }
        return s;
    }
    public void onRoundEnd()
    {
        //add one to round counter text
        round++;
        int rand = Random.Range(0, waypointHolder.instance.waypoints.Count);
        waypointHolder.instance.waypoints[rand].GetComponent<waypointScript>().Infect();
        print("infected" + waypointHolder.instance.waypoints[rand].name);
    }
    //research will consume 2 turns
}
