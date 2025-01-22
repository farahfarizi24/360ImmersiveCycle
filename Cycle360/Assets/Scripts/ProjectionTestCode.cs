using System;
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
    public int SplittedClipNumber;
    private int TotalVideo = 13;
    public int TotalClipForThisVideo;
    public bool isChanging=false;
    // Start is called before the first frame update
    void Start()
    {
        SplittedClipNumber = 0;
        CurrentVideoNumber = 1;
        RunCurrentVideo();
        StartCoroutine(CountdownToStartVid());

    }

    // Update is called once per frame
    void Update()
    {
        ProjTestVP.loopPointReached += DecideWhatToDoNext;

    }

    private void DecideWhatToDoNext(VideoPlayer source)
    {
        if (!isChanging)
        {
            isChanging = true;
            if (CurrentVideoNumber == 1)
            {
                CurrentVideoNumber++;
                ProjTestVP.Stop();

                RunCurrentVideo();
                StartCoroutine(CountdownToStartVid());
            }
            else
            {
                if (CurrentVideoNumber != TotalVideo)
                {
                    if (SplittedClipNumber <= TotalClipForThisVideo-1)
                    {
                        ProjTestVP.Stop();

                        SplittedClipNumber++;
                        Debug.Log(SplittedClipNumber.ToString());
                        RunCurrentVideo();
                        ProjTestVP.Play();
                       
                        //StartCoroutine(CountdownToStartVid());
                    }
                    else
                    {
                        ProjTestVP.Stop();

                        SplittedClipNumber = 0;
                        CurrentVideoNumber++;
                        RunCurrentVideo();
                        StartCoroutine(CountdownToStartVid());
                    }


                    //check which is currently active;
                    //add split clip number if they're not at the end
                    //add video number 

                }
                else
                {
                    //EndingScene
                }

            }
        }
      
    }

  


    IEnumerator CountdownToStartVid()
    {
        ProjTestVP.Play();
        yield return new WaitForSeconds(1f);
        ProjTestVP.Pause();
        //start countdown
        yield return new WaitForSeconds(3f);
        ProjTestVP.Play();
        isChanging = false;

    }
    void RunCurrentVideo()
    {

        switch (CurrentVideoNumber) 
        {
            case 1:
               

                ProjTestVP.clip = ProjPrac1;
                break;
            case 2:
                ProjTestVP.clip = ProjPrac2[SplittedClipNumber];
                TotalClipForThisVideo = ProjPrac2.Length;
                
                break;

            case 3:
                ProjTestVP.clip = ProjPrac3[SplittedClipNumber];
                TotalClipForThisVideo = ProjPrac3.Length;



                break;
                case 4:
                ProjTestVP.clip = ProjQ1[SplittedClipNumber];
                TotalClipForThisVideo = ProjQ1.Length;

                break;
                case 5:
                ProjTestVP.clip = ProjQ2[SplittedClipNumber];
                TotalClipForThisVideo = ProjQ2.Length;

                break;
                case 6:
                ProjTestVP.clip = ProjQ3[SplittedClipNumber];
                TotalClipForThisVideo = ProjQ3.Length;

                break;
                case 7:
                ProjTestVP.clip = ProjQ4[SplittedClipNumber];
                TotalClipForThisVideo = ProjQ4.Length;

                break;
                case 8:
                ProjTestVP.clip = ProjQ5[SplittedClipNumber];
                TotalClipForThisVideo = ProjQ5.Length;

                break;
                case 9:
                ProjTestVP.clip = ProjQ6[SplittedClipNumber];
                TotalClipForThisVideo = ProjQ6.Length;

                break;
                case 10:
                ProjTestVP.clip = ProjQ7[SplittedClipNumber];
                TotalClipForThisVideo = ProjQ7.Length;

                break;
                case 11:
                ProjTestVP.clip = ProjQ8[SplittedClipNumber];
                TotalClipForThisVideo = ProjQ8.Length;

                break;
                case 12:
                ProjTestVP.clip = ProjQ9[SplittedClipNumber];
                TotalClipForThisVideo = ProjQ9.Length;

                break;
                case 13:
                ProjTestVP.clip = ProjQ10[SplittedClipNumber];
                TotalClipForThisVideo = ProjQ10.Length;

                break;




        }


    }
}
