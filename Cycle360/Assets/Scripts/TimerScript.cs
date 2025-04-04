using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerScript : MonoBehaviour
{
    public VideoPlayerScript VPScript;
    public int TimeLeft;
    public bool TimerOn = false;
    public TMP_Text TimerTxt;
    public GameObject TimerBG;
    // Start is called before the first frame update
    void Start()
    {
        TimerTxt.text = "";
        TimeLeft = 3;
    }

    private void Update()
    {
        if ((VPScript.isReady))
        {
            VPScript.isReady = false;
            TimerOn = true;
            StartCoroutine(countdownTimer());
        }

    }
    IEnumerator countdownTimer()
    {
       TimerBG.SetActive(true);
       while(TimeLeft > 0)
        {
            TimerTxt.text = TimeLeft.ToString();
            yield return new WaitForSeconds(1f);
            TimeLeft--;

        }
        TimerTxt.text = "Go!";
        yield return new WaitForSeconds(1f);
        TimerBG.SetActive(false);
        startVid();
        TimeLeft = 3;
        StopCoroutine(countdownTimer());
    }


    public void startVid()
    {
        VPScript.PlaybackManager();
        
        TimerTxt.text = "";
    }
    
}
