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



    /**
 * @memo 2022
 * when max infect outbreak
 */
    public void onMaxInfect()
    {
        foreach(GameObject place in borderPlaces)
        {
            if (place.GetComponent<waypointScript>().getNumberInfections() < 3)
            {
                place.GetComponent<waypointScript>().Infect(1);
            }
        }

    }
    /**
 * @memo 2022
 * infects this place
 */
    public void Infect(int infectionNumber)
    {
        print(this.name + " " + numberInfections);
        
        if (numberInfections < 3)
        {
            numberInfections+=infectionNumber;
            if (numberInfections > 3)
            {
                numberInfections = 3;
            }
        }
        else//means 3 infections in site
        {
            onMaxInfect();
        }
        updateInfections();
    }
    /**
 * @memo 2022
 * start method
 */
    private void Start()
    {
        GameObject canvas = Instantiate(gameManagerScript.instance.canvasPrefab, this.transform);
        text = canvas.transform.GetChild(0).GetComponent<TMP_Text>();
        radioActiveHolder = canvas.transform.GetChild(1);
        
        infectButton = canvas.GetComponent<Button>();
        infectButton.onClick.AddListener(onButtonClick); //subscribe to the onButtonClick event
        if (isResearch)
        {
            Instantiate(gameManagerScript.instance.researchPrefab, this.transform);
        }
        updateInfections();
    }
    /**
 * @memo 2022
 * updates the infections in this place
 */
    private void updateInfections()
    {
        //print(text);
        if (text == null)
        {
            return;
        }
        text.text = numberInfections.ToString();
        updateRadioactiveUI();
    }
    /**
 * @memo 2022
 * on button clicked
 */
    private void onButtonClick()
    {

        //prompt ui
        
        if (isResearch)
        {
            //researchUI.SetActive(true);
            //prompt research ui
            Controller.instance.researchPrompt(this.name);
        }
        else
        {
            Controller.instance.goHerePrompt(this.name);
        }
        //make player go here 
        
        
    }
    /**
 * @memo 2022
 * getter for number of infections
 */
    public int getNumberInfections()
    {
        return numberInfections;
    }
    /**
 * @memo 2022
 * updates the radioactive ui
 */
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
    /**
 * @memo 2022
 * cleanses this place by a set amount
 */
    public void cure(int temp)
    {
        numberInfections--;
        if (numberInfections < 0)
        {
            numberInfections = 0;
        }
        updateInfections();
    }
}
