using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    public GameObject roleDescription;
    private int doubleCureChance;
    private string role = "";
    public int researchPoints;

    public TMPro.TMP_Text roundInfoText;
    public TMPro.TMP_Text roleText;
    public TMPro.TMP_Text roleDescriptionText;

    public UnityEngine.UI.Image roleImage;

    private bool hasToMove = false;
    private readonly float movespeed = 1f;//move speed for moving player around

    public GameObject researchUI;
    public GameObject goHereUI;

    private string toGo = "";

    public GameObject researchYesButton;
    public GameObject researchNoButton;
    public GameObject buyPreventionPanel;
    /**
 * @memo 2022
 * Creates instance of this script
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
 * Stat method
 */
    private void Start()
    {
        getRole();
        updateUI();
    }
    /**
 * @memo 2022
 * update method
 */
    private void Update()
    {
        if (hasToMove)
        {
            moveTile();
        }
    }
    /**
 * @memo 2022
 * gets the role for the player on that game
 */
    private void getRole()
    {
        role = gameManagerScript.instance.getRole();
        roleText.text = role;
    }
    /**
 * @memo 2022
 * shoes the role description to the player in the ui
 */
    public void showHideRoleDescription()
    {
        roleDescription.SetActive(!roleDescription.activeSelf);
        roleDescriptionText.text = gameManagerScript.instance.getRoleDescription(role);
    }
    /**
 * @memo 2022
 * cures a specific waypoint
 */
    public void goHereCure(waypointScript waypoint)
    {
        int rand = Random.Range(0, 100);
        if (rand <= doubleCureChance)//double cure
        {
            if (waypoint.getNumberInfections() > 0)
            {
                waypoint.cure(2);
                researchPoints += 2;
            }

        }
        else
        {
            if (waypoint.getNumberInfections() > 0)
            {
                waypoint.cure(1);
                researchPoints += 1;
            }

        }
        updateUI();
    }
    /**
 * @memo 2022
 * updates ui
 */
    public void updateUI()
    {
        roundInfoText.text = $"Round: {gameManagerScript.instance.round}\tResearch Points: {researchPoints}";
    }
    /**
 * @memo 2022
 * adds cure crit chance essencialy
 */
    public void addCureChance(int chance)
    {
        doubleCureChance += chance;
    }
    /**
 * @memo 2022
 * moves the player
 */
    private void moveTile()//dir should be 1 forward or -1 backward
    {
        int pos = getPlaceByIndex(toGo);
        if (transform.position != waypointHolder.instance.waypoints[pos].transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypointHolder.instance.waypoints[pos].transform.position, movespeed * Time.deltaTime);
        }
        if (transform.position.x == waypointHolder.instance.waypoints[pos].transform.position.x && transform.position.y == waypointHolder.instance.waypoints[pos].transform.position.y)//just check x and y
        {//if in the place already
            hasToMove = false;
            toGo = "";
            goHereCure(waypointHolder.instance.waypoints[pos].GetComponent<waypointScript>());
            gameManagerScript.instance.onRoundEnd();
        }
    }
    /**
 * @memo 2022
 * gets place by index from the waypoint holder
 */
    private int getPlaceByIndex(string temp)
    {
        int i = 0;

        foreach (GameObject place in waypointHolder.instance.waypoints)
        {
            if (place.name == temp)
            {
                return i;
            }
            i++;
        }
        return -1;//place does not exist
    }
    /**
 * @memo 2022
 * getter for current role
 */
    public string getCurrentRole()
    {
        return role;
    }
    /**
 * @memo 2022
 * go here pop up
 */
    public void goHerePrompt(string place)
    {
        goHereUI.SetActive(true);
        toGo = place;
        goHereUI.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = $"Are you sure you want to go to {toGo}?";
    }
    /**
 * @memo 2022
 * research pop up
 */
    public void researchPrompt(string place)
    {
        researchUI.SetActive(true);
        researchYesButton.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = $"Go to {place}!";
        if (role == "Scientist")//means dont have to move due to passive
        {
            researchYesButton.SetActive(false);
            researchNoButton.SetActive(false);
            buyPreventionPanel.SetActive(false);
        }
        else
        {
            researchYesButton.SetActive(true);
            researchNoButton.SetActive(true);
            buyPreventionPanel.SetActive(true);
        }
    }
    /**
 * @memo 2022
 * button yes for goHere
 */
    public void onHitYes()
    {
        goHereUI.SetActive(false);
        hasToMove = true;
    }
    /**
 * @memo 2022
 * button no for goHere
 */
    public void onHitNo()
    {
        goHereUI.SetActive(false);
    }
    /**
 * @memo 2022
 * button yes for research
 */
    public void onHitYesResearch()
    {
        researchYesButton.SetActive(false);
        researchNoButton.SetActive(false);
        hasToMove = true;
    }
    /**
 * @memo 2022
 * button no for research
 */
    public void onHitNoResearch()
    {
        researchUI.SetActive(false);
    }
}
