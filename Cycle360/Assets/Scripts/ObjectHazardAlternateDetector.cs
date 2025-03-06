using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHazardAlternateDetector : MonoBehaviour
{
    public PercTest2 VPscript;
    public AudioSource CorrectBeep;
    SaveDatas savingScript;
    // Update is called once per frame
   // public GameObject[] Prac1Hazards;
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

                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider != null)
                    {
                        Debug.Log("Is hit");
                        // VPscript.PauseVid();
                        CorrectBeep.Play();
                        Debug.Log(hit.collider.gameObject.name);
                        IfCorrectHazardHit(hit.collider.transform.parent.name);
                        GameObject ParentObj = hit.collider.transform.parent.gameObject;
                        ParentObj.GetComponent<MeshRenderer>().enabled = true;
                       // IfCorrectHazardHit(hit.collider.gameObject.name);
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
                    IfCorrectHazardHit(hit.collider.transform.parent.name);
                    GameObject ParentObj = hit.collider.transform.parent.gameObject;
                    ParentObj.GetComponent<MeshRenderer>().enabled = true;
                    //   IfCorrectHazardHit(hit.collider.gameObject.name);
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

        if (HazardName == "RedCar")
        {
            int score = 0;
            //Score is 10 if its the first 1/3, is 5 if its 2/3, 3 if its close to end

            if (VPscript.curVideoTime >= 15.0f && VPscript.curVideoTime <= 18.4f)
            {
                score = 10;
            }

            else if (VPscript.curVideoTime >= 18.3f && VPscript.curVideoTime <= 21.7f)
            {
                score = 5;
            }

            else
            {
                score = 3;
            }


            VPscript.score++;
            savingScript.OnPerceptionTestCorrectClick("Practice_1", VPscript.curVideoTime.ToString(), HazardName, score);
        }
        if (HazardName == "Roundabout")
        {
            int score = 0;
            //Score is 10 if its the first 1/3, is 5 if its 2/3, 3 if its close to end

            if (VPscript.curVideoTime >= 18.0f && VPscript.curVideoTime <= 19.0f)
            {
                score = 10;
            }

            else if (VPscript.curVideoTime >= 18.9f && VPscript.curVideoTime <= 19.5f)
            {
                score = 5;
            }

            else
            {
                score = 3;
            }
            VPscript.score++;

            savingScript.OnPerceptionTestCorrectClick("Practice_1", VPscript.curVideoTime.ToString(), HazardName, score);
        }

        if (HazardName == "CarBraking")
        {
            int score = 0;
            //Score is 10 if its the first 1/3, is 5 if its 2/3, 3 if its close to end

            if (VPscript.curVideoTime >= 16.0f && VPscript.curVideoTime <= 19.0f)
            {
                score = 10;
            }

            else if (VPscript.curVideoTime >= 18.9f && VPscript.curVideoTime <= 22.0f)
            {
                score = 5;
            }

            else
            {
                score = 3;
            }
            VPscript.score++;

            savingScript.OnPerceptionTestCorrectClick("Practice_1", VPscript.curVideoTime.ToString(), HazardName, score);
        }

        if (HazardName == "ParkedGreyCar")
        {
            int score = 0;
            //Score is 10 if its the first 1/3, is 5 if its 2/3, 3 if its close to end

            if (VPscript.curVideoTime >= 16.0f && VPscript.curVideoTime <= 19.0f)
            {
                score = 10;
            }

            else if (VPscript.curVideoTime >= 18.9f && VPscript.curVideoTime <= 22.0f)
            {
                score = 5;
            }

            else
            {
                score = 3;
            }
            VPscript.score++;

            savingScript.OnPerceptionTestCorrectClick("Practice_1", VPscript.curVideoTime.ToString(), HazardName, score);
        }


        ///Q2
        ///
        if (HazardName == "Roundabout_P2")
        {
            int score = 0;
            //Score is 10 if its the first 1/3, is 5 if its 2/3, 3 if its close to end

            if (VPscript.curVideoTime >= 16.0f && VPscript.curVideoTime <= 19.0f)
            {
                score = 10;
            }

            else if (VPscript.curVideoTime >= 18.9f && VPscript.curVideoTime <= 22.0f)
            {
                score = 5;
            }

            else
            {
                score = 3;
            }
            VPscript.score++;

            savingScript.OnPerceptionTestCorrectClick("Practice_2", VPscript.curVideoTime.ToString(), HazardName, score);
        }
        if (HazardName == "Pedestrian_P2")
        {
            int score = 0;
            //Score is 10 if its the first 1/3, is 5 if its 2/3, 3 if its close to end

            if (VPscript.curVideoTime >= 16.0f && VPscript.curVideoTime <= 19.0f)
            {
                score = 10;
            }

            else if (VPscript.curVideoTime >= 18.9f && VPscript.curVideoTime <= 22.0f)
            {
                score = 5;
            }

            else
            {
                score = 3;
            }
            VPscript.score++;

            savingScript.OnPerceptionTestCorrectClick("Practice_2", VPscript.curVideoTime.ToString(), HazardName, score);
        }
        if (HazardName == "Speedbump_P2")
        {
            int score = 0;
            //Score is 10 if its the first 1/3, is 5 if its 2/3, 3 if its close to end

            if (VPscript.curVideoTime >= 16.0f && VPscript.curVideoTime <= 19.0f)
            {
                score = 10;
            }

            else if (VPscript.curVideoTime >= 18.9f && VPscript.curVideoTime <= 22.0f)
            {
                score = 5;
            }

            else
            {
                score = 3;
            }
            VPscript.score++;

            savingScript.OnPerceptionTestCorrectClick("Practice_2", VPscript.curVideoTime.ToString(), HazardName, score);
        }
    }


    public void IfWrongCLick()
    {
        savingScript.OnPerceptionTestWrongClick(VPscript.curVideoTime.ToString());
    }
}
