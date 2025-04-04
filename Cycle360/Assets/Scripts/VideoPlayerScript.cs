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
               video.controlledAudioTrackCount = 1;

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
            if (CurPlayingClip ==6) 
            {//NextButton.gameObject.SetActive(false);

                ShowEndScene();
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
        else if (FinalFeedback.gameObject.activeSelf)
        {
            SceneManager.LoadScene(0);

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
        video.controlledAudioTrackCount = 1;
        SaveObj.TotalCorrectWithinQuestions = 0;
        SaveObj.TotalHazardWithinQuestion = 0;
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
        if (VidToPlay == 5)
        {
            PlayerObject.transform.eulerAngles = new Vector3(0.0f, 80.0f, 0.0f);
        }
        if (VidToPlay == 6)
        {
            PlayerObject.transform.eulerAngles = new Vector3(0.0f, 80.0f, 0.0f);
        }
        if (VidToPlay == 7)
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
        setBoolfalse();
        video.Play();
        curFrame = 0;
        isPlaying = true;


    }
    public void setBoolfalse()
    {
        for(int i = 0; i < Prac1Hazards.Length; i++)
        {
            if ((Prac1Hazards[i])!= null)
            {
                Prac1Hazards[i].gameObject.SetActive(false);

            }
        }
        for (int i = 0; i < Prac2Hazards.Length; i++)
        {
            if ((Prac2Hazards[i]) != null)
            {
                Prac2Hazards[i].gameObject.SetActive(false);

            }
        }
        for (int i = 0; i < Q1Hazards.Length; i++)
        {
            if ((Q1Hazards[i]) != null)
            {
                Q1Hazards[i].gameObject.SetActive(false);

            }
        }
        for (int i = 0; i < Q2Hazards.Length; i++)
        {
            if ((Q2Hazards[i]) != null)
            {
                Q2Hazards[i].gameObject.SetActive(false);

            }
        }
        for (int i = 0; i < Q3Hazards.Length; i++)
        {
            if ((Q3Hazards[i]) != null)
            {
                Q3Hazards[i].gameObject.SetActive(false);

            }
        }
        for (int i = 0; i < Q4Hazards.Length; i++)
        {
            if ((Q4Hazards[i]) != null)
            {
                Q4Hazards[i].gameObject.SetActive(false);

            }
        }
        for (int i = 0; i < Q5Hazards.Length; i++)
        {
            if ((Q5Hazards[i]) != null)
            {
                Q5Hazards[i].gameObject.SetActive(false);

            }
        }
        
        
        for (int i = 0; i < Prac1HazardisSet.Length; i++)
        {
            Prac1HazardisSet[i] = false;

        }
     
        for (int i = 0; i < Prac2HazardisSet.Length; i++)
        {
            Prac2HazardisSet[i] = false;

        }
        for (int i = 0; i < Q1HazardisSet.Length; i++)
        {
            Q1HazardisSet[i] = false;

        }
        for (int i = 0; i < Q2HazardisSet.Length; i++)
        {
            Q2HazardisSet[i] = false;

        }
        for (int i = 0; i < Q3HazardisSet.Length; i++)
        {
            Q3HazardisSet[i] = false;

        }
        for (int i = 0; i < Q4HazardisSet.Length; i++)
        {
            Q4HazardisSet[i] = false;

        }
        for (int i = 0; i < Q5HazardisSet.Length; i++)
        {
            Q5HazardisSet[i] = false;

        }
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
                SaveObj.TotalHazardWithinQuestion = Prac1HazardisSet.Length;
                if (Prac1Hazards[0] != null)//Ped
                {
                    Prac1Hazards[0].initTime = 4.0f;
                    Prac1Hazards[0].despawnTime = 6.0f;
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
                    Prac1Hazards[1].initTime = 9.0f;
                    Prac1Hazards[1].despawnTime = 14.5f;
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
                    Prac1Hazards[2].initTime = 13.0f;
                    Prac1Hazards[2].despawnTime = 19.0f;
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
                    Prac1Hazards[3].initTime = 14.0f;
                    Prac1Hazards[3].despawnTime = 21.0f;
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
                SaveObj.TotalHazardWithinQuestion = Prac2HazardisSet.Length;


                Debug.Log("Prac 2 is active");
                if ( Prac2Hazards[0]!=null)
                {
                    Prac2Hazards[0].initTime = 1.0f;
                    Prac2Hazards[0].despawnTime = 7.0f;
                    if (curFrame >= 1.0f && !Prac2HazardisSet[0])
                    {

                        Prac2Hazards[0].gameObject.SetActive(true);
                        Prac2Hazards[0].StartMove(new Vector3(-73.0999985f, 6.19999981f, -226f), 
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
                    Prac2Hazards[1].initTime = 5.0f;
                    Prac2Hazards[1].despawnTime = 14.0f;
                    if (curFrame >= 5.0f && !Prac2HazardisSet[1])
                    {

                        Prac2Hazards[1].gameObject.SetActive(true);
                        Prac2Hazards[1].StartMove(new Vector3(335.040009f, -5.9000001f, 86f), 
                            new Vector3(252.600006f, -11.3999996f, 74.5999985f), 5.0f);
                        Prac2HazardisSet[1] = true;
                        //   question++;Vector3(335.040009,-5.9000001,86)
                    }
                    if (curFrame >= 14.0f && Prac2HazardisSet[1])
                    {
                        Prac2Hazards[1].gameObject.SetActive(false);
                    }
                }
                if ( Prac2Hazards[2] != null)//Pedestrian
                {
                    Prac2Hazards[2].initTime = 5.0f;
                    Prac2Hazards[2].despawnTime = 10.0f;
                    if (curFrame >= 5.0f && !Prac2HazardisSet[2])
                    {

                        Prac2Hazards[2].gameObject.SetActive(true);
                        Prac2Hazards[2].StartMove(new Vector3(254f, -6.30758619f, 110f), 
                            new Vector3(48.4f, -8.7f, -68.69f), 8.5f);
                        Prac2HazardisSet[2] = true;
                        //   question++;
                    }
                    if (curFrame >= 10.0f && Prac2HazardisSet[2])
                    {
                        Prac2Hazards[2].gameObject.SetActive(false);
                    }
                }
                

                break;
                case 2:
                Debug.Log("Q1 is active");
                SaveObj.TotalHazardWithinQuestion = Q1HazardisSet.Length;

                if (Q1Hazards[0] != null)//light
                {
                    if (curFrame >= 1.0f && !Q1HazardisSet[0])
                    {
                        Q1Hazards[0].initTime = 1.0f;
                        Q1Hazards[0].despawnTime = 6.0f;

                        Q1Hazards[0].gameObject.SetActive(true);
                        Q1Hazards[0].StartMove(new Vector3(-61f, 10f, -151.2f),
                             new Vector3(-53.65f, 20.4f, -170.7f), 5f);
                        Q1HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 6.0f && Q1HazardisSet[0])
                    {
                        Q1Hazards[0].gameObject.SetActive(false);
                    }
                }

                if (Q1Hazards[1] != null)//pedestrian1 2 ppl
                {
                    Q1Hazards[1].initTime = 3.0f;
                    Q1Hazards[1].despawnTime = 9.0f;
                    if (curFrame >= 3.0f && !Q1HazardisSet[1])
                    {

                        Q1Hazards[1].gameObject.SetActive(true);
                        Q1Hazards[1].StartMove(new Vector3(-174.78f, -5.8f, -234.1f),
                           new Vector3(-46.5f, -22.5f, 55.4f), 8f);
                        Q1HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 9.0f && Q1HazardisSet[1])
                    {
                        Q1Hazards[1].gameObject.SetActive(false);
                    }
                }

                if (Q1Hazards[2] != null)//2nd pedestrian
                {
                    Q1Hazards[2].initTime = 12.0f;
                    Q1Hazards[2].despawnTime = 16.0f;
                    if (curFrame >= 12.0f && !Q1HazardisSet[2])
                    {
                       

                        Q1Hazards[2].gameObject.SetActive(true);
                        Q1Hazards[2].StartMove(new Vector3(-133f, -7.8f, -149.36f),
                            new Vector3(-120.0f, -22.79f, -3.7f), 6f);
                        Q1HazardisSet[2] = true;//check on this
                  
                    }
                    if (curFrame >= 16.0f && Q1HazardisSet[2])
                    {
                        Q1Hazards[2].gameObject.SetActive(false);
                    }
                }
               
                break;
                case 3:
                SaveObj.TotalHazardWithinQuestion = Q2HazardisSet.Length;

                if (Q2Hazards[0] != null)//pedestrian
                {
                    Q2Hazards[0].initTime = 1.0f;
                    Q2Hazards[0].despawnTime = 7.0f;
                    if (curFrame >= 1.0f && !Q2HazardisSet[0])
                    {

                        Q2Hazards[0].gameObject.SetActive(true);
                        Q2Hazards[0].StartMove(new Vector3(165.3f, -10.3f, -8.3f),
                            new Vector3(62.92f, -26.691f, -59.5f), 6.8f);
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
                    Q2Hazards[1].initTime = 4.0f;
                    Q2Hazards[1].despawnTime = 7.0f;
                    if (curFrame >= 4.0f && !Q2HazardisSet[1])//cyclist
                    {

                        Q2Hazards[1].gameObject.SetActive(true);
                        Q2Hazards[1].StartMove(new Vector3(150.19f, -7.93f, -1.79f),
                            new Vector3(93.0f, -18.6f, -20.8f), 3.0f);
                        Q2HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 7.0f && Q2HazardisSet[1])
                    {
                        Q2Hazards[1].gameObject.SetActive(false);
                    }
                }
                if (Q2Hazards[2] != null)//car
                {
                    Q2Hazards[2].initTime = 7.0f;
                    Q2Hazards[2].despawnTime = 10.0f;
                    if (curFrame >= 7.0f && !Q2HazardisSet[2])
                    {

                        Q2Hazards[2].gameObject.SetActive(true);
                        Q2Hazards[2].StartMove(new
                            Vector3(194.69f, -8.1f, -12.25f),
                           new Vector3(111.3f, -14.5f, 49.7f), 1.8f);
                        Q2HazardisSet[2] = true;
                        //   question++;
                    }
                    if (curFrame >= 10.0f && Q2HazardisSet[2])
                    {
                        Q2Hazards[2].gameObject.SetActive(false);
                    }
                }
                Debug.Log("Q2 is active");
                break;
                case 4:
                SaveObj.TotalHazardWithinQuestion = Q3HazardisSet.Length;

                Debug.Log("Q3 is active");
                if (Q3Hazards[0] != null)
                {
                    Q3Hazards[0].initTime = 1.0f;
                    Q3Hazards[0].despawnTime = 6.0f;
                    if (curFrame >= 1.0f && !Q3HazardisSet[0])
                    {

                        Q3Hazards[0].gameObject.SetActive(true);
                        Q3Hazards[0].StartMove(new Vector3(189.35f, -7.9f, 58.8f),
                            new Vector3(71.1f, -22.2f, 49.7f), 5.0f);
                        Q3HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 6.0f && Q3HazardisSet[0])
                    {
                        Q3Hazards[0].gameObject.SetActive(false);
                    }
                }
                if (Q3Hazards[1] != null)//dog
                {
                    Q3Hazards[1].initTime = 8.0f;
                    Q3Hazards[1].despawnTime = 12.0f;
                    if (curFrame >= 8.0f && !Q3HazardisSet[1])
                    {

                        Q3Hazards[1].gameObject.SetActive(true);
                        Q3Hazards[1].StartMove(
                            new Vector3(186f, -10.3f, 95.8f),
                            new Vector3(8.1f, -39.6f, 46.0f), 5.0f);
                        Q3HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 12.0f && Q3HazardisSet[1])
                    {
                        Q3Hazards[1].gameObject.SetActive(false);
                    }
                }
                if (Q3Hazards[2] != null)
                {
                    Q3Hazards[2].initTime = 9.0f;
                    Q3Hazards[2].despawnTime = 15.0f;
                    if (curFrame >= 9.0f && !Q3HazardisSet[2])
                    {

                        Q3Hazards[2].gameObject.SetActive(true);
                        Q3Hazards[2].StartMove(
                            new Vector3(166.25f, -1.8f, 30f),
                            new Vector3(53.5f, -16f, -18.7999992f), 5.0f);
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
                SaveObj.TotalHazardWithinQuestion = Q4HazardisSet.Length;

                Debug.Log("Q4 is active");
                if (Q4Hazards[0] != null)//car
                {
                    Q4Hazards[0].initTime = 1.0f;
                    Q4Hazards[0].despawnTime = 6.0f;
                    if (curFrame >= 1.0f && !Q4HazardisSet[0])
                    {

                        Q4Hazards[0].gameObject.SetActive(true);
                        Q4Hazards[0].StartMove(
                            new Vector3(194.8f, 5f, -27.1f),
                            new Vector3(150.91f, -2.17f, -34.7f)
                            , 5.0f);
                        Q4HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 6.0f && Q4HazardisSet[0])
                    {
                        Q4Hazards[0].gameObject.SetActive(false);
                    }
                }
                if (Q4Hazards[1] != null)//carLeft
                {
                    Q4Hazards[1].initTime = 8.0f;
                    Q4Hazards[1].despawnTime = 11.0f;
                    if (curFrame >= 8.0f && !Q4HazardisSet[1])
                    {

                        Q4Hazards[1].gameObject.SetActive(true);
                        Q4Hazards[1].StartMove(
                            new Vector3(70.6f, -4.7f, 253.02f),
                            new Vector3(-55.29f, -10.6f, 234.27f), 3.0f);
                        Q4HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 11.0f && Q4HazardisSet[1])
                    {
                        Q4Hazards[1].gameObject.SetActive(false);
                    }
                }
                if (Q4Hazards[2] != null)//car reversing
                {
                    Q4Hazards[2].initTime = 7.0f;
                    Q4Hazards[2].despawnTime = 14.0f;
                    if (curFrame >= 7.0f && !Q4HazardisSet[2])
                    {

                        Q4Hazards[2].gameObject.SetActive(true);
                        Q4Hazards[2].StartMove(
                            new Vector3(213.3f, 8f, 60f),
                            new Vector3(76.0100021f, 1.7f, 54.7f), 6.0f);
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
                    Q4Hazards[3].initTime = 13.0f;
                    Q4Hazards[3].despawnTime = 18.0f;
                    if (curFrame >= 13.0f && !Q4HazardisSet[3])
                    {

                        Q4Hazards[3].gameObject.SetActive(true);
                        Q4Hazards[3].StartMove(
                            new Vector3(-124.5f, -9.393f, -60.4f),
                            new Vector3(14.5f, -7f, -64.41f),6.5f);
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
                SaveObj.TotalHazardWithinQuestion = Q5HazardisSet.Length;

                Debug.Log("Q5 is active");
                if (Q5Hazards[0] != null)//cone
                {
                    Q5Hazards[0].initTime = 2.0f;
                    Q5Hazards[0].despawnTime = 6.0f;
                    if (curFrame >= 2.0f && !Q5HazardisSet[0])
                    {

                        Q5Hazards[0].gameObject.SetActive(true);
                        Q5Hazards[0].StartMove(
                            new Vector3(185.94f, -10.5f, -31.2f),
                            new Vector3(-53.2f, -24.1f, -83.8f), 5.0f);
                        Q5HazardisSet[0] = true;
                        //   question++;
                    }
                    if (curFrame >= 6.0f && Q5HazardisSet[0])
                    {
                        Q5Hazards[0].gameObject.SetActive(false);
                    }
                }
                if (Q5Hazards[1] != null)//speedbump
                {
                    Q5Hazards[1].initTime = 11.0f;
                    Q5Hazards[1].despawnTime = 15.0f;
                    if (curFrame >= 11.0f && !Q5HazardisSet[1])
                    {

                        Q5Hazards[1].gameObject.SetActive(true);
                        Q5Hazards[1].StartMove(
                            new Vector3(237.8f, -17.3f, -0.89f),
                            new Vector3(73.4400024f, -20.2000008f, -10.5f)
                            , 4.8f);
                        Q5HazardisSet[1] = true;
                        //   question++;
                    }
                    if (curFrame >= 15.0f && Q5HazardisSet[1])
                    {
                        Q5Hazards[1].gameObject.SetActive(false);
                    }
                }
                if (Q5Hazards[2] != null)//pedestrian
                {
                    Q5Hazards[2].initTime = 17.0f;
                    Q5Hazards[2].despawnTime = 19.0f;
                    if (curFrame >= 17.0f && !Q5HazardisSet[2])
                    {

                        Q5Hazards[2].gameObject.SetActive(true);
                        Q5Hazards[2].StartMove(
                            new Vector3(112.129997f, -7.78000021f, 20.0699997f),
                            new Vector3(48.1500015f, -7.78000021f, 35.2999992f), 1.8f);
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
                    Q5Hazards[3].initTime = 21.0f;
                    Q5Hazards[3].despawnTime = 24.0f;
                    if (curFrame >= 21.0f && !Q5HazardisSet[3])
                    {

                        Q5Hazards[3].gameObject.SetActive(true);
                        Q5Hazards[3].StartMove(
                            new Vector3(192.419998f, -10.5f, 27.2999992f),
                            new Vector3(87.5999985f, -10.5f, 27.2999992f), 2.8f);
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



    public static int CalculateEffiency(int A, int B)
    {
        if (B == 0)
        {
            Debug.LogWarning("Cannot divide by zero! Returning 0%");
            return 0;
        }

        int percentage = Mathf.RoundToInt(((float)A / B) * 100); // Ensures correct calculation
        return percentage;
    }
    public void ShowEndScene()
    {
        int efficiency = CalculateEffiency(SaveObj.TotalCorrectClick, SaveObj.TotalNumberofClick);

        FinalFeedback.gameObject.SetActive(true);
        SaveObj.TotalHazardOnTheTest = HazardTotalCount;
       
            FinalFeedback.text = "You identified " + SaveObj.TotalCorrectWithinTheTest + " out of 24 hazards."
         +"\n"+ "Your hazard detection efficiency is " + efficiency +"%";
          
            NextButton.GetComponentInChildren<TMP_Text>().text = "Complete Test";
        
   
      

    }

    void EndofQuestion(VideoPlayer vp)
    {
        //GetSaveFile();
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
       
    }
}
