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

    public GameObject CloudPrefab;
    private GameObject cloud;
    /**
 * @memo 2022
 * Creates instence of this script
 */
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    /**
     * @memo 2022
     * gets the description of the selected role
     */
    public string getRoleDescription(string role)
    {
        string s = "";
        if (role.Equals("Medic"))
        {
            s = "Every round get 1 extra research point.\nPassive: Will heal 2x the desired location.";
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
    /**
 * @memo 2022
 * gets a random role from the list
 */
    public string getRole()
    {
        int rand = Random.Range(0, roles.Length);
        Controller.instance.roleImage.sprite = roleImage[rand];
        return roles[rand];
    }
    public void onRoundEnd()
    {
        //add one to round counter text
        round++;
        addDiseaseRoundEnd();
        addDiseaseRoundEnd();
        if (Controller.instance.getCurrentRole().Equals("Scientist"))
        {
            if (round % 4 == 0)
            {
                Controller.instance.researchPoints++;
            }
        }
        //waypointHolder.instance.onRoundEnd();
    }
    /**
 * @memo 2022
 * Start method, genereates 2 diseases in a random position
 */
    private void Start()
    {
        addSetDisease(2);
    }
    /**
 * @memo 2022
 * adds disease on round end
 */
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
    /**
 * @memo 2022
 * adds a set asmount of diseases to a random place
 */
    private void addSetDisease(int numberDisease)
    {
        int rand = Random.Range(0, waypointHolder.instance.waypoints.Count);
        waypointHolder.instance.waypoints[rand].GetComponent<waypointScript>().Infect(numberDisease);
        print("infected" + waypointHolder.instance.waypoints[rand].name);
    }
    /**
 * @memo 2022
 * update method used only for clouds
 */
    private void Update()
    {
        cloudManager();
    }
    /**
 * @memo 2022
 * cloud movement
 */
    private void cloudManager()
    {
        if (transform.childCount == 0)
        {
             cloud= Instantiate(CloudPrefab, this.transform);
            cloud.transform.position = new Vector3(15f, 0, 0);
        }
        //GameObject cloud= Instantiate(CloudPrefab, this.transform);
        if (cloud == null) { return; }
        if (cloud.transform.position.x > -11) 
        {
            
                cloud.transform.position= new Vector3(cloud.transform.position.x-Time.deltaTime, 0, 0);

        }
        else
        {
            Destroy(cloud);
        }
    }
    //research will consume 2 turns
}
