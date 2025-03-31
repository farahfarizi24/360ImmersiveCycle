using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InstructionTextUI : MonoBehaviour
{
    private int index = 0;
    public GameObject[] Instructions;
    public GameObject nextButton;
    public GameObject Bg;
    // Start is called before the first frame update
    void Start()
    {
        Bg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void NextHit()
    {
        index++;
        ChangeUI(index);
    }


    public void ChangeUI(int i)
    {
        int temp = i - 1;
        Bg.SetActive(true);
        switch (i)
        {
            case 0:

                Instructions[temp].SetActive(false);
                Instructions[i].SetActive(true);
                break;


            case 1:
                Instructions[temp].SetActive(false);
                Instructions[i].SetActive(true);
                break;
            case 2:
                Instructions[temp].SetActive(false);
                Instructions[i].SetActive(true);
                break;
            case 3:
                Instructions[temp].SetActive(false);
                Instructions[i].SetActive(true);
                nextButton.SetActive(false);
                break;
        }
    }
}
