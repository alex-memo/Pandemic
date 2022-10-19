using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class waypointScript : MonoBehaviour
{
    public List<GameObject> borderPlaces;
    [SerializeField]
    private int numberInfections = 0;
    [SerializeField]
    private bool isResearch;
    private TMP_Text text;
    private Button infectButton;
    
    
    
    public void onMaxInfect()
    {
        int i = 0;
        while (i < borderPlaces.Count)
        {
            if (borderPlaces[i].GetComponent<waypointScript>().getNumberInfections()==3)
            {
                return;
            }
            borderPlaces[i].GetComponent<waypointScript>().Infect();
            i++;
        }

    }
    public void Infect()
    {
        print(this.name + " " + numberInfections);
        
        if (numberInfections < 3)
        {
            numberInfections++;
        }
        else//means 3 infections in site
        {
            onMaxInfect();
        }
        updateInfections();
    }
    private void Start()
    {
        GameObject canvas = Instantiate(gameManagerScript.instance.canvasPrefab, this.transform);
        text = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
       updateInfections();
        infectButton = canvas.GetComponent<Button>();
        infectButton.onClick.AddListener(onButtonClick); //subscribe to the onClick event
    }
    private void updateInfections()
    {
        text.text = numberInfections.ToString();
    }
    private void onButtonClick()
    {
        Infect();
    }
    public int getNumberInfections()
    {
        return numberInfections;
    }
}
