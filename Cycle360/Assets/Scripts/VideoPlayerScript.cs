using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    public HazardTracker hazardTracker;
    public bool HazardisSet;
    float curFrame;
    public string WhichScene;
    public VideoPlayer video;
    public bool isPlaying;
    public bool isReady = false;
    public int question=0;
    public TextMeshProUGUI pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        // isPlaying = true;
      StartCoroutine(LoadAtStart());
       HazardisSet = false;
        hazardTracker.gameObject.SetActive(false);
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
        Debug.Log("Frame" + curFrame);
        switch (WhichScene)
        {
            case "perception":
                PerceptionVP();
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
        pauseButton.text = "Continue";
    }
    public void ContinueVid()
    {
       video.Play();
        isPlaying = true;
        pauseButton.text = "Pause";


    }
    #endregion

    #region VideoManager


    void PerceptionVP()
    {
        if (curFrame >= 15.0f && !HazardisSet) 
        {
            hazardTracker.gameObject.SetActive(true);
            hazardTracker.StartMove(new Vector3(-221.100006f, -6.80000019f, -110.599998f), new Vector3(14.3000002f, -6.80000019f, -36.9000015f), 5.0f);
            HazardisSet = true;
            question++;
        }
        if(curFrame>=25.0f && HazardisSet && question == 1)
        {
            hazardTracker.gameObject.SetActive(false);
        }


        


    }


    void ProjectionVP()
    {

    }

    void ComprehensionVP()
    {

    }
    #endregion

}
