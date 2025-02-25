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

public class PercTest2 : MonoBehaviour
{
    public SaveDatas saveDatas;
    public float curVideoTime;
    public VideoPlayer VPlayer;
    public bool isPlaying;
    public int CurrentClipNumber;

    public string VideoID;
    public string CorrectAnswer;
    public bool IsCorrect;
    public bool VideoIsPreparing;
    public bool isReady;
    public int timer = 3;
    public float ResponseTime;
    public GameObject[] Hazards;
    public TextMeshProUGUI AnswerStatus;
    public string RootPath;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
