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
    public float totalScore;
    public float ThisQuestionScore;
    public float curVidTime;
    public float buttonHitTime;
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
    public GameObject ResetButton;
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
        ResetButton.SetActive(false);
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
        if (!isChanging)
        {
            savingScript.OnProjectionTestResponse(QuestionID, "No Response", "NA", 0, curVidTime);
            totalScore += 0;
            StopButton.gameObject.SetActive(false);
            EndofVid();
            // isChanging = true;
         //   OnNoButtonPress();
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
        buttonHitTime = curVidTime;

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
        ResetButton.SetActive(false);

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
            ResetButton.SetActive(false);

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
            ResetButton.SetActive(false);

            QuestionFeedback.SetActive(true);
            FinalFeedback.SetActive(true);
            BackgroundUI.SetActive(true);
            
            //SHOWCASE END SCREEN
        }
    }

    public float CalculateScore()
    {
      
        switch (CurrentClipNumber)
        {
            case 1:

                CalculateScore(13.0f,16.0f,buttonHitTime);
                break;


                case 2:
                CalculateScore(8.0f, 10.0f, buttonHitTime);
                break;
                
                case 3:
                CalculateScore(9.0f, 12.0f, buttonHitTime);

                break;
                case 4:
                CalculateScore(9.0f, 11.0f, buttonHitTime);

                break;
            case 5:
                CalculateScore(8.0f, 10.0f, buttonHitTime);

                break;
                case 6:
                CalculateScore(13.0f, 15.0f, buttonHitTime);

                break;
                case 7:
                CalculateScore(7.0f, 10.0f, buttonHitTime);

                break;
                case 8:
                CalculateScore(8.0f, 10.0f, buttonHitTime);

                break;
                case 9:
                CalculateScore(14.0f, 16.0f, buttonHitTime);

                break;
                case 10:
                CalculateScore(11.0f, 13.0f, buttonHitTime);

                break;
                case 11:
                CalculateScore(8.0f, 10.0f, buttonHitTime);

                break;
                case 12:
                CalculateScore(13.0f, 15.0f, buttonHitTime);

                break;
                case 13:
                CalculateScore(11.0f, 13.0f, buttonHitTime);

                break;


        }
        return ThisQuestionScore;
    }

    public void CalculateScore(float startTime, float endTime, float ButtonPressTime)
    {
        if (ButtonPressTime < startTime || ButtonPressTime > endTime)
        {
            ThisQuestionScore = 0.0f;
            if(ButtonPressTime < startTime)
            {
                ResOutcome = "Early";
                QuestionFeedback.GetComponentInChildren<TMP_Text>().text = "You responded too early";
            }
            else
            {
                ResOutcome = "Very late";
                QuestionFeedback.GetComponentInChildren<TMP_Text>().text = "You responded too late";
            }
            return;
        }
            
        if (ButtonPressTime == startTime)
        {

            ThisQuestionScore = 10.0f;
            ResOutcome = "Correct";
            QuestionFeedback.GetComponentInChildren<TMP_Text>().text = "You responded correctly and in a safe manner, well done";
            return;
        }
        if (ButtonPressTime == endTime)
        {
            ThisQuestionScore = 1.0f;
            ResOutcome = "Late";
            QuestionFeedback.GetComponentInChildren<TMP_Text>().text = "You responded late";
            return;
        }
          

        ThisQuestionScore = 10 - 9 * (ButtonPressTime - startTime) / (endTime - startTime);
        ResOutcome = "Correct";
        QuestionFeedback.GetComponentInChildren<TMP_Text>().text = "You responded correctly and in a safe manner, well done";

        return;
    }
    public void ResetScenario()
    {
        VP.Stop();
        SetDeviceInstruction.SetActive(true);
        NextButton.SetActive(true);
        BackgroundUI.SetActive(true);
        ResetButton.SetActive(false);
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
        ResetButton.SetActive(true);
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
        //ADJUST PLAYER ROTATION
       
            if (CurrentClipNumber == 1|| CurrentClipNumber==2 || CurrentClipNumber == 5
            || CurrentClipNumber == 6 || CurrentClipNumber == 7 || CurrentClipNumber == 8
            || CurrentClipNumber == 9 || CurrentClipNumber == 10 || CurrentClipNumber == 11
            || CurrentClipNumber == 12 || CurrentClipNumber == 13)
            {
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 60.0f, 0.0f);

            }
            if(CurrentClipNumber == 3)
           {
            PlayerObject.transform.eulerAngles = new Vector3(0.0f, -150.0f, 0.0f);

           }
        if (CurrentClipNumber == 4)
        {
            PlayerObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        }
        if (CurrentClipNumber == 9)
        {
            PlayerObject.transform.eulerAngles = new Vector3(0.0f, 110.0f, 0.0f);

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
        string tempPath = Path.Combine(RootPath, "ProjectionTest" + CurrentClipNumber + ".mp4");
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
