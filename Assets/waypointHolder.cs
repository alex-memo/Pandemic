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
}
