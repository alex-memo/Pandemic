using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointHolder : MonoBehaviour
{
    public static waypointHolder instance;
    public List<GameObject> waypoints= new List<GameObject>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        foreach(Transform child in this.transform)
        {
            waypoints.Add(child.gameObject);
        }
    }
    public void onRoundEnd()//when round ends
    {
        //if there is an infection anywhere add one to that infection
        foreach(GameObject place in waypointHolder.instance.waypoints)
        {
            if (place.GetComponent<waypointScript>().getNumberInfections() > 0)
            {
                place.GetComponent<waypointScript>().Infect(1);
            }
            
        }
    }
}
