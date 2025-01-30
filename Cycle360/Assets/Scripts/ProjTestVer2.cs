using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Video;

public class ProjTestVer2 : MonoBehaviour
{
    public VideoClip[] clip;//There will be 13 video clip 
    public int CurrentClipNumber;
    public VideoPlayer VP;
    public bool isChanging=false;
    SaveDatas savingScript;
    public string QuestionID;
    public int totalScore;
    public int ThisQuestionScore;
    public float curVidTime;
    public string ResOutcome;
    // Start is called before the first frame update
    void Start()
    {
        GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        savingScript = SaveFileObject.GetComponent<SaveDatas>();
        savingScript.OnProjectionTestEnter();
        CurrentClipNumber = 1;
        LoadClip();
        totalScore = 0;
        ThisQuestionScore = 0;
        ResOutcome = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (VP.isPlaying)
        {
            VP.loopPointReached += SwitchVid;

        }
        curVidTime = (float)VP.time;
    }

    private void SwitchVid(VideoPlayer player)
    {
        if (!isChanging && CurrentClipNumber!=13)
        {

            isChanging = true;
            VP.Stop();
            CurrentClipNumber++;
            LoadClip();
        }

        if(CurrentClipNumber == 13)
        {
            isChanging = true;
            VP.Stop();
            //SHOWCASE END SCREEN
        }
    }


    private void OnNoButtonPress()
    {
        savingScript.OnProjectionTestResponse(QuestionID, "No Response", "NA", 0,curVidTime);
    }

    private void OnStopButtonPress()
    {
       
        CalculateScore();

        //CalculateScore
        //CalculateTotalScore
        // Within the correct time window for the response
        // the earlier they respond appropriately the more points they score
        // - 10 points if they respond early to the situation,
        // down to 1 point if they respond just in time.
        // They will not score any points if they respond before the time window or after the time window
        totalScore += ThisQuestionScore;

        savingScript.OnProjectionTestResponse(QuestionID, "Stop", ResOutcome, ThisQuestionScore,curVidTime);
        CurrentClipNumber++;
        LoadClip();
    }

    public int CalculateScore()
    {
      
        switch (CurrentClipNumber)
        {
            case 1:
                if(curVidTime>=25.0f && curVidTime <= 28.0f)
                {
                    ThisQuestionScore = 10;
                    ResOutcome = "Correct";
                    
                }else if(curVidTime<=24.9)
                {
                    ThisQuestionScore = 0;
                    ResOutcome = "Early";
                }
                else
                {
                    ThisQuestionScore = 0;
                    ResOutcome = "Late";
                }
                break;


                case 2:

                if (curVidTime >= 7.0f && curVidTime <= 10.0f)
                {
                    ThisQuestionScore = 10;
                    ResOutcome = "Correct";

                }
                else if (curVidTime <= 9.9f)
                {
                    ThisQuestionScore = 0;
                    ResOutcome = "Early";
                }
                else
                {
                    ThisQuestionScore = 0;
                    ResOutcome = "Late";
                }
                break;
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
        return ThisQuestionScore;
    }
    IEnumerator CountdownToStartVid()
    {
        //Provide 3 seconds break before Starting to play video
        yield return new WaitForSeconds(3f);
        VP.Play();
        isChanging = false;

    }
    public void LoadClip()
    {
        ThisQuestionScore = 0;
        VP.clip = clip[CurrentClipNumber - 1];
        VP.Prepare();
        if (CurrentClipNumber <= 3)
        {
            QuestionID="Practice" + CurrentClipNumber.ToString();
        }
        else
        {
           int  tempID = CurrentClipNumber - 3;
            QuestionID = tempID.ToString();
        }
        StartCoroutine(CountdownToStartVid());
    }
}
