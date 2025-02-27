using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public GameObject GeneralInstruction;
    public GameObject PercTestInstruction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPerceptionTest()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadComprehensionTest()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadProjectionTest()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadAltPerceptionTest()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadPerceptionTestInstruction()
    {
        GeneralInstruction.SetActive(false);
        PercTestInstruction.SetActive(true);
    }

}
