using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTracker : MonoBehaviour
{
    public float speed=10000.0f;
    public float time = 5;
    public GameObject Hazard;
    void Start()
    {
   
       
    }

    // Update is called once per frame
    void Update()
    {
      
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
