using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointScript : MonoBehaviour
{
    public List<GameObject> borderPlaces;
    [SerializeField]
    private int numberInfections = 0;
    [SerializeField]
    private bool isResearch;

    public void onMaxInfect()
    {
        foreach(GameObject place in borderPlaces)
        {
            place.GetComponent<waypointScript>().Infect();
        }
    }
    public void Infect()
    {
        if (numberInfections < 3)
        {
            numberInfections++;
        }
        else//means 3 infections in site
        {
            onMaxInfect();
        }
    }
}
