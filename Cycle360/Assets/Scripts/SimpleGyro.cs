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
}
