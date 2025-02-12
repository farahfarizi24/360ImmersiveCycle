using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public GameObject SetDeviceInstruction;
    public GameObject NextButton;
    public GameObject StopButton;
    public GameObject BackgroundUI;
    public GameObject FinalFeedback;
    public GameObject QuestionFeedback;
    public string RootPath;
    public bool VideoBeingPrepared;
    // Start is called before the first frame update
    void Start()
    {
        SetStartComponent();
      
    }

    public void SetStartComponent()
    {
        VideoBeingPrepared = false;
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
        RootPath = Application.persistentDataPath;
        // StartCoroutine(LoadAtStart());
        SetDeviceInstruction.gameObject.SetActive(false);
        isReady = false ;
        InstructionText.gameObject.SetActive(true);
        NextButton.GetComponentInChildren<TMP_Text>().text = "Start";
        NextButton.gameObject.SetActive(true);
        BackgroundUI.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (VP.isPlaying)
        {
            VP.loopPointReached += SwitchVid;

        }
        curVidTime = (float)VP.time;
        if (VP.isPrepared && VideoBeingPrepared)
        {
            StartCoroutine(LoadAtStart());
            VideoBeingPrepared=false;
        }
        if (isReady)
        {
            StartCoroutine(CountdownToStartVid());
        }
    }

    private void SwitchVid(VideoPlayer player)
    {
        if (VP.isPlaying && CurrentClipNumber!=13)
        {

            isChanging = true;
            VP.Stop();
            OnNoButtonPress();
            StopButton.SetActive(false);
            NextButton.SetActive(true);
            NextButton.GetComponentInChildren<TMP_Text>().text = "Next";
            QuestionFeedback.SetActive(true);
            BackgroundUI.SetActive(true);    
            CurrentClipNumber++;
           // LoadClip();

        }

        if(VP.isPlaying && CurrentClipNumber == 13)
        {
            isChanging = true;
            OnNoButtonPress();
            StopButton.SetActive(false);
            NextButton.GetComponent<TMP_Text>().text = "Finish Test";
            NextButton.SetActive(true);

            QuestionFeedback.SetActive(true);
            FinalFeedback.SetActive(true);
            BackgroundUI.SetActive(true);
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

        EndofVid();

        
    }

    public void EndofVid()
    {

        if (!isChanging && CurrentClipNumber != 13)
        {

            isChanging = true;
            VP.Stop();

            StopButton.SetActive(false);
            NextButton.GetComponentInChildren<TMP_Text>().text = "Next";

            NextButton.SetActive(true);
            QuestionFeedback.SetActive(true);
            BackgroundUI.SetActive(true);
            CurrentClipNumber++;
           // LoadClip();

        }

        if (!isChanging && CurrentClipNumber == 13)
        {
            isChanging = true;
            VP.Stop();
            StopButton.SetActive(false);
            NextButton.GetComponentInChildren<TMP_Text>().text = "Finish Test";
            NextButton.SetActive(true);

            QuestionFeedback.SetActive(true);
            FinalFeedback.SetActive(true);
            BackgroundUI.SetActive(true);
            
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
            isReady = false;

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
            NextButton.GetComponentInChildren<TMP_Text>().text = "Start Practice Test";

            NextButton.gameObject.SetActive(true);
            BackgroundUI.gameObject.SetActive(true);
            InstructionText.gameObject.SetActive(false);
            SetDeviceInstruction.gameObject.SetActive(true);
            //  LoadClip();    IEnumerator CountdownToStartVid()
          //  StartCoroutine(CountdownToStartVid());

        }else if (SetDeviceInstruction.gameObject.activeSelf)
        {
            NextButton.gameObject.SetActive(false);
          //  BackgroundUI.gameObject.SetActive(false);
            SetDeviceInstruction.gameObject.SetActive(false);
            LoadClip();

        }
        else if (QuestionFeedback.gameObject.activeSelf)
        {
           
            if (CurrentClipNumber == 13)
            {
                NextButton.gameObject.SetActive(false);
                BackgroundUI.gameObject.SetActive(false);
                QuestionFeedback.gameObject.SetActive(false);
                FinalFeedback.gameObject.SetActive(false);
                //END
                Application.Quit();
            }
            else
            {
                QuestionFeedback.gameObject.SetActive(false);

                SetDeviceInstruction.gameObject.SetActive(true );
                NextButton.GetComponentInChildren<TMP_Text>().text = "Continue";
                //StartCoroutine(CountdownToStartVid());
            }
        }
    }
    IEnumerator LoadAtStart()
    {
       
            if (CurrentClipNumber == 1)
            {
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

            }
            if (CurrentClipNumber >= 2)
            {
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);

            }
            VP.Play();
            yield return new WaitForSeconds(0.2f);
            VP.Pause();

            BackgroundUI.gameObject.SetActive(false);

            isReady = true;
        
       
        
    }
    public void LoadClip()
    {
        ThisQuestionScore = 0;
        string tempPath = Path.Combine(RootPath, "Projection_Test_" + CurrentClipNumber + ".mp4");
        VP.url = tempPath;
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
        VideoBeingPrepared = true;
      //  StartCoroutine(LoadAtStart());
        //StartCoroutine(CountdownToStartVid());
    }
}
