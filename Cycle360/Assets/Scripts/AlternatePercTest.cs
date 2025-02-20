using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class AlternatePercTest : MonoBehaviour
{
    //https://discussions.unity.com/t/playing-large-video-files-in-gear-vr-in-unity-project/673596/4
    //https://discussions.unity.com/t/retrieving-and-playing-a-video-file-stored-locally-on-an-android-vr-headset-oculus-quest/224309/3

    
    public GameObject PlayerObject;
    public int CurrentClipNumber;
    public TMP_Text TimerText;
    public int CountdownTimer;
    public VideoPlayer VP;
    public bool isChanging = false;
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
        isReady = false;
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
            VideoBeingPrepared = false;
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

    IEnumerator CountdownToStartVid()
    {
        while (CountdownTimer > 0)
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

}
