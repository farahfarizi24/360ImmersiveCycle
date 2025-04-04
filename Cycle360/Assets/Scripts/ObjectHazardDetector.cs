using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class ObjectHazardDetector : MonoBehaviour
{

    public VideoPlayerScript VPscript;
    public AudioSource CorrectBeep;
    SaveDatas savingScript;
    public GameObject tempObject;
    public float startHazardTime;
    public float endHazrdTime;
    // Update is called once per frame

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
                        tempObject = hit.collider.gameObject;
                        startHazardTime = tempObject.GetComponent<HazardTracker>().initTime;
                        endHazrdTime = tempObject.GetComponent<HazardTracker>().despawnTime;
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


        int score= CalculateScore(startHazardTime, endHazrdTime, VPscript.curFrame);
        savingScript.OnPerceptionTestCorrectClick(VPscript.CurPlayingClip.ToString(), VPscript.curFrame.ToString(), HazardName, score);
        score = 0;
        startHazardTime = 0.0f;
        endHazrdTime = 0.0f;
    }

    public static int CalculateScore(float startTime, float endTime, float interactionTime)
    {
        if (interactionTime < startTime)
            interactionTime = startTime;
        if (interactionTime > endTime)
            interactionTime = endTime;

        float totalDuration = endTime - startTime;
        float interactionDelay = interactionTime - startTime;

        if (totalDuration == 0f)
            return 10; // No duration = auto-perfect score

        float oneThird = totalDuration / 3f;

        if (interactionDelay <= oneThird)
            return 10;
        else if (interactionDelay <= 2f * oneThird)
            return 5;
        else
            return 10;

    }

    public void IfWrongCLick()
    {
        savingScript.OnPerceptionTestWrongClick(VPscript.curFrame.ToString());
    }
}
