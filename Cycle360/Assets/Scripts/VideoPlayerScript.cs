using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.IO;
public class VideoPlayerScript : MonoBehaviour
{
    SaveDatas SaveObj;
  

   
    public HazardTracker[] Prac1Hazards;
    public bool[] Prac1HazardisSet;

    public HazardTracker[] Prac2Hazards;
    public bool[] Prac2HazardisSet;

    public HazardTracker[] Q1Hazards;
    public bool[] Q1HazardisSet;
    public HazardTracker[] Q2Hazards;
    public bool[] Q2HazardisSet;
    public HazardTracker[] Q3Hazards;
    public bool[] Q3HazardisSet;
    public HazardTracker[] Q4Hazards;
    public bool[] Q4HazardisSet;
    public HazardTracker[] Q5Hazards;
    public bool[] Q5HazardisSet;


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
    public string RootPath;
   // public string WhichScene;
    public VideoPlayer video;
    public bool isPlaying;
    public bool isReady = false;
    public int CurPlayingClip=0;
    public VideoClip[] Projectionclips;
    public GameObject PlayerObject;
    public GameObject DeviceInstruction;
    public GameObject ResetButton;
   
    //  public TextMeshProUGUI pauseButton;
    // Start is called before the first frame update


    void Start()
    {
      
        SetStartComponents();

    }

