using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHazardDetector : MonoBehaviour
{
    public VideoPlayerScript VPscript;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    Debug.Log("Is hit");
                    VPscript.PauseVid();

                }

            }

        }


#if UNITY_EDITOR
        if (Input.GetMouseButton(0)) 
        
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log("Is hit");

                    VPscript.PauseVid();

                }

            }

        }

#endif
    }
}
