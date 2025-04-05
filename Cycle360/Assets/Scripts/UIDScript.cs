using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIDScript : MonoBehaviour
{
    public TMP_InputField UIDInput;
    public SaveDatas SaveDatas;
    public GameObject InstructionTxt1;
    public GameObject NextButton;
    public GameObject TitleObject;
    public GameObject BG;
    // Start is called before the first frame update


    public void OnStartInstruction()
    {
        string inputText = UIDInput.text;
        if (inputText.Length >= 1)
        {
            SaveDatas.UID = inputText;
            InstructionTxt1.gameObject.SetActive(true);
            NextButton.gameObject.SetActive(true);
            TitleObject.SetActive(false);
            BG.SetActive(true);
        }


    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
