using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ProjectionTestCode : MonoBehaviour
{
    public VideoPlayer VP_1=null;
    public VideoPlayer VP_2=null;
    public VideoPlayer VP_3 = null;

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
 //   public int SplittedClipNumber;
    private int TotalVideo = 13;
    public int TotalClipForThisVideo;
    public bool isChanging=false;
    public bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
   //     SplittedClipNumber = 0;
        CurrentVideoNumber = 1;
        LoadCurrentVideo();
       // RunCurrentVideo();
        //StartCoroutine(CountdownToStartVid());
       
    }


    //https://discussions.unity.com/t/how-to-play-video-from-adressable-assetbundle-on-android/744900/13
    //s https://discussions.unity.com/t/create-obb-with-video-bigger-than-2-gb-for-android-assetbundle/681923/9
    //https://www.mp4compress.com/

  
    // Update is called once per frame
    void Update()
    {
        // ProjTestVP.loopPointReached += DecideWhatToDoNext;
       // if (VP_1.isPrepared&&!isPlaying) { VP_1.Play(); isPlaying = true; }
       if(VP_1.isPlaying) 
        VP_1.loopPointReached += SwitchVid;
       if(VP_2.isPlaying)
            VP_2.loopPointReached += SwitchVid;
       if(VP_3.isPlaying)
            VP_3.loopPointReached += SwitchVid;
        if (VP_1.isPlaying) { Debug.Log("First VP playing"); }
        if (VP_2.isPlaying) { Debug.Log("Second VP is playing"); }
        if (VP_3.isPlaying) { Debug.Log("Third VP is playing"); }
    }


    private void SwitchVid (VideoPlayer source)
    {
        Debug.Log("Switch Vid encountered");
        if (!isChanging)
        {
            if (source== VP_1)
            {
                Debug.Log("Source is VP1");
                if (TotalClipForThisVideo==1)
                {
                    CurrentVideoNumber++;
                    Debug.Log("Next video is" + CurrentVideoNumber);
                    LoadCurrentVideo();

                }
                else
                {
                
                    VP_2.Play();
                }
                VP_1.Stop();
                isChanging = true;
            }
            else if (VP_2.isPlaying)
            {
                if(TotalClipForThisVideo==3)
                {
                    VP_3.Play();
                }
                else
                {
                    CurrentVideoNumber++;
                    LoadCurrentVideo();
                }
                VP_2.Stop();
                isChanging = true;
            }
            else if (VP_3.isPlaying)
            {
                VP_3.Stop();
                
                    CurrentVideoNumber++;
                    LoadCurrentVideo();
                
                isChanging = true;

            }
        
        }
    }


    public void PlayNextClip()
    {
        if (VP_1.isPlaying)
        {
         
            VP_2.Play();
            VP_1.Stop();
            //Play the other one
        }
        else
        {
            VP_1.Play();
            VP_2.Stop();

            //PlayProjTestVP
        }
    }

    public void PrepareVP(VideoClip NextClip)
    {


        if (VP_1.isPlaying)
        {
            VP_2.clip = NextClip;
            VP_2.Prepare();

            //Play the other one
        }
        else
        {
            VP_1.clip = NextClip;
            VP_1.Prepare();
            //PlayProjTestVP
        }

    }


  
    IEnumerator CountdownToStartVid()
    {
      //  VP_1.Play();
      //  yield return new WaitForSeconds(1f);
     //   VP_1.Pause();
        //start countdown
        yield return new WaitForSeconds(3f);
        VP_1.Play();
        isChanging = false;

    }
    void LoadCurrentVideo()
    {

        switch (CurrentVideoNumber) 
        {
            case 1:
               

                VP_1.clip = ProjPrac1;
                VP_2.clip = null;
                VP_3.clip = null;
                TotalClipForThisVideo = 1;
                break;
            case 2:
             
                VP_1.clip = ProjPrac2[0];
                VP_2.clip = ProjPrac2[1];
                VP_3.clip = null;
                TotalClipForThisVideo = ProjPrac2.Length;
                
                break;

            case 3:
                VP_1.clip = ProjPrac3[0];
                VP_2.clip = ProjPrac3[1];
                VP_3.clip = null;
                TotalClipForThisVideo = ProjPrac3.Length;



                break;
                 
            case 4:
                VP_1.clip = ProjQ1[0];
                VP_2.clip = ProjQ1[1];
                VP_3.clip = ProjQ1[2];
                TotalClipForThisVideo = ProjQ1.Length;

                break;
            case 5:
                VP_1.clip = ProjQ2[0];
                VP_2.clip = ProjQ2[1];
                VP_3.clip = null ;
                TotalClipForThisVideo = ProjQ2.Length;

                break;
            case 6:
                VP_1.clip = ProjQ3[0];
                VP_2.clip = ProjQ3[1];
                VP_3.clip = null ;
                TotalClipForThisVideo = ProjQ3.Length;

                break;
                case 7:
                VP_1.clip = ProjQ4[0];
                VP_2.clip = ProjQ4[1];
                VP_3.clip = ProjQ4[2] ;
                TotalClipForThisVideo = ProjQ4.Length;

                break;
                case 8:
                VP_1.clip = ProjQ5[0];
                VP_2.clip = ProjQ5[1];
                VP_3.clip = null ;
                TotalClipForThisVideo = ProjQ5.Length;

                break;
                case 9:
                VP_1.clip = ProjQ6[0];
                VP_2.clip = ProjQ6[1];
                VP_3.clip = ProjQ6[2];
                TotalClipForThisVideo = ProjQ6.Length;

                break;
                case 10:
                VP_1.clip = ProjQ7[0];
                VP_2.clip = ProjQ7[1];
                VP_3.clip = ProjQ7[2];
                TotalClipForThisVideo = ProjQ7.Length;

                break;
                case 11:
                VP_1.clip = ProjQ8[0];
                VP_2.clip = ProjQ8[1];
                VP_3.clip = ProjQ8[2];
                TotalClipForThisVideo = ProjQ8.Length;

                break;
                case 12:
                VP_1.clip = ProjQ9[0];
                VP_2.clip = ProjQ9[1];
                VP_3.clip = ProjQ9[2];
                TotalClipForThisVideo = ProjQ9.Length;

                break;
                case 13:
                VP_1.clip = ProjQ10[0];
                VP_2.clip = ProjQ10[1];
                VP_3.clip = ProjQ10[2];
                TotalClipForThisVideo = ProjQ10.Length;

                break;




        }

        PrepareVideoToPlay();
        StartCoroutine(CountdownToStartVid());


    }

    void PrepareVideoToPlay()
    {
        VP_1.Prepare();
        VP_2.Prepare();
        VP_3.Prepare();
    }
}
