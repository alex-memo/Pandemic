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
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        getRole();
    }
    private void getRole()
    {
        role = gameManagerScript.instance.getRole();
        roleText.text = role;
    }
    public void showHideRoleDescription()
    {
        roleDescription.SetActive(!roleDescription.activeSelf);
        roleDescriptionText.text = gameManagerScript.instance.getRoleDescription(role);
    }
    public void goHereCure(waypointScript waypoint)
    {
        int rand = Random.Range(0, 100);
        if (rand <= doubleCureChance)//double cure
        {
            waypoint.cure(2);
            researchPoints += 2;
        }
        else
        {
            waypoint.cure(1);
            researchPoints += 1;
        }
        updateUI();
    }
    public void updateUI()
    {
        roundInfoText.text = $"Round: {gameManagerScript.instance.round}\tResearch Points: {researchPoints}";
    }
    public void addCureChance(int chance)
    {
        doubleCureChance += chance;
    }
}
