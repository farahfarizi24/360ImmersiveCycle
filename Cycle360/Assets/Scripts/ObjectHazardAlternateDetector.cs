using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHazardAlternateDetector : MonoBehaviour
{
    public VideoPlayerScript VPscript;
    public AudioSource CorrectBeep;
    SaveDatas savingScript;
    // Update is called once per frame
    public GameObject[] Prac1Hazards;
    private void Awake()
    {
        GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        savingScript = SaveFileObject.GetComponent<SaveDatas>();
        savingScript.OnPerceptionTestEnter();
    }
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (VPscript.isPlaying)
            {
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider != null)
                    {
                        Debug.Log("Is hit");
                        // VPscript.PauseVid();
                        CorrectBeep.Play();
                        Debug.Log(hit.collider.gameObject.name);
                        IfCorrectHazardHit(hit.collider.gameObject.name);
                        savingScript.TotalCorrectWithinTheTest++;
                        savingScript.TotalCorrectWithinQuestions++;
                        Destroy(hit.collider.gameObject);

                    }

                }
                else
                {

                    IfWrongCLick();
                }
            }



        }


#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))

        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider != null)
                {
                    Debug.Log("Is hit");

                    CorrectBeep.Play();
                    Debug.Log(hit.collider.gameObject.name);
                    IfCorrectHazardHit(hit.collider.gameObject.name);
                    Destroy(hit.collider.gameObject);
                    savingScript.TotalCorrectWithinTheTest++;
                    savingScript.TotalCorrectWithinQuestions++;
                    //save data 

                }

            }
            else
            {
                Debug.Log("is click!");

                IfWrongCLick();
            }



        }

#endif
    }



    public void IfCorrectHazardHit(string HazardName)
    {
        //  GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        //  savingScript = SaveFileObject.GetComponent<SaveDatas>();

        if (HazardName == "RedCar_Hazard")
        {
            int score = 0;
            //Score is 10 if its the first 1/3, is 5 if its 2/3, 3 if its close to end

            if (VPscript.curFrame >= 15.0f && VPscript.curFrame <= 18.4f)
            {
                score = 10;
            }

            else if (VPscript.curFrame >= 18.3f && VPscript.curFrame <= 21.7f)
            {
                score = 5;
            }

            else
            {
                score = 3;
            }

            savingScript.OnPerceptionTestCorrectClick("Practice_1", VPscript.curFrame.ToString(), HazardName, score);
        }
        if (HazardName == "ParkedCar_Hazard")
        {
            int score = 0;
            //Score is 10 if its the first 1/3, is 5 if its 2/3, 3 if its close to end

            if (VPscript.curFrame >= 18.0f && VPscript.curFrame <= 19.0f)
            {
                score = 10;
            }

            else if (VPscript.curFrame >= 18.9f && VPscript.curFrame <= 19.5f)
            {
                score = 5;
            }

            else
            {
                score = 3;
            }

            savingScript.OnPerceptionTestCorrectClick("Practice_1", VPscript.curFrame.ToString(), HazardName, score);
        }

        if (HazardName == "SpeedBump_Roundabout_Hazard")
        {
            int score = 0;
            //Score is 10 if its the first 1/3, is 5 if its 2/3, 3 if its close to end

            if (VPscript.curFrame >= 16.0f && VPscript.curFrame <= 19.0f)
            {
                score = 10;
            }

            else if (VPscript.curFrame >= 18.9f && VPscript.curFrame <= 22.0f)
            {
                score = 5;
            }

            else
            {
                score = 3;
            }

            savingScript.OnPerceptionTestCorrectClick("Practice_1", VPscript.curFrame.ToString(), HazardName, score);
        }
    }


    public void IfWrongCLick()
    {
        savingScript.OnPerceptionTestWrongClick(VPscript.curFrame.ToString());
    }
}
