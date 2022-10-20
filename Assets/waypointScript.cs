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
    private Transform radioActiveHolder;
    
    
    
    public void onMaxInfect()
    {
        foreach(GameObject place in borderPlaces)
        {
            if (place.GetComponent<waypointScript>().getNumberInfections() < 3)
            {
                place.GetComponent<waypointScript>().Infect();
            }
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
        radioActiveHolder = canvas.transform.GetChild(1);
        updateInfections();
        infectButton = canvas.GetComponent<Button>();
        infectButton.onClick.AddListener(onButtonClick); //subscribe to the onButtonClick event
    }
    private void updateInfections()
    {
        text.text = numberInfections.ToString();
        updateRadioactiveUI();
    }
    private void onButtonClick()
    {
        Infect();
        //prompt ui
        if (isResearch)
        {
            //prompt research ui
        }
        else
        {
            //prompt 
        }
        //make player go here 
        
    }
    public int getNumberInfections()
    {
        return numberInfections;
    }
    private void updateRadioactiveUI()
    {
        foreach (Transform child in radioActiveHolder)
        {
            Destroy(child.gameObject);
        }
        int i = 0;

        while (i<numberInfections)
        {
            Instantiate(gameManagerScript.instance.radioactivePrefab, radioActiveHolder.transform);
            i++;
        }

    }
}
