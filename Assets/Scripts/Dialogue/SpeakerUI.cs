using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeakerUI : MonoBehaviour
{
    public Image portrait;
    public TextMeshProUGUI fullName;
    public TextMeshProUGUI dialog;
    public TextMeshProUGUI choice = null;
    public GameObject hint;

    private Character speaker;
    
    public Character Speaker
    {
        get { return speaker; }
        set
        {
            speaker = value;
            Debug.Log(speaker);
            portrait.sprite = speaker.portrait;
            fullName.text = speaker.fullName;
        }
    }

    public string Dialog
    {
        set { dialog.text = value; }
    }

    public bool HasSpeaker()
    {
        return speaker != null;
    }

    public bool IsEqualsTo(Character character)
    {
        return fullName.text == character.fullName && speaker.portrait == character.portrait;
    }
    

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
