using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class VideoManager : MonoBehaviour
{
    public SaveDatas saveDatas;
    public float curVideoTime;
    public VideoPlayer VPlayer; 
    public bool isPlaying;
    public int CurrentClipNumber;

    public string VideoID;
    public string CorrectAnswer;
    public bool ChoiceASelected;
    public bool ChoiceBSelected;
    public bool ChoiceCSelected;
    public bool ChoiceDSelected;
    public bool IsCorrect;
    public bool VideoIsPreparing;
    public float timer;
    public float ResponseTime;
    public GameObject QObj;
    public TextMeshProUGUI QuestionText;
    public TextMeshProUGUI ChoiceText_1;
    public TextMeshProUGUI ChoiceText_2;
    public TextMeshProUGUI ChoiceText_3;
    public TextMeshProUGUI ChoiceText_4;
    public UnityEngine.UI.Slider ConfidenceSlider;
    public TextMeshProUGUI AnswerStatus;
    string NextVideo;
    public string RootPath;

    public GameObject NextObject;
    public GameObject BackgroundImage;
    public GameObject DeviceInstructions;
    public GameObject IntroductionText;
    int score;
    int totalQuestion;
    // Start is called before the first frame update
    void Start()
    {

        //3seconds freeze frame
        //After the freeze frame done GetSaveFile();
      
       SetStartComponent();
}

    public void SetStartComponent()
    {
        VideoIsPreparing = false;
        IsCorrect = false;
        GetSaveFile();
        TurnOffAllChoice();
        CurrentClipNumber = 1;
        RootPath = Application.persistentDataPath;
        score = 0;
        totalQuestion = 13;
        QObj.SetActive(false);
        AnswerStatus.gameObject.SetActive(false);
        NextObject.SetActive(true);
        BackgroundImage.SetActive(true);

        DeviceInstructions.SetActive(false );
    }



    // Update is called once per frame
    void Update()
    {   curVideoTime = (float)VPlayer.time;
        UpdateVideo();
    }


    void GetSaveFile()
    {

        GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        saveDatas = SaveFileObject.GetComponent<SaveDatas>();
        saveDatas.OnComprehensionTestEnter();
    }

    void UpdateVideo()
    {
        switch (CurrentClipNumber)
        
        {

            case 1:
                //Need to write which vid here

                QuestionText.text = "What should you do at the upcoming junction?";
                ChoiceText_1.text = "a) Stop because there was a Give Way sign";
                ChoiceText_2.text = "b) Go straight over because the light was about to turn Green";
                ChoiceText_3.text = "c) Stop because the light was about to turn Red";
                ChoiceText_4.text = "d) Go straight over because the light was on Yellow and there was time";

                CorrectAnswer = "C";

                VPlayer.loopPointReached += PauseVid;

                if (curVideoTime >= 11.9f)
                { 
                  
                    TimerStart();
                  
                }
                
                break;

            case 2:

                FinalResult();
                break;
        
        
        
        
        }


        
    }

    public void OnNextButtonPressed()
    {
        if (IntroductionText.activeSelf)
        {

        }
        else if (DeviceInstructions.activeSelf)
        {
            NextObject.gameObject.SetActive(false);
            DeviceInstructions.SetActive(false);
            BackgroundImage.SetActive(false);
            LoadClip();

        }
        else if (AnswerStatus.gameObject.activeSelf)
        {
            if (CurrentClipNumber == 13)
            {
                NextObject.gameObject.SetActive(false);
                
            }
            else
            {

                AnswerStatus.gameObject.SetActive(false);
                DeviceInstructions.gameObject.SetActive(true);
                NextObject.GetComponentInChildren<TMP_Text>().text = "Continue";
            }
        }
    }

    IEnumerator LoadAtStart()
    {

        if (CurrentClipNumber == 1)
        {
          //  PlayerObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        }
        if (CurrentClipNumber >= 2)
        {
         //   PlayerObject.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);

        }
        VPlayer.Play();
        yield return new WaitForSeconds(0.2f);
        VPlayer.Pause();

        BackgroundImage.gameObject.SetActive(false);

       // isReady = true;



    }

    public void LoadClip()
    {

    }

    public void GetResponseTime()
    {
        ResponseTime = Time.deltaTime;
    }
    public void TimerStart()
    {
        timer += Time.deltaTime;
    }

    public void PauseVid(VideoPlayer vp)
    {
        QObj.SetActive(true);
        BackgroundImage.SetActive(true);
        QuestionText.gameObject.SetActive(true);
        ChoiceText_1.gameObject.SetActive(true);
        ChoiceText_2.gameObject.SetActive(true);
        ChoiceText_3.gameObject.SetActive(true);
        ChoiceText_4.gameObject.SetActive(true);

       // VPlayer.Pause();
        isPlaying = false;
    }
    public void ContinueVid()
    {
        VPlayer.Play();
        isPlaying = true;


    }
    public void ChangeVideoClip()
    {

    }

   

    //OnlyCheckThisWhenTheConfidenceIsRated
    public void CheckIfAnswerIsCorrect()
    {
        if (ChoiceASelected && CorrectAnswer == "A")
        {
           OnCorrectAnswer();
        }

        if (ChoiceBSelected && CorrectAnswer == "B")
        {
             OnCorrectAnswer();


        }

        if (ChoiceCSelected && CorrectAnswer == "C")
        {
            OnCorrectAnswer();

        }

        if (ChoiceDSelected && CorrectAnswer == "D")
        {
            OnCorrectAnswer();


        }

        if (!IsCorrect) 
        
        {
            IsCorrect = false;
            saveDatas.OnComprehensionTestIncorrect(ResponseTime.ToString(), VideoID, CorrectAnswer, ConfidenceSlider.value.ToString());
            ConfidenceSlider.value = 0;
            TurnOffAllChoice();
            timer = 0;
            ResponseTime = 0;
            AnswerStatus.text = "That answer is incorrect!";

        }
        ConfidenceSlider.gameObject.SetActive(false);

        VideoID = NextVideo;
        totalQuestion++;
        //OTHERWISE IT IS INCORRECT
    }


    void OnCorrectAnswer()
    {
        IsCorrect = true;
        saveDatas.OnComprehensionTestCorrect(ResponseTime.ToString(), VideoID, CorrectAnswer, ConfidenceSlider.value.ToString());
        AnswerStatus.text = "Well done, that is correct!";
        ConfidenceSlider.value = 0;
        TurnOffAllChoice();
        timer = 0;
        ResponseTime = 0;
        score++;
    }

    void TurnOffAllChoice()
    {IsCorrect = false;
        ChoiceASelected=false;
        ChoiceBSelected=false;
        ChoiceCSelected=false;
        ChoiceDSelected=false;
    }

    public void FinalResult()
    {
        AnswerStatus.text = "You got "+ score +" out of " + totalQuestion + "right!";
        NextObject.SetActive(true);
    }

    public void ReturnToMainMenu()
    {

        SceneManager.LoadScene(0);
    }


    #region ChoiceBoolCheck

    public void AnswerIsA()
    {
        ChoiceASelected = true;
    }

    public void AnswerIsB()
    {
        ChoiceBSelected = true;
    }
    public void AnswerIsC()
    {
        ChoiceCSelected = true; 
    }

    public void AnswerIsD()
    {
        ChoiceDSelected = true;
    }
    #endregion

}
