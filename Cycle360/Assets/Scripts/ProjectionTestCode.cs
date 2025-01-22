using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ProjectionTestCode : MonoBehaviour
{
    public VideoPlayer ProjTestVP;
    public VideoClip ProjPrac1;
    public VideoClip[] ProjPrac2;
    public VideoClip[] ProjPrac3;
    public VideoClip[] ProjQ1;
    public VideoClip[] ProjQ2;
    public VideoClip[] ProjQ3;
    public VideoClip[] ProjQ4;
    public VideoClip[] ProjQ5;
    public VideoClip[] ProjQ6;
    public VideoClip[] ProjQ7;
    public VideoClip[] ProjQ8;
    public VideoClip[] ProjQ9;
    public VideoClip[] ProjQ10;
    public int CurrentVideoNumber; //13 video, 3 prac 10 final

    // Start is called before the first frame update
    void Start()
    {
        CurrentVideoNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void StartTimer()
    {
        StartCoroutine(CountdownToStartVid());
    }


    IEnumerator CountdownToStartVid()
    {
        ProjTestVP.Play();
        yield return new WaitForSeconds(1f);
        ProjTestVP.Pause();
        //start countdown
        yield return new WaitForSeconds(3f);

        
    }
    void RunCurrentVideo()
    {
        switch (CurrentVideoNumber) 
        {
            case 1:
                ProjTestVP.clip = ProjPrac1;
                break;
            case 2:
                break;

            case 3:
                break;
                case 4:
                break;
                case 5:
                break;
                case 6:
                break;
                case 7:
                break;
                case 8:
                break;
                case 9:
                break;
                case 10:
                break;
                case 11:
                break;
                case 12:
                break;
                case 13:
                break;




        }


    }
}
