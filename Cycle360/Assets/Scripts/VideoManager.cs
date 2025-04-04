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
    public bool isReady;
    public int timer=3;
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
    public GameObject ResetButton;
    public GameObject PlayerObject;

    public GameObject NextObject;
    public GameObject BackgroundImage;
    public GameObject DeviceInstructions;
    public GameObject IntroductionText;
    public GameObject StartObject;
    public TMP_Text TimerObject;
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
        timer = 3;

        RootPath = Application.persistentDataPath;
        NextObject.SetActive(false);
        score = 0;
        totalQuestion = 0;
        QObj.SetActive(false);
        AnswerStatus.gameObject.SetActive(false);
        isReady = false;    
        BackgroundImage.SetActive(true);

        DeviceInstructions.SetActive(false );
    }



    // Update is called once per frame
    void Update()
    {   curVideoTime = (float)VPlayer.time;
        UpdateVideo();
      
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
                
                QuestionText.text = "What intersection are you approaching?";
                ChoiceText_1.text = "a) T Junction";
                ChoiceText_2.text = "b) Roundabout";
                ChoiceText_3.text = "c) Crossroads";
                ChoiceText_4.text = "d) I am not approaching a junction";

                CorrectAnswer = "B";
           

                VPlayer.loopPointReached += PauseVid;

              
                
                break;

            case 2:


                QuestionText.text = "Is it safe to pass the cyclist in front?";

                ChoiceText_1.text = "a) Yes, there is no oncoming traffic and no cyclist close behind";
                ChoiceText_2.text = "b) No, there is an oncoming cyclist";
                ChoiceText_3.text = "c) No, there is an oncoming runner";
                ChoiceText_4.text = "d) No, there is a cyclist behind who is about to overtake";

                CorrectAnswer = "D";

                VPlayer.loopPointReached += PauseVid;

                break;
            case 3:

                QuestionText.text = "The bike lane has ended, is it legal to ride in the bus lane?";

                ChoiceText_1.text = "a) Yes, cyclists can ride in all bus lanes across Australia";
                ChoiceText_2.text = "b) No, cyclists are never allowed to ride in bus lanes";
                ChoiceText_3.text = "c) It depends, cyclists can use bus lanes but only during peak hours";
                ChoiceText_4.text = "d) It depends, there are different laws for riding in bus lanes in the different the states and territories";


                CorrectAnswer = "D";
                VPlayer.loopPointReached += PauseVid;

                break;
            case 4:
                QuestionText.text = "What should you do when approaching the shared path intersection?";


                ChoiceText_1.text = "a) Speed up to get in front of the pedestrians ";
                ChoiceText_2.text = "b) Sound a bell to make sure the pedestrians see you and continue at the same speed";
                ChoiceText_3.text = "c) Slow down and ensure you give way to pedestrians if appropriate";
                ChoiceText_4.text = "d) Continue at same speed because pedestrians have to give way to cyclists";

                CorrectAnswer = "C";
                VPlayer.loopPointReached += PauseVid;

                break;
            case 5:

                QuestionText.text = "Why are the vehicles in front indicating to merge in to the lane you're in?";

                ChoiceText_1.text = "a) The two lanes are merging in to one lane";
                ChoiceText_2.text = "b) There is an upcoming junction and the vehicles are planning to turn left";
                ChoiceText_3.text = "c) There is upcoming roadwork in the right-hand lane";
                ChoiceText_4.text = "d) There is a stopped vehicle in the right-hand lane waiting to turn right";


                

                CorrectAnswer = "D";
                VPlayer.loopPointReached += PauseVid;
                break;
            case 6:


                QuestionText.text = "Is it safe to enter the roundabout?";

                ChoiceText_1.text = "a) Yes, because the car the car entering the roundabout from the right is taking the first exit so there is space to enter the roundabout safely";
                ChoiceText_2.text = "b) No, because the car entering the roundabout from the right is going straight and has right of way";
                ChoiceText_3.text = "c) No, because there is a car behind about to overtake you before the roundabout";
                ChoiceText_4.text = "d) No, because the car entering the roundabout from the left has right of way";

                CorrectAnswer = "A";
                VPlayer.loopPointReached += PauseVid;
                break;
            case 7:


                QuestionText.text = "Based on the road markings, which lane should you be in if you want to turn left at the intersection?";

                ChoiceText_1.text = "a) Either the left hand lane or the cycle lane ";
                ChoiceText_2.text = "b) The left hand lane with the other vehicles";
                ChoiceText_3.text = "c) The bike lane as cyclists should stay in this lane";
                ChoiceText_4.text = "d) The right hand lane with the other vehicles";

                CorrectAnswer = "B";
                VPlayer.loopPointReached += PauseVid;
                break;
            case 8:

                QuestionText.text = "Why should you slow down for this corner? ";


                ChoiceText_1.text = "a) There is a car pulling out on to the road on the corner";
                ChoiceText_2.text = "b) There is a parked car immediately after the corner on your side of the road";
                ChoiceText_3.text = "c) There is cyclist ahead slowing to take the corner";
                ChoiceText_4.text = "d) There is an intersection immediately after the corner";
       

                CorrectAnswer = "A";
                VPlayer.loopPointReached += PauseVid;
                break;
            case 9:

                QuestionText.text = "Is it safe to move round the cyclist who stopped?";
     
                ChoiceText_1.text = "a) Yes, there is no oncoming traffic and no cyclist close behind";
                ChoiceText_2.text = "b) No, there is an oncoming cyclist";
                ChoiceText_3.text = "c) No, there is a cyclist behind who is about to overtake";
                ChoiceText_4.text = "d) No, there is not enough space to pass";
                 
                CorrectAnswer = "A";
                VPlayer.loopPointReached += PauseVid;
                break;
            case 10:

                QuestionText.text = "Who has right of way in this scenario?";

                ChoiceText_1.text = "a) The cyclist has right of way because the car is entering their lane";
                ChoiceText_2.text = "b) The car has right of way because it is a Stop junction";
                ChoiceText_3.text = "c) The pedestrian has right of way because it is a pedestrian crossing";
                ChoiceText_4.text = "d) The car overtaking the cyclists because they will be the first to the intersection";


              CorrectAnswer = "B";
                VPlayer.loopPointReached += PauseVid;
                break;
            case 11:

                QuestionText.text = "What should you do at the upcoming intersection?";

                ChoiceText_1.text = "a) Stop because there was a Give Way sign";
                ChoiceText_2.text = "b) Go straight over because the light was about to turn Green";
                ChoiceText_3.text = "c) Stop because the light was about to turn Red";
                ChoiceText_4.text = "d) Go straight over because the light was on Yellow and there was time";
        
                CorrectAnswer = "C";
                VPlayer.loopPointReached += PauseVid;
                break;
            case 12:

                QuestionText.text = "Is it safe to pass the dogwalker in front?";

                ChoiceText_1.text = "a) Yes, because the pedestrian has crossed the path with the dog in front of them on a lead";
                ChoiceText_2.text = "b) No, because the pedestrian dropped something in the path and is about to retrieve it";
                ChoiceText_3.text = "c) No, because the pedestrian has a second dog and the lead is across the path";
                ChoiceText_4.text = "d) No, because the dog is off the lead and is about to run across the path";

                CorrectAnswer = "C";
                VPlayer.loopPointReached += PauseVid;
                break;
            case 13:

                QuestionText.text = "Can you go straight ahead?";

                ChoiceText_1.text = "a) Yes, bicyclists are excepted ahead because there is a bike lane";
                ChoiceText_2.text = "b) No, all traffic must turn left because it is a one - way street ahead";
                ChoiceText_3.text = "c) No, only cars are excepted ahead because there is no bike lane";
                ChoiceText_4.text = "d) Yes, all traffic can go straight ahead";

    

                CorrectAnswer = "A";
                VPlayer.loopPointReached += PauseVid;
                FinalResult();

                break;
        
        
        
        
        }


        
    }
    IEnumerator CountdownToStartVid()
    {
        TimerObject.transform.parent.gameObject.SetActive(true);
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
        TimerObject.transform.parent.gameObject.SetActive(false);

        //StopButton.gameObject.SetActive(true);
        VPlayer.Play();
       // isReady = false;
        timer = 3;
        ResetButton.SetActive(true);

        StopCoroutine(CountdownToStartVid());


    }
    public void ResetScenario()
    {
        VPlayer.Stop();
        isPlaying = false;
        DeviceInstructions.SetActive(true);
        NextObject.SetActive(true);
        BackgroundImage.SetActive(true);
        ResetButton.SetActive(false);
    }
    public void OnNextButtonPressed()
    {
        if (IntroductionText.activeSelf)
        {
            IntroductionText.SetActive(false);
            NextObject.GetComponentInChildren<TMP_Text>().text = "Next";

            NextObject.gameObject.SetActive(true);
            DeviceInstructions.SetActive(true);
            StartObject.SetActive(false );
        }
        else if (QuestionText.gameObject.activeSelf)
        {
            NextObject.GetComponentInChildren<TMP_Text>().text = "Record Answer";
            NextObject.gameObject.SetActive(true);
            QuestionText.gameObject.SetActive(false);
            ConfidenceSlider.gameObject.SetActive(true);

        }
        else if (DeviceInstructions.activeSelf)
        {
            //NextObject.GetComponentInChildren<TMP_Text>().text = "Next";

            NextObject.gameObject.SetActive(false);
            DeviceInstructions.SetActive(false);
            BackgroundImage.SetActive(false);
            LoadClip();
           //
           //isReady = true;
        }

        else if (ConfidenceSlider.gameObject.activeSelf)
        {
            NextObject.GetComponentInChildren<TMP_Text>().text = "Next";

            ConfidenceSlider.gameObject.SetActive(false);
            NextObject.gameObject.SetActive(true);

            
            CheckIfAnswerIsCorrect();
            if (CurrentClipNumber == 3 || CurrentClipNumber==14)
            {
                // FinalResult
                FinalResult();
            }
            AnswerStatus.gameObject.SetActive(true);

        }
        else if (AnswerStatus.gameObject.activeSelf)
        {
            //THERE ARE TWO HERE RECHECK;
            if (CurrentClipNumber == 14)
            {
                NextObject.gameObject.SetActive(false);
                
            }
            else
            {

                AnswerStatus.gameObject.SetActive(false);
                DeviceInstructions.gameObject.SetActive(true);
                NextObject.GetComponentInChildren<TMP_Text>().text = "Continue";
                NextObject.gameObject.SetActive(true);


            }
        }
    }

    IEnumerator LoadAtStart()
    {
        adjustRotation();
     
        VideoIsPreparing = false;

        VPlayer.Play();
        yield return new WaitForSeconds(0.2f);
        VPlayer.Pause();
      //  isReady = true;
        BackgroundImage.gameObject.SetActive(false);

        isReady = true;
        IsCorrect = false;


    }

    public void adjustRotation()
    {
        switch (CurrentClipNumber)
        {
            case 1:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 65.0f, 0.0f);
                break;
            case 2:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 65.0f, 0.0f);

                break;
            case 3:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 65.0f, 0.0f);

                break;
            case 4:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 65.0f, 0.0f);

                break;
            case 5:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 65.0f, 0.0f);

                break;
            case 6:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

                break;
            case 7:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 65.0f, 0.0f);


                break;
            case 8:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 65.0f, 0.0f);

                break;
            case 9:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 65.0f, 0.0f);

                break;
            case 10:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 65.0f, 0.0f);

                break;
            case 11:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 65.0f, 0.0f);

                break;
            case 12:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);

                break;
            case 13:
                PlayerObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

                break;

        }

    }

    public void LoadClip()
    {
        score = 0;
        string tempPath = Path.Combine(RootPath, "ComprehensionTest" + CurrentClipNumber + ".mp4");
        VPlayer.url = tempPath;
        VPlayer.controlledAudioTrackCount = 1;

        VPlayer.Prepare();

        // StartCoroutine(LoadAtStart());
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

    public void GetResponseTime()
    {
        ResponseTime = Time.deltaTime;
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
        ResetButton.SetActive(false);
       // VPlayer.Pause();
        isPlaying = false;
    }
    public void ContinueVid()
    {
        VPlayer.Play();
        isPlaying = true;


    }
  

   

    //OnlyCheckThisWhenTheConfidenceIsRated
    public void CheckIfAnswerIsCorrect()
    {
        if (ChoiceASelected && CorrectAnswer == "A")
        {
           OnCorrectAnswer();
            IsCorrect = true;
            return;
        }
      
      

        if (ChoiceBSelected && CorrectAnswer == "B")
        {
             OnCorrectAnswer();
            IsCorrect = true;

            return ;
        }
       

        if (ChoiceCSelected && CorrectAnswer == "C")
        {
            OnCorrectAnswer();
            IsCorrect = true;
            return;
        }
       

        if (ChoiceDSelected && CorrectAnswer == "D")
        {
            OnCorrectAnswer();
            IsCorrect = true;
            return;
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
            ConfidenceSlider.gameObject.SetActive(false);
            CurrentClipNumber++;
            // VideoID = NextVideo;
            totalQuestion++;
            //DeviceInstructions.gameObject.SetActive(true);
            AnswerStatus.gameObject.SetActive(true);
            BackgroundImage.gameObject.SetActive(true);
            NextObject.gameObject.SetActive(true);
            //OTHERWISE IT IS INCORRECT
        }

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
        ConfidenceSlider.gameObject.SetActive(false);
        CurrentClipNumber++;
        // VideoID = NextVideo;
        totalQuestion++;
        //DeviceInstructions.gameObject.SetActive(true);
        AnswerStatus.gameObject.SetActive(true);
        BackgroundImage.gameObject.SetActive(true);
        NextObject.gameObject.SetActive(true);
        //OTHERWISE IT IS INCORRECT
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
