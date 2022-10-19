using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
