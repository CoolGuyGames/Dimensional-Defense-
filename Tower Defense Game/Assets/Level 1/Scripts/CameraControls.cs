using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Vector3[] locations;
    public int currentLocation = 0;

    private void Update()
    {
        this.transform.position = locations[currentLocation];

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentLocation = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentLocation = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentLocation = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentLocation = 3;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            currentLocation++;
            currentLocation = currentLocation % 4;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            currentLocation--;
            currentLocation += 4;
            currentLocation = currentLocation % 4;
        }
    }
}
