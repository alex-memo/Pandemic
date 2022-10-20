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
            s = "Every 4 rounds will heal 1 spot completely.\nPassive: Will heal 2x the desired location.";
        }
        else if (role.Equals("Scientist"))
        {
            s = "Every 4 rounds you will get 1 research\nPassive: Research can be done anywhere in the map.";//researches 
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
    public string getRole()
    {
        int rand = Random.Range(0, roles.Length);
        return roles[rand];
    }
    public void onRoundEnd()
    {
        //add one to round counter text
        round++;
        addDiseaseRoundEnd();
        addDiseaseRoundEnd();
    }
    private void addDiseaseRoundEnd()
    {
        int rand = Random.Range(0, waypointHolder.instance.waypoints.Count);
        int infectionNumber;
        if (round < 5)//if round is less than 5 only give one infection
        {
            infectionNumber = 1;
        }
        else if (round < 10)//gives 2 infection
        {
            infectionNumber = 2;
        }
        else//round is above 10
        {
            infectionNumber = 3;//gives 3 infection
        }

        waypointHolder.instance.waypoints[rand].GetComponent<waypointScript>().Infect(infectionNumber);
        print("infected" + waypointHolder.instance.waypoints[rand].name);

    }
    //research will consume 2 turns
}
