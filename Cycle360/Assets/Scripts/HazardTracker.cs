using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTracker : MonoBehaviour
{
  //  public Vector3 initialPosition = new Vector3 (-13.0f,53.0f,9.2f);
   // public Vector3 finalPosition = new Vector3(-4.3f,39.4f,-141.8f);
    public float speed=10000.0f;
    float time = 5;
    public GameObject Hazard;
    // Start is called before the first frame update
    void Start()
    {
     //   Hazard.transform.position=initialPosition;

       
    }

    // Update is called once per frame
    void Update()
    {
       // var step = speed * Time.deltaTime;
       // Hazard.transform.position = Vector3.MoveTowards(initialPosition, finalPosition, step);
        
    }


    public void StartMove(Vector3 initPosi,Vector3 finalPosi,float t)
    {
        StartCoroutine(Move(initPosi, finalPosi, t));
    }

    IEnumerator Move(Vector3 beginPos, Vector3 endPos, float time)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            Debug.Log("isMoving");
            Hazard.transform.position = Vector3.Lerp(beginPos, endPos, t);
            yield return null;
        }


        


    }

}
