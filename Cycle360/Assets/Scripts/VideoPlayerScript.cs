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

    public GameObject BackgroundUI;
    public TextMeshProUGUI QuestionFeedback;
    public TextMeshProUGUI TestFeedback;
    public GameObject NextButton;
    public GameObject CompleteButton;
    private int HazardTotalCount;
    public float curFrame;
    public string WhichScene;
    public VideoPlayer video;
    public bool isPlaying;
    public bool isReady = false;
    public int question=0;
  //  public TextMeshProUGUI pauseButton;
    // Start is called before the first frame update
  

    void Start()
    {
        // isPlaying = true;
      StartCoroutine(LoadAtStart());
        HazardisSet_RC = false;
        RedCar_hazardTracker.gameObject.SetActive(false);
        HazardisSet_PC = false;
        ParkedCar_hazardTracker.gameObject.SetActive(false);
        HazardisSet_Roundabout = false;
        Roundabout_hazardTracker.gameObject.SetActive(false);
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
        switch (WhichScene)
        {
            case "perception":
                PerceptionVP();
                video.loopPointReached += ShowEndScene;
                break;
            case "projection":
                ProjectionVP();
                break;
            case "comprehension":
                ComprehensionVP();
                break;
        }
    }

    #region PlaybackMethods
    public void PlaybackManager()
    {
        if (isPlaying) 
        { PauseVid(); }
        else { ContinueVid(); }
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
, 9.0f); SaveObj.TotalHazardWithinQuestion = 3;
                HazardTotalCount += SaveObj.TotalHazardWithinQuestion;
                HazardisSet_Roundabout = true;
                //  question++;
            }
            if (curFrame >= 25.0f && HazardisSet_Roundabout)

            {
                
                Roundabout_hazardTracker.gameObject.SetActive(false);
            }
        }

       
        video.loopPointReached += EndofQuestion;










    }


    void ProjectionVP()
    {

    }

    void ComprehensionVP()
    {

    }
    #endregion
    


    public void OnStartLoad()
    {
        SceneManager.LoadScene(0);
    }
public void ShowEndScene(VideoPlayer vp)
    {
        isPlaying = false;
        SaveObj.TotalHazardOnTheTest = HazardTotalCount;
        QuestionFeedback.text = "You identified " + SaveObj.TotalCorrectWithinTheTest + " out of " +
          SaveObj.TotalHazardOnTheTest + " hazards in the video";

    }

    void EndofQuestion(VideoPlayer vp)
    {
        GetSaveFile();
        BackgroundUI.gameObject.SetActive(true);
        NextButton.gameObject.SetActive(true);
       TestFeedback.gameObject.SetActive(true);
        TestFeedback.text = "You identified " + SaveObj.TotalCorrectWithinQuestions + " out of " +
          SaveObj.TotalHazardWithinQuestion + " hazards in the video";

    }

    void GetSaveFile()
    {

        GameObject SaveFileObject = GameObject.FindGameObjectWithTag("Manager");
        SaveObj = SaveFileObject.GetComponent<SaveDatas>();
    }
}
