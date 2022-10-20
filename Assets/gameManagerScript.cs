using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    public GameObject canvasPrefab;
    public GameObject researchPrefab;
    public static gameManagerScript instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
