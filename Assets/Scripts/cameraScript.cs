using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    private int camSpeed = 5;
    // Update is called once per frame
    /**
 * @memo 2022
 * simple camera movement script for league like camera
 */
    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.mousePosition.y >= Screen.height - 40 && pos.y <= 6)
        {
            pos.y += camSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= 40 && pos.y >= -6)
        {
            pos.y -= camSpeed * Time.deltaTime;

        }
        if (Input.mousePosition.x >= Screen.width - 40 && pos.x <= 6)
        {
            pos.x += camSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= 40 && pos.x >= -3)
        {
            pos.x -= camSpeed * Time.deltaTime;

        }
        transform.position = pos;
    }
}