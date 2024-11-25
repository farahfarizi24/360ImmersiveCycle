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
    // Start is called before the first frame update
    void Start()
    {
        TimerTxt.text = "";
       
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
       while(TimeLeft > 0)
        {
            TimerTxt.text = TimeLeft.ToString();
            yield return new WaitForSeconds(1f);
            TimeLeft--;
        }
        TimerTxt.text = "Go!";
        yield return new WaitForSeconds(1f);
        startVid();
        StopCoroutine(countdownTimer());
    }


    public void startVid()
    {
        VPScript.PlaybackManager();
        TimerTxt.text = "";
    }
    
}
