using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueOB;
    public Image profile;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNameText;
    public static bool PSpace = false;
    public static bool PInt = true;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;

    public void Speech(Sprite p, string[] txt, string actorName)
    {
        PSpace = true;
        dialogueOB.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TSentence());
    }

    IEnumerator TSentence(){
        foreach (char letter in sentences[index].ToCharArray()){
            speechText.text += letter;
            PInt = false;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && PSpace == true)
        {
            NextSentence();
        }
    }
    void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                PSpace = true;
                index++;
                speechText.text = "";
                PInt = false;
                StartCoroutine(TSentence());
            }else
            {
                speechText.text = "";
                index = 0;
                dialogueOB.SetActive(false);
                PInt = true;
                PSpace = false;
            }
        }
    }
}
