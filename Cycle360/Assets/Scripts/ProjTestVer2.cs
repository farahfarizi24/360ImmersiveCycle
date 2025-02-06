using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class ProjTestVer2 : MonoBehaviour
{
    //https://discussions.unity.com/t/playing-large-video-files-in-gear-vr-in-unity-project/673596/4
    //https://discussions.unity.com/t/retrieving-and-playing-a-video-file-stored-locally-on-an-android-vr-headset-oculus-quest/224309/3

    public VideoClip[] clip;//There will be 13 video clip 
    public GameObject PlayerObject;
    public int CurrentClipNumber;
    public TMP_Text TimerText;
    public int CountdownTimer;
    public VideoPlayer VP;
    public bool isChanging=false;
    public bool isReady = false;
    SaveDatas savingScript;
    public string QuestionID;
    public int totalScore;
    public int ThisQuestionScore;
    public float curVidTime;
    public string ResOutcome;
    public GameObject InstructionText;
    public GameObject NextButton;
    public GameObject StopButton;
    public GameObject BackgrounUI;
    public GameObject FinalFeedback;
    public GameObject QuestionFeedback;
    // Start is called before the first frame update
    void Start()
    {
        GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        savingScript = SaveFileObject.GetComponent<SaveDatas>();
        savingScript.OnProjectionTestEnter();
        CurrentClipNumber = 1;
       // LoadClip();
        totalScore = 0;
        ThisQuestionScore = 0;
        ResOutcome = "";
        TimerText.text = "";
        CountdownTimer = 3;
        StartCoroutine(LoadAtStart());
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
            OnNoButtonPress();
            StopButton.SetActive(false);
            NextButton.SetActive(true);
            QuestionFeedback.SetActive(true);
            BackgrounUI.SetActive(true);    
            CurrentClipNumber++;
            LoadClip();

        }

        if(CurrentClipNumber == 13)
        {
            isChanging = true;
            OnNoButtonPress();
            StopButton.SetActive(false);
            NextButton.GetComponent<TMP_Text>().text = "Finish Test";
            NextButton.SetActive(true);

            QuestionFeedback.SetActive(true);
            FinalFeedback.SetActive(true);
            BackgrounUI.SetActive(true);
            VP.Stop();
            //SHOWCASE END SCREEN
        }
    }


    private void OnNoButtonPress()
    {
        savingScript.OnProjectionTestResponse(QuestionID, "No Response", "NA", 0,curVidTime);
        totalScore += 0;
        StopButton.gameObject.SetActive(false);
    
        //savingScript.OnProjectionTestResponse(QuestionID, "Stop", ResOutcome, ThisQuestionScore, curVidTime);
        if (CurrentClipNumber == 13)
        {
            VP.Stop();
            //ENDING SCENE
        }
        else
        {
            CurrentClipNumber++;
            LoadClip();
        }
    }
    public void ConfigureNextButton()
    {
        if (CurrentClipNumber == 1)
        {
            NextButton.GetComponentInChildren<TMP_Text>().text = "Start Practice";

        }
        else
        {
            NextButton.GetComponentInChildren<TMP_Text>().text = "Next";

        }
        if (CurrentClipNumber == 4)
        {
            NextButton.GetComponentInChildren<TMP_Text>().text = "Start Projection Test";
        }
        else
        {
            NextButton.GetComponentInChildren<TMP_Text>().text = "Next";

        }
     
      
    }
    public void OnStopButtonPress()
    {

        CalculateScore();
        StopButton.gameObject.SetActive(false);

        //CalculateScore
        //CalculateTotalScore
        // Within the correct time window for the response
        // the earlier they respond appropriately the more points they score
        // - 10 points if they respond early to the situation,
        // down to 1 point if they respond just in time.
        // They will not score any points if they respond before the time window or after the time window
        totalScore += ThisQuestionScore;


        savingScript.OnProjectionTestResponse(QuestionID, "Stop", ResOutcome, ThisQuestionScore,curVidTime);


        if (!isChanging && CurrentClipNumber != 13)
        {

            isChanging = true;
            VP.Stop();
         
            StopButton.SetActive(false);
            NextButton.SetActive(true);
            QuestionFeedback.SetActive(true);
            BackgrounUI.SetActive(true);
            CurrentClipNumber++;
            LoadClip();

        }

        if (CurrentClipNumber == 13)
        {
            isChanging = true;
            StopButton.SetActive(false);
            NextButton.GetComponent<TMP_Text>().text = "Finish Test";
            NextButton.SetActive(true);

            QuestionFeedback.SetActive(true);
            FinalFeedback.SetActive(true);
            BackgrounUI.SetActive(true);
            VP.Stop();
            //SHOWCASE END SCREEN
        }

        
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
        while(CountdownTimer > 0)
        {
            Debug.Log("Timer start");
            TimerText.text = CountdownTimer.ToString();
            yield return new WaitForSeconds(1f);
            CountdownTimer--;
        }
        TimerText.text = "Go!";
        //Provide 3 seconds break before Starting to play video
        //yield return new WaitForSeconds(3f);
        yield return new WaitForSeconds(1f);
        TimerText.text = "";
        StopButton.gameObject.SetActive(true);
        VP.Play();
        isChanging = false;
        CountdownTimer = 3;
        StopCoroutine(CountdownToStartVid());


    }

    public void OnNextButtonPressed()
    {
        if (InstructionText.activeSelf)
        {
            NextButton.gameObject.SetActive(false);
            BackgrounUI.gameObject.SetActive(false);
            InstructionText.gameObject.SetActive(false );
            //  LoadClip();    IEnumerator CountdownToStartVid()
            StartCoroutine(CountdownToStartVid());

        }
        else if (QuestionFeedback.gameObject.activeSelf)
        {
            NextButton.gameObject.SetActive(false);
            BackgrounUI.gameObject.SetActive(false);
            QuestionFeedback.gameObject.SetActive(false);
            FinalFeedback.gameObject.SetActive(false);
            if (CurrentClipNumber == 13)
            {
                //END
                Application.Quit();
            }
            else
            {
                StartCoroutine(CountdownToStartVid());
            }
        }
    }
    IEnumerator LoadAtStart()
    {
        VP.Play();
        yield return new WaitForSeconds(0.5f);
        VP.Pause();
        isReady = true;
        if(CurrentClipNumber == 1)
        {
            PlayerObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        }
        if (CurrentClipNumber >= 2)
        {
            PlayerObject.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);

        }
    }
    public void LoadClip()
    {
        ThisQuestionScore = 0;
        ConfigureNextButton();

        VP.clip = clip[CurrentClipNumber - 1];
        VP.Prepare();

       // StartCoroutine(LoadAtStart());
        if (CurrentClipNumber <= 3)
        {
            QuestionID="Practice" + CurrentClipNumber.ToString();
        }
        else
        {
           int  tempID = CurrentClipNumber - 3;
            QuestionID = tempID.ToString();
        }
        StartCoroutine(LoadAtStart());
        //StartCoroutine(CountdownToStartVid());
    }
}
