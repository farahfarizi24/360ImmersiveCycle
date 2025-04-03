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

                    if (hit.collider != null)
                    {
                        Debug.Log("Is hit");
                        // VPscript.PauseVid();
                        CorrectBeep.Play();
                        Debug.Log(hit.collider.gameObject.name);

                        if (hit.collider.gameObject.tag =="Hazards")
                    {
                        NewHazardHit(hit.collider.transform.parent.name);
                        // IfCorrectHazardHit(hit.collider.transform.parent.name);
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
                    if (hit.collider.gameObject.tag == "Hazards")
                    {
                        NewHazardHit(hit.collider.transform.parent.name);
                        // IfCorrectHazardHit(hit.collider.transform.parent.name);
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



        }

#endif
    }

    public void NewHazardHit(string HazardName)
    {
        Debug.Log("Hit " + HazardName);
    }



    public void IfCorrectHazardHit(string HazardName)
    {
        //  GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        //  savingScript = SaveFileObject.GetComponent<SaveDatas>();
        //Need fixing


    }


    public void IfWrongCLick()
    {
        savingScript.OnPerceptionTestWrongClick(VPscript.curVideoTime.ToString());
    }
}
