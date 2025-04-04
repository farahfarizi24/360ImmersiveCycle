using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHazardAlternateDetector : MonoBehaviour
{
    public PercTest2 VPscript;
    public AudioSource CorrectBeep;
    SaveDatas savingScript;
    private float timerDuration = 6f;
    private float timer;
    private bool timerRunning = false;
    public int totalscore;
    // Update is called once per frame
    // public GameObject[] Prac1Hazards;
    private void Awake()
    {
        GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        savingScript = SaveFileObject.GetComponent<SaveDatas>();
        savingScript.OnFreezePerceptionTestEnter();
    }
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                Debug.Log("is click click");
                    if (hit.collider != null)
                    {
                        Debug.Log("Is hit");
                        // VPscript.PauseVid();
                        Debug.Log(hit.collider.gameObject.name);

                        if (hit.collider.gameObject.tag =="Hazards")
                    {
                        CorrectBeep.Play();

                        NewHazardHit(hit.collider.transform.parent.name);
                         IfCorrectHazardHit(hit.collider.transform.parent.name);
                        GameObject ParentObj = hit.collider.transform.parent.gameObject;
                        ParentObj.GetComponent<MeshRenderer>().enabled = true;
                         //IfCorrectHazardHit(hit.collider.gameObject.name);
                        savingScript.TotalCorrectWithinTheTest++;
                        savingScript.TotalCorrectWithinQuestions++;
                        Destroy(hit.collider.gameObject);
                    }
                  

                    }

                }
                else
                {
                Debug.Log("Wrong Click!");
                    IfWrongCLick();
                }


            if (timerRunning)
            {
                timer -= Time.deltaTime;

                if (timer <= 0f)
                {
                    timerRunning = false;
                    timer = 0f;

                    OnTimerComplete();
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
                    if (hit.collider.gameObject.tag == "Hazards")
                    {
                        NewHazardHit(hit.collider.transform.parent.name);
                         IfCorrectHazardHit(hit.collider.transform.parent.name);
                        GameObject ParentObj = hit.collider.transform.parent.gameObject;
                        ParentObj.GetComponent<MeshRenderer>().enabled = true;
                        // IfCorrectHazardHit(hit.collider.gameObject.name);
                        savingScript.TotalCorrectWithinTheTest++;
                        savingScript.TotalCorrectWithinQuestions++;
                        Destroy(hit.collider.gameObject);
                    }

                }

            }
            else
            {
                Debug.Log("is click!");

                IfWrongCLick();
            }
            if (timerRunning)
            {
                timer -= Time.deltaTime;

                if (timer <= 0f)
                {
                    timerRunning = false;
                    timer = 0f;

                    OnTimerComplete();
                }
            }


        }

#endif
    }
    public void StartTimer()
    {
        timer = timerDuration;
        timerRunning = true;
        Debug.Log("Timer started for 6 seconds.");
    }

    private void OnTimerComplete()
    {
        Debug.Log("Timer finished!");
        // Add your custom logic here (e.g., trigger an event, change scene, etc.)
    }
    public void NewHazardHit(string HazardName)
    {
        Debug.Log("Hit " + HazardName);
    }



    public void IfCorrectHazardHit(string HazardName)
    {
        GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        savingScript = SaveFileObject.GetComponent<SaveDatas>();
        VPscript.score++;
        totalscore++;
        savingScript.OnFreezePerceptionTestCorrectClick(VPscript.CurrentClipNumber.ToString(),timer.ToString(),HazardName,1);

        //Need fixing


    }


    public void IfWrongCLick()
    {
        savingScript.OnFreezePerceptionTestWrongClick(VPscript.CurrentClipNumber.ToString(),VPscript.curVideoTime.ToString());
    }
}
