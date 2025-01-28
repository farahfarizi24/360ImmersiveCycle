using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ProjectionTestCode : MonoBehaviour
{
    public VideoPlayer ProjTestVP=null;
    public VideoPlayer SecondVideoPlayer=null;
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
    public bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        SplittedClipNumber = 0;
        CurrentVideoNumber = 1;
       // RunCurrentVideo();
        //StartCoroutine(CountdownToStartVid());
        TestTransition();
    }


    //https://discussions.unity.com/t/how-to-play-video-from-adressable-assetbundle-on-android/744900/13
    //s https://discussions.unity.com/t/create-obb-with-video-bigger-than-2-gb-for-android-assetbundle/681923/9
    //https://www.mp4compress.com/

    private void TestTransition()
    {
        ProjTestVP.clip = ProjPrac2[0];
        ProjTestVP.Prepare();
        SecondVideoPlayer.clip = ProjPrac2[1];
        SecondVideoPlayer.Prepare();
        
    }

    // Update is called once per frame
    void Update()
    {
        // ProjTestVP.loopPointReached += DecideWhatToDoNext;
        if (ProjTestVP.isPrepared&&!isPlaying) { ProjTestVP.Play(); isPlaying = true; }

        ProjTestVP.loopPointReached += SwitchVid;

        if (ProjTestVP.isPlaying) { Debug.Log("First VP playing"); }
        if (SecondVideoPlayer.isPlaying) { Debug.Log("Second VP is playing"); }
    }


    private void SwitchVid (VideoPlayer source)
    {
        if (!isChanging)
        {
            SecondVideoPlayer.Play();
            ProjTestVP.Stop();
            isChanging = true;
        }
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
                       
                        PlayNextClip();
                        //PrepareVP()
                       /* SplittedClipNumber++;
                        Debug.Log(SplittedClipNumber.ToString());
                        RunCurrentVideo();
                        ProjTestVP.Play();
                       */
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


    public void PlayNextClip()
    {
        if (ProjTestVP.isPlaying)
        {
         
            SecondVideoPlayer.Play();
            ProjTestVP.Stop();
            //Play the other one
        }
        else
        {
            ProjTestVP.Play();
            SecondVideoPlayer.Stop();

            //PlayProjTestVP
        }
    }

    public void PrepareVP(VideoClip NextClip)
    {


        if (ProjTestVP.isPlaying)
        {
            SecondVideoPlayer.clip = NextClip;
            SecondVideoPlayer.Prepare();

            //Play the other one
        }
        else
        {
            ProjTestVP.clip = NextClip;
            ProjTestVP.Prepare();
            //PlayProjTestVP
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