    //This code is unused, but it is the base of reading video from a file
    public void RetrieveClips()
    {
        RootPath = Application.persistentDataPath;
        for(int i = 0; i <=3; i++)
        {
            string tempPath = Path.Combine(RootPath, "Ordered_Dynamic" + i + ".mp4");
            if (File.Exists(tempPath))
            {
                video.url = tempPath;
                // for testing
                //work
                //video.Play();
            }
        }

    }
    public void SetStartComponents()
    {
        RootPath = Application.persistentDataPath;

        PlayerObject.transform.eulerAngles = new Vector3 (0.0f,180.0f,0.0f);
        //CurPlayingClip = 0;
       
       Instruction_1.gameObject.SetActive(true);
        Instruction_2.gameObject.SetActive(false);
        IntermissionScene.gameObject.SetActive(false);
        NextButton.gameObject.SetActive(true);
        NextButton.gameObject.GetComponentInChildren <TextMeshProUGUI>().text = "Next";
        DeviceInstruction.SetActive(false);
        ResetButton.SetActive(false);
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
            DeviceInstruction.SetActive(true);


        }
        else if (DeviceInstruction.activeSelf)
        {
            NextButton.gameObject.SetActive(false);
            DeviceInstruction.SetActive(false);
            BackgroundUI.SetActive(false);
            LoadVid();

        }
        else  if (TestFeedback.gameObject.activeSelf)
        {
            if (CurPlayingClip ==7) 
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
                DeviceInstruction.SetActive(true);

                //SetHazardAllToNull and BoolTo false
                //
                CurPlayingClip++;


              

               
            }
      
            
            
            
            //Load next;
        }
    }
    public void ResetScenario()
    {
        video.Stop();
        isPlaying = false;
        DeviceInstruction.SetActive(true);
        NextButton.SetActive(true);
        BackgroundUI.SetActive(true);
        ResetButton.SetActive(false);
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
        else { ContinueVid();
            ResetButton.SetActive(true);
        }
    }
    
    public void LoadVid()
    {
        int VidToPlay = CurPlayingClip + 1;
        string tempPath = Path.Combine(RootPath, "Ordered_Dynamic" + VidToPlay + ".mp4");
        video.url = tempPath;
        //video.clip = Projectionclips[CurPlayingClip];
        if (VidToPlay == 2)
        {
            PlayerObject.transform.eulerAngles = new Vector3(0.0f, 80.0f, 0.0f);
            
        }
        if (VidToPlay == 3)
        {
            PlayerObject.transform.eulerAngles = new Vector3(0.0f, -180.0f, 0.0f);
        }
        if (VidToPlay == 4)
        {
            PlayerObject.transform.eulerAngles = new Vector3(0.0f, 80.0f, 0.0f);
        }
        video.Prepare();
        StartCoroutine(LoadAtStart());
    }
    public void PauseVid()
    {
        video.Pause();
        isPlaying = false;
        ResetButton.SetActive(false);

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

                if (Prac1Hazards[0] != null)//Ped
                {
                    if (curFrame >= 4.0f && !Prac1HazardisSet[0])
                    {
                        

                        Prac1Hazards[0].gameObject.SetActive(true);
                        Prac1Hazards[0].StartMove(new Vector3(34.1f, -15.4f, -131.8f),
                            new Vector3(66f, -23.7f, -32.9f), 2.0f);
                        Prac1HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 6.0f && Prac1HazardisSet[0])
                    {
                        Prac1Hazards[0].gameObject.SetActive(false);
                     
                    }
                }

                if (Prac1Hazards[1] != null)//Speedbump
                {
                    if (curFrame >= 9.0f && !Prac1HazardisSet[1])
                    {

                        Prac1Hazards[1].gameObject.SetActive(true);
                        Prac1Hazards[1].StartMove(new Vector3(-13.4f, 0.0f, -151.2f),
                            new Vector3(-25.5f, -17.7f, -89.8f), 5f);
                        Prac1HazardisSet[1] = true;
                        //   question++;
                     
                    }
                    if (curFrame >= 14.5f && Prac1HazardisSet[1])
                    {
                      
                   
                        Prac1Hazards[1].gameObject.SetActive(false);
                    }
                }

                if (Prac1Hazards[2] != null)//PedestrianCrossing
                {
                    if (curFrame >= 13.0f && !Prac1HazardisSet[2])
                    {
                      
                        Prac1Hazards[2].gameObject.SetActive(true);
                        Prac1Hazards[2].StartMove(new Vector3(-96.5f, 0.52f, -196.7f),
                            new Vector3(0.3f, -13.6f, -107.6f), 9f);
                        Prac1HazardisSet[2] = true;
                        //   question++;
                    }
                    if (curFrame >= 19.0f && Prac1HazardisSet[2])
                    {
                       
                        Prac1Hazards[2].gameObject.SetActive(false);
                    }
                }

                if (Prac1Hazards[3] != null)//Roundabout
                {
                    if (curFrame >= 14.0f && !Prac1HazardisSet[3])
                    {

                        Prac1Hazards[3].gameObject.SetActive(true);
                        Prac1Hazards[3].StartMove(new Vector3(-8.87f, 10.9f, -150f),
                            new Vector3(-9.4f, -2f, -92.4f), 6.8f);
                        Prac1HazardisSet[3] = true;

                      
                        //   question++;
                    }
                    if (curFrame >= 21.0f && Prac1HazardisSet[3])
                    {
                        Prac1Hazards[3].gameObject.SetActive(false);
                    }
                }

        
                break;
                case 1:


                Debug.Log("Prac 2 is active");
                if ( Prac2Hazards[0]!=null)
                {
                    if (curFrame >= 1.0f && !Prac2HazardisSet[0])
                    {

                        Prac2Hazards[0].gameObject.SetActive(true);
                        Prac2Hazards[0].StartMove(new Vector3(-72.03f, -13f, - 31.13f), 
                            new Vector3(-12f, -19f, -44.5369568f), 6.8f);
                        Prac2HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 7.0f && Prac2HazardisSet[0])
                    {
                        Prac2Hazards[0].gameObject.SetActive(false);
                    }
                }

                if (  Prac2Hazards[1] != null)//Car
                {
                    if (curFrame >= 5.0f && !Prac2HazardisSet[1])
                    {

                        Prac2Hazards[1].gameObject.SetActive(true);
                        Prac2Hazards[1].StartMove(new Vector3(335.040009f, -5.9000001f, 86f), 
                            new Vector3(252.600006f, -11.3999996f, 74.5999985f), 5.0f);
                        Prac2HazardisSet[1] = true;
                        //   question++;Vector3(335.040009,-5.9000001,86)
                    }
                    if (curFrame >= 10.0f && Prac2HazardisSet[1])
                    {
                        Prac2Hazards[1].gameObject.SetActive(false);
                    }
                }
                if ( Prac2Hazards[2] != null)//Pedestrian
                {
                    if (curFrame >= 5.0f && !Prac2HazardisSet[2])
                    {

                        Prac2Hazards[2].gameObject.SetActive(true);
                        Prac2Hazards[2].StartMove(new Vector3(254f, -6.30758619f, 110f), 
                            new Vector3(-144f, 143f, -113f), 8.5f);
                        Prac2HazardisSet[2] = true;
                        //   question++;
                    }
                    if (curFrame >= 14.0f && Prac2HazardisSet[2])
                    {
                        Prac2Hazards[2].gameObject.SetActive(false);
                    }
                }
                

                break;
                case 2:
                Debug.Log("Q1 is active");
                if (Q1Hazards[0] != null)
                {
                    if (curFrame >= 1.0f && !Q1Hazards[0])
                    {

                        Q1Hazards[0].gameObject.SetActive(true);
                        Q1Hazards[0].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 8.8f);
                        Q1HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 9.0f && Q1HazardisSet[0])
                    {
                        Q1Hazards[0].gameObject.SetActive(false);
                    }
                }

                if (Q1Hazards[1] != null)
                {
                    if (curFrame >= 3.0f && !Q1Hazards[1])
                    {

                        Q1Hazards[1].gameObject.SetActive(true);
                        Q1Hazards[1].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 5.8f);
                        Q1HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 9.0f && Q1HazardisSet[1])
                    {
                        Q1Hazards[1].gameObject.SetActive(false);
                    }
                }

                if (Q1Hazards[2] != null)
                {
                    if (curFrame >= 12.0f && !Q1Hazards[2])
                    {

                        Q1Hazards[2].gameObject.SetActive(true);
                        Q1Hazards[2].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 3.8f);
                        Q1HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 16.0f && Q1HazardisSet[2])
                    {
                        Q1Hazards[2].gameObject.SetActive(false);
                    }
                }
                /*

                if (Prac3Hazards[0] != null)
                {//Ped_By_Car
                    if (curFrame >= 2.0f && !Prac3HazardisSet[0])
                    {

                        Prac3Hazards[0].gameObject.SetActive(true);
                        Prac3Hazards[0].StartMove(new Vector3(67.0f, -2.0f, -230.0f),
                            new Vector3(55.0f, -14.6f, -114.69f), 11.0f);
                        Prac3HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 13.0f && Prac3HazardisSet[0])
                    {
                        Prac3Hazards[0].gameObject.SetActive(false);
                    }
                }

                if (Prac3Hazards[1] != null)
                {//Ped_Looking_To_Cross
                    if (curFrame >= 8.0f && !Prac3HazardisSet[1])
                    {

                        Prac3Hazards[1].gameObject.SetActive(true);
                        Prac3Hazards[1].StartMove(new Vector3(-1.29f, -0.97f, -88.34f),
                            new Vector3(-20.39f, -2.7f, -64.4f), 13.0f);
                        Prac3HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 17.0f && Prac3HazardisSet[1])
                    {
                        Prac3Hazards[1].gameObject.SetActive(false);
                    }
                }
                if (Prac3Hazards[2] != null)
                {
                    //CarParallelPark
                    if (curFrame >= 17.0f && !Prac3HazardisSet[2])
                    {

                        Prac3Hazards[2].gameObject.SetActive(true);
                        Prac3Hazards[2].StartMove(new Vector3(47.0f, -5.987f, -235.867f),
                            new Vector3(38.3f, -10.72f, -141.7f), 10.0f);
                        Prac3HazardisSet[2] = true;
                        //   question++;
                    }
                    if (curFrame >= 27.0f && Prac3HazardisSet[2])
                    {
                        Prac3Hazards[2].gameObject.SetActive(false);
                    }
                }
                if (Prac3Hazards[3] != null)
                {//LightCar
                    if (curFrame >= 27.0f && !Prac3HazardisSet[3])
                    {

                        Prac3Hazards[3].gameObject.SetActive(true);
                        Prac3Hazards[3].StartMove(new Vector3(54.0f, -11.0f, -215.92f),
                            new Vector3(41.2f, -8.0f, -69.5f), 5.0f);
                        Prac3HazardisSet[3] = true;

                        SaveObj.TotalHazardWithinQuestion = 4;
                        HazardTotalCount = 11;
                        //   question++;
                    }
                    if (curFrame >= 32.0f && Prac3HazardisSet[3])
                    {
                        Prac3Hazards[3].gameObject.SetActive(false);
                    }
                }
                */
                break;
                case 3:
                if (Q2Hazards[0] != null)
                {
                    if (curFrame >= 1.0f && !Q2Hazards[0])
                    {

                        Q2Hazards[0].gameObject.SetActive(true);
                        Q2Hazards[0].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 6.8f);
                        Q2HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 7.0f && Q2HazardisSet[0])
                    {
                        Q2Hazards[0].gameObject.SetActive(false);
                    }
                }
                if (Q2Hazards[1] != null)
                {
                    if (curFrame >= 4.0f && !Q2Hazards[1])
                    {

                        Q2Hazards[1].gameObject.SetActive(true);
                        Q2Hazards[1].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 3.0f);
                        Q2HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 7.0f && Q2HazardisSet[1])
                    {
                        Q2Hazards[1].gameObject.SetActive(false);
                    }
                }
                if (Q2Hazards[2] != null)
                {
                    if (curFrame >= 7.0f && !Q2Hazards[2])
                    {

                        Q2Hazards[2].gameObject.SetActive(true);
                        Q2Hazards[2].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 1.8f);
                        Q2HazardisSet[2] = true;
                        //   question++;
                    }
                    if (curFrame >= 11.0f && Q1HazardisSet[2])
                    {
                        Q2Hazards[2].gameObject.SetActive(false);
                    }
                }
                Debug.Log("Q2 is active");
                break;
                case 4:
                Debug.Log("Q3 is active");
                if (Q3Hazards[0] != null)
                {
                    if (curFrame >= 1.0f && !Q3Hazards[0])
                    {

                        Q3Hazards[0].gameObject.SetActive(true);
                        Q3Hazards[0].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 5.0f);
                        Q3HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 6.0f && Q3HazardisSet[0])
                    {
                        Q3Hazards[0].gameObject.SetActive(false);
                    }
                }
                if (Q3Hazards[1] != null)
                {
                    if (curFrame >= 8.0f && !Q3Hazards[1])
                    {

                        Q3Hazards[1].gameObject.SetActive(true);
                        Q3Hazards[1].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 3.0f);
                        Q3HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 12.0f && Q3HazardisSet[1])
                    {
                        Q3Hazards[1].gameObject.SetActive(false);
                    }
                }
                if (Q3Hazards[2] != null)
                {
                    if (curFrame >= 9.0f && !Q3Hazards[2])
                    {

                        Q3Hazards[2].gameObject.SetActive(true);
                        Q3Hazards[2].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 5.0f);
                        Q3HazardisSet[2] = true;
                        //   question++;
                    }
                    if (curFrame >= 15.0f && Q3HazardisSet[2])
                    {
                        Q3Hazards[2].gameObject.SetActive(false);
                    }
                }
                break;
                case 5:
                Debug.Log("Q4 is active");
                if (Q4Hazards[0] != null)
                {
                    if (curFrame >= 1.0f && !Q4Hazards[0])
                    {

                        Q4Hazards[0].gameObject.SetActive(true);
                        Q4Hazards[0].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 7.0f);
                        Q4HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 8.0f && Q4HazardisSet[0])
                    {
                        Q4Hazards[0].gameObject.SetActive(false);
                    }
                }
                if (Q4Hazards[1] != null)
                {
                    if (curFrame >= 8.0f && !Q4Hazards[1])
                    {

                        Q4Hazards[1].gameObject.SetActive(true);
                        Q4Hazards[1].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 2.0f);
                        Q4HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 11.0f && Q4HazardisSet[1])
                    {
                        Q4Hazards[1].gameObject.SetActive(false);
                    }
                }
                if (Q4Hazards[2] != null)
                {
                    if (curFrame >= 7.0f && !Q4Hazards[2])
                    {

                        Q4Hazards[2].gameObject.SetActive(true);
                        Q4Hazards[2].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 6.0f);
                        Q4HazardisSet[2] = true;
                        //   question++;
                    }
                    if (curFrame >= 14.0f && Q4HazardisSet[2])
                    {
                        Q4Hazards[2].gameObject.SetActive(false);
                    }
                }
                if (Q4Hazards[3] != null)
                {
                    if (curFrame >= 13.0f && !Q4Hazards[3])
                    {

                        Q4Hazards[3].gameObject.SetActive(true);
                        Q4Hazards[3].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 4.8f);
                        Q4HazardisSet[3] = true;
                        //   question++;
                    }
                    if (curFrame >= 18.0f && Q4HazardisSet[3])
                    {
                        Q4Hazards[3].gameObject.SetActive(false);
                    }
                }
                break;
                case 6:
                Debug.Log("Q5 is active");
                if (Q5Hazards[0] != null)
                {
                    if (curFrame >= 2.0f && !Q5Hazards[0])
                    {

                        Q5Hazards[0].gameObject.SetActive(true);
                        Q5Hazards[0].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 5.0f);
                        Q5HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 6.0f && Q5HazardisSet[0])
                    {
                        Q5Hazards[0].gameObject.SetActive(false);
                    }
                }
                if (Q5Hazards[1] != null)
                {
                    if (curFrame >= 11.0f && !Q5Hazards[1])
                    {

                        Q5Hazards[1].gameObject.SetActive(true);
                        Q5Hazards[1].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 4.8f);
                        Q5HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 16.0f && Q5HazardisSet[1])
                    {
                        Q5Hazards[1].gameObject.SetActive(false);
                    }
                }
                if (Q5Hazards[2] != null)
                {
                    if (curFrame >= 17.0f && !Q5Hazards[2])
                    {

                        Q5Hazards[2].gameObject.SetActive(true);
                        Q5Hazards[2].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 1.8f);
                        Q5HazardisSet[2] = true;
                        //   question++;
                    }
                    if (curFrame >= 19.0f && Q5HazardisSet[2])
                    {
                        Q5Hazards[2].gameObject.SetActive(false);
                    }
                }
                if (Q5Hazards[3] != null)
                {
                    if (curFrame >= 21.0f && !Q5Hazards[3])
                    {

                        Q5Hazards[3].gameObject.SetActive(true);
                        Q5Hazards[3].StartMove(new Vector3(43.5f, -7.8f, -151.2f),
                            new Vector3(65.7f, -29.387f, -46.4487f), 2.8f);
                        Q5HazardisSet[3] = true;
                        //   question++;
                    }
                    if (curFrame >= 24.0f && Q5HazardisSet[3])
                    {
                        Q5Hazards[3].gameObject.SetActive(false);
                    }
                }
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
           // ShowEndScene(vp);
        }

    }

    void GetSaveFile()
    {

        GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        SaveObj = SaveFileObject.GetComponent<SaveDatas>();
        SaveObj.TotalCorrectWithinQuestions = 0;
        SaveObj.TotalHazardWithinQuestion = 0;
    }
}
