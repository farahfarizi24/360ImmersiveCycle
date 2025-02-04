using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class VideoPlayerScript : MonoBehaviour
{
    SaveDatas SaveObj;
    public HazardTracker RedCar_hazardTracker;
    public bool HazardisSet_RC;

    public HazardTracker ParkedCar_hazardTracker;
    public bool HazardisSet_PC;

    public HazardTracker Roundabout_hazardTracker;
    public bool HazardisSet_Roundabout;

    public HazardTracker[] Prac2Hazards;
    public bool[] Prac2HazardisSet;

    public HazardTracker[] Prac3Hazards;
    public bool[] Prac3HazardisSet;

    public GameObject BackgroundUI;
    public GameObject Instruction_1;
    public GameObject Instruction_2;
    public GameObject IntermissionScene;
    public TextMeshProUGUI FinalFeedback;
    public TextMeshProUGUI TestFeedback;
    public GameObject NextButton;
    public GameObject CompleteButton;
    private int HazardTotalCount;
    public float curFrame;
   // public string WhichScene;
    public VideoPlayer video;
    public bool isPlaying;
    public bool isReady = false;
    public int CurPlayingClip=0;
    public VideoClip[] Projectionclips;
    public GameObject PlayerObject;
  //  public TextMeshProUGUI pauseButton;
    // Start is called before the first frame update
  

    void Start()
    {
        SetStartComponents();

    }



    public void SetStartComponents()
    {
        PlayerObject.transform.eulerAngles = new Vector3 (0.0f,90.0f,0.0f);
        //CurPlayingClip = 0;
        HazardisSet_RC = false;
        RedCar_hazardTracker.gameObject.SetActive(false);
        HazardisSet_PC = false;
        ParkedCar_hazardTracker.gameObject.SetActive(false);
        HazardisSet_Roundabout = false;
        Roundabout_hazardTracker.gameObject.SetActive(false);
       Instruction_1.gameObject.SetActive(true);
        Instruction_2.gameObject.SetActive(false);
        IntermissionScene.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(true);
        NextButton.gameObject.GetComponentInChildren <TextMeshProUGUI>().text = "Next";

    isReady = false;
        curFrame = 0;
        isPlaying = false;

    }

    public void NextButtonClicked()
    {
        if (Instruction_1.activeSelf)
        {
            Instruction_1 .SetActive(false);
            Instruction_2 .SetActive(true);

        }
        else if (Instruction_2.activeSelf) 
        {
            Instruction_2.SetActive(false);
            NextButton.gameObject.SetActive(false);
            BackgroundUI.gameObject.SetActive(false);
            LoadVid();


        }
       else  if (TestFeedback.gameObject.activeSelf)
        {
            if (CurPlayingClip >= Projectionclips.Length-1) 
            {//NextButton.gameObject.SetActive(false);
               
                FinalFeedback.gameObject.SetActive(false) ;
                TestFeedback.gameObject.SetActive(false) ;
                //FinalScene
            }
            else
            {
                SaveObj.TotalCorrectWithinQuestions = 0;
                SaveObj.TotalHazardWithinQuestion = 0;



                TestFeedback.gameObject.SetActive(false);
                BackgroundUI.gameObject.SetActive(false);

                NextButton.gameObject.SetActive(false);
                //SetHazardAllToNull and BoolTo false
                //
                CurPlayingClip++;

                if(CurPlayingClip >= 1)
                {
                    PlayerObject.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                }

                LoadVid();
            }
      
            
            
            
            //Load next;
        }
    }

    IEnumerator LoadAtStart()
    {
        yield return new WaitForSeconds(1f);
        video.Pause();
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVideoFrame();
    }

    public void UpdateVideoFrame()
    {
        curFrame = (float)video.time;
        Debug.Log("Time:" + curFrame);
    
                PerceptionVP();
         //       video.loopPointReached += ShowEndScene;
            
        
    }

    #region PlaybackMethods
    public void PlaybackManager()
    {
        if (isPlaying) 
        { PauseVid(); }
        else { ContinueVid(); }
    }
    
    public void LoadVid()
    {
        video.clip = Projectionclips[CurPlayingClip];
        video.Prepare();
        StartCoroutine(LoadAtStart());
    }
    public void PauseVid()
    {
        video.Pause();
        isPlaying = false;
      
    }
    public void ContinueVid()
    {
       video.Play();
        isPlaying = true;


    }
    #endregion

    #region VideoManager


    void PerceptionVP()
    {
        GetSaveFile();

        switch (CurPlayingClip)
        {
            case 0:
                //Practice 1


                if (RedCar_hazardTracker != null)
                {
                    if (curFrame >= 15.0f && !HazardisSet_RC)
                    {

                        RedCar_hazardTracker.gameObject.SetActive(true);
                        RedCar_hazardTracker.StartMove(new Vector3(-221.100006f, -6.80000019f, -110.599998f), new Vector3(14.3000002f, -6.80000019f, -36.9000015f), 5.0f);
                        HazardisSet_RC = true;
                        //   question++;
                    }
                    if (curFrame >= 25.0f && HazardisSet_RC)
                    {
                        RedCar_hazardTracker.gameObject.SetActive(false);
                    }
                }



                if (ParkedCar_hazardTracker != null)
                {
                    if (curFrame >= 18.0f && !HazardisSet_PC)
                    {

                        ParkedCar_hazardTracker.gameObject.SetActive(true);
                        ParkedCar_hazardTracker.StartMove(new Vector3(19.7900009f, -1.29999995f, -1.17999995f)
        , new Vector3(-2.70000005f, -0.310000002f, -7.76000023f), 2.0f);
                        HazardisSet_PC = true;
                        //  question++;
                    }
                    if (curFrame >= 20.0f && HazardisSet_PC)
                    {
                        ParkedCar_hazardTracker.gameObject.SetActive(false);
                    }
                }

                if (Roundabout_hazardTracker != null)
                {
                    if (curFrame >= 16.0f && !HazardisSet_Roundabout)
                    {

                        Roundabout_hazardTracker.gameObject.SetActive(true);
                        Roundabout_hazardTracker.StartMove(new Vector3(14.1499996f, 0.200000003f, -7.05999994f)
        , new Vector3(3.07999992f, 0.939999998f, -9.89999962f)
        , 9.0f); 
                        
                        SaveObj.TotalHazardWithinQuestion = 3;
                        HazardTotalCount += SaveObj.TotalHazardWithinQuestion;
                        HazardisSet_Roundabout = true;
                        //  question++;
                    }
                    if (curFrame >= 25.0f && HazardisSet_Roundabout)

                    {

                        Roundabout_hazardTracker.gameObject.SetActive(false);
                    }
                }

                break;
                case 1:


                Debug.Log("Prac 2 is active");
                if ( Prac2Hazards[0]!=null)
                {
                    if (curFrame >= 7.7f && !Prac2HazardisSet[0])
                    {

                        Prac2Hazards[0].gameObject.SetActive(true);
                        Prac2Hazards[0].StartMove(new Vector3(43.5f, -7.8f, - 151.2f), 
                            new Vector3(65.7f, -29.387f, -46.4487f), 1.8f);
                        Prac2HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 9.5f && Prac2HazardisSet[0])
                    {
                        Prac2Hazards[0].gameObject.SetActive(false);
                    }
                }

                if (  Prac2Hazards[1] != null)
                {
                    if (curFrame >= 10.0f && !Prac2HazardisSet[1])
                    {

                        Prac2Hazards[1].gameObject.SetActive(true);
                        Prac2Hazards[1].StartMove(new Vector3(-71.99998474121094f, -9.98709487915f, -438.77f), 
                            new Vector3(1.5f, -53.087f, -32.27f), 7.8f);
                        Prac2HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 18.0f && Prac2HazardisSet[1])
                    {
                        Prac2Hazards[1].gameObject.SetActive(false);
                    }
                }
                if ( Prac2Hazards[2] != null)
                {
                    if (curFrame >= 16.0f && !Prac2HazardisSet[2])
                    {

                        Prac2Hazards[2].gameObject.SetActive(true);
                        Prac2Hazards[2].StartMove(new Vector3(-218.499984f, 99.11289978f, -181.67f), 
                            new Vector3(6.1f, -11.287f, -110.27f), 5.0f);
                        Prac2HazardisSet[2] = true;
                        //   question++;
                    }
                    if (curFrame >= 21.0f && Prac2HazardisSet[2])
                    {
                        Prac2Hazards[2].gameObject.SetActive(false);
                    }
                }
                if ( Prac2Hazards[3] != null)
                {
                    if (curFrame >= 14.0f && !Prac2HazardisSet[3])
                    {

                        Prac2Hazards[3].gameObject.SetActive(true);
                        Prac2Hazards[3].StartMove(new Vector3(-13.6f, -3.0f, -60.3f), new Vector3(-26.0f, -3.0f, -172.0f), 3.0f);
                        Prac2HazardisSet[3] = true;
                        //   question++;
                    }
                    if (curFrame >= 23.0f && Prac2HazardisSet[3])
                    {
                        Prac2Hazards[3].gameObject.SetActive(false);
                    }
                }

                break;
                case 2:
                Debug.Log("Prac 3 is active");
                break;
        }



       
        video.loopPointReached += EndofQuestion;










    }



    
    #endregion
    


 
public void ShowEndScene(VideoPlayer vp)
    {
        FinalFeedback.gameObject.SetActive(true);
        SaveObj.TotalHazardOnTheTest = HazardTotalCount;
        if (CurPlayingClip == 2)
        {
            FinalFeedback.text = "You identified " + SaveObj.TotalCorrectWithinTheTest + " out of " +
     SaveObj.TotalHazardOnTheTest + " hazards in the practice test.";
            NextButton.GetComponentInChildren<TMP_Text>().text = "Start Test";
        }
        else
        {
            NextButton.GetComponentInChildren<TMP_Text>().text = "Complete Test";
        }
   
      

    }

    void EndofQuestion(VideoPlayer vp)
    {
        GetSaveFile();
        isPlaying = false;

        BackgroundUI.gameObject.SetActive(true);
        NextButton.gameObject.SetActive(true);
       TestFeedback.gameObject.SetActive(true);
        TestFeedback.text = "You identified " + SaveObj.TotalCorrectWithinQuestions + " out of " +
          SaveObj.TotalHazardWithinQuestion + " hazards in the video";

        if(CurPlayingClip==2 || CurPlayingClip == 13)
        {
            ShowEndScene(vp);
        }

    }

    void GetSaveFile()
    {

        GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        SaveObj = SaveFileObject.GetComponent<SaveDatas>();
    }
}
