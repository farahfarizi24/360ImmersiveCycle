using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.Playables;

public class SaveDatas : MonoBehaviour
{
    public static SaveDatas instance;
    string saveFile;
    private StreamWriter sw;

    public string UID;
    public string theDate;
    public string TestID;

    //For perception test
    int TotalCorrectClick;
    int TotalIncorrectClick;
    int TotalNumberofClick;
    int TotalScore;
  
    //for showcase
   public int TotalCorrectWithinQuestions;
   public int TotalHazardWithinQuestion;
   public int TotalHazardOnTheTest;
    public int TotalCorrectWithinTheTest;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
       

    }

    public void OnPerceptionTestEnter()
    {
        saveFile = Application.persistentDataPath + "/360Cycle-PerceptionTest.csv";
        Debug.Log("File is saved at:" + saveFile);

        if (!File.Exists(saveFile))
        {

            sw = File.AppendText(saveFile);

            sw.WriteLine("ID," + "Date," + "Time," + "Test ID," + "Question ID," + "Video Time," + "Object ID," + "Score," + "Total Correct Click," +
            "Total Incorrect Click," + "Total Number of Click," + "Total Score");
            sw.Close();

        }
       
        theDate = System.DateTime.Now.ToString("MM/dd/yyyy");
        TestID = "Perception";
        TotalCorrectClick = 0;
        TotalIncorrectClick = 0;
        TotalNumberofClick = 0;
        TotalScore = 0;
       // Debug.Log(theDate + theTime);
      
    }

    public void OnPerceptionTestWrongClick(string VideoTime)
    {
       sw = File.AppendText(saveFile);
        string theTime = System.DateTime.Now.ToString("hh:mm:ss");
        TotalIncorrectClick++;
        TotalNumberofClick++;
        sw.Write(UID + "," + theDate + "," + theTime + "," + TestID + "," + "wrong" + "," + VideoTime+","+ "wrong"+","+"0"+ ",");
        sw.Write(TotalCorrectClick + "," + TotalIncorrectClick + "," + TotalNumberofClick + "," + TotalScore +"\n"); 
        sw.Close();
    }


    public void OnPerceptionTestCorrectClick(string QuestionID, string VideoTime, string ObjectID, int AddScore)
    {
        sw = File.AppendText(saveFile);
        string theTime = System.DateTime.Now.ToString("hh:mm:ss");
        TotalCorrectClick++;
        TotalNumberofClick++;
        TotalScore += AddScore;

        sw.Write(UID + "," + theDate + "," + theTime + "," + TestID + "," + QuestionID + "," + VideoTime + "," + ObjectID + "," + AddScore + ",");
        sw.Write(TotalCorrectClick + "," + TotalIncorrectClick + "," + TotalNumberofClick + "," + TotalScore + "\n");
        sw.Close();
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
