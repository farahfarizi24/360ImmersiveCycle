using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    public VideoPlayer video;
    public bool isPlaying;
    public TextMeshProUGUI pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonHit()
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
}
