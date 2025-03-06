using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGyro : MonoBehaviour
{
    private Vector3 RotationCam;
    // Start is called before the first frame update
    void Start()
    {
        RotationCam = Vector3.zero;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        RotationCam.y = Input.gyro.rotationRateUnbiased.y *-2.0f;
       
        RotationCam.x = Input.gyro.rotationRateUnbiased.x * -2.0f;
        transform.Rotate(RotationCam);
    }

    /* 
     * 
     * 
    TEST THIS LATER
    private bool gyroEnabled; 
    private UnityEngine.Gyroscope gyro;
    private GameObject GyroControl;
    private Quaternion rot;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        GyroControl = new GameObject("Gyro Control");
        GyroControl.transform.position = transform.position; 
        transform.SetParent(GyroControl.transform); //parents the object to an empty control object
        gyroEnabled = EnableGyro();
    }
    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            GyroControl.transform.rotation = Quaternion.Euler(90f, -90f, 0f); //These offset values are essential for the gyroscope to orientate itself correctly
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        return false;
    }
    private void Update()
    {
        Quaternion rotMin = Quaternion.Euler(new Vector3(0, 0, 0));

        Quaternion rotation = transform.rotation; //Values for locking the rotation of the gyro

        if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }

        if (rotation.y < rotMin.y)
        {
            transform.eulerAngles = Vector3.zero; //Doesnt allow rotation values to be in the negative
        }
    }*/
}
