using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.IO;

public class PercTest2 : MonoBehaviour
{
    public SaveDatas saveDatas;
    public float curVideoTime;
    public VideoPlayer VPlayer;
    public bool isPlaying;
    public int CurrentClipNumber;

    public string VideoID;
    public string CorrectAnswer;
    public bool IsCorrect;
    public bool VideoIsPreparing;
    public bool isReady;
    public int timer = 3;
    public float ResponseTime;
    public GameObject[] Hazards;
    public string RootPath;

    public GameObject PlayerObject;
    public GameObject NextObject;
    public GameObject BackgroundImage;
    public GameObject DeviceInstructions;
    public GameObject IntroductionText_1;
  //  public GameObject IntroductionText_2;
    public TMP_Text TimerObject;
    public GameObject AnswerFeedback;
    int score;
    int totalQuestion;
    // Start is called before the first frame update
    void Start()
    {
        SetStartComponents();
    }

    // Update is called once per frame
    void Update()
    {
        curVideoTime = (float)VPlayer.time;
        Debug.Log("Time:" + curVideoTime);

        PerceptionVP();
        if (VPlayer.isPrepared && VideoIsPreparing)
        {
            StartCoroutine(LoadAtStart());
            // VideoIsPreparing = false;
        }
        if (isReady)
        {
            isReady = false;
            NextObject.SetActive(false);
            timer = 3;
            StartCoroutine(CountdownToStartVid());
        }
    }

    IEnumerator CountdownToStartVid()
    {
        while (timer > 0)
        {
            isReady = false;

            Debug.Log("Timer start " + timer);
            TimerObject.text = timer.ToString();
            yield return new WaitForSeconds(1f);

            timer--;
        }
        TimerObject.text = "Go!";
        //Provide 3 seconds break before Starting to play video
        //yield return new WaitForSeconds(3f);
        yield return new WaitForSeconds(1f);
        TimerObject.text = "";
        //StopButton.gameObject.SetActive(true);
        VPlayer.Play();
        // isReady = false;
        timer = 3;
        StopCoroutine(CountdownToStartVid());


    }
    public void SetStartComponents()
    {
        RootPath = Application.persistentDataPath;

        PlayerObject.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
        //CurPlayingClip = 0;

        IntroductionText_1.gameObject.SetActive(true);
       
        DeviceInstructions.gameObject.SetActive(false);
        //NextObject.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        BackgroundImage.SetActive(true);
        NextObject.gameObject.SetActive(true);
        AnswerFeedback.SetActive(false);
        timer = 0;
        ResponseTime = 0.0f;
        isReady = false;
        curVideoTime = 0;
        isPlaying = false;
        CurrentClipNumber = 1;
    }


    public void NextButtonClicked()
    {
        if (IntroductionText_1.activeSelf)
        {
            IntroductionText_1.SetActive(false);
            NextObject.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
            NextObject.gameObject.SetActive(true);
            BackgroundImage.SetActive(true);
            DeviceInstructions.SetActive(true);

        }
        else if (DeviceInstructions.activeSelf)
        {
            NextObject.gameObject.SetActive(false);
            DeviceInstructions.SetActive(false);
            BackgroundImage.SetActive(false);
            LoadVid();

        }

        else if (AnswerFeedback.gameObject.activeSelf)
        {
            if (CurrentClipNumber == 13)
            {

                //Add more in the feedback
                AnswerFeedback.gameObject.SetActive(false);
                //NextButton.gameObject.SetActive(false);
                //FinalScene
            }
            else
            {
                saveDatas.TotalCorrectWithinQuestions = 0;
                saveDatas.TotalHazardWithinQuestion = 0;



                AnswerFeedback.gameObject.SetActive(false);
                BackgroundImage.gameObject.SetActive(false);

                NextObject.gameObject.SetActive(false);
                //SetHazardAllToNull and BoolTo false
                //
                CurrentClipNumber++;

                if (CurrentClipNumber >= 2)
                {
                    PlayerObject.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                }

                LoadVid();
            }


            

            //Load next;
        }

    }

    public void LoadVid()
    {
        string tempPath = Path.Combine(RootPath, "PerceptionTest" + CurrentClipNumber + ".mp4");
        VPlayer.url = tempPath;
        VPlayer.Prepare();


        if (CurrentClipNumber <= 3)
        {
            VideoID = "Practice" + CurrentClipNumber.ToString();
        }
        else
        {
            int tempID = CurrentClipNumber - 3;
            VideoID = tempID.ToString();
        }
        VideoIsPreparing = true;

    }

    IEnumerator LoadAtStart()
    {
        VideoIsPreparing = false;

        VPlayer.Play();
        yield return new WaitForSeconds(0.2f);
        VPlayer.Pause();
        //  isReady = true;
        BackgroundImage.gameObject.SetActive(false);

        isReady = true;
    }
    public void PauseVid()
    {
        VPlayer.Pause();
        isPlaying = false;

    }
    void PerceptionVP()
    {
        GetSaveFile();

        switch (CurrentClipNumber)
        {
            case 1:
                if (curVideoTime >= 19.0f && VPlayer.isPlaying)
                {
                    PauseVid();
                }
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
    void GetSaveFile()
    {

        GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        saveDatas = SaveFileObject.GetComponent<SaveDatas>();
    }
}
