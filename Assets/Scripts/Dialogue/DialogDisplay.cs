using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class QuestionEvent : UnityEvent<Question> { }

public class DialogDisplay : MonoBehaviour
{
    public Conversation conversation;
    public QuestionEvent questionEvent;
    public GameObject speaker;
    public BoolVariable playerCanControl;
    private SpeakerUI speakerUI;

    private bool conversationStarted = false;

    private int activeLineIndex = 0;

    public void LaunchConversation(Conversation convo)
    {
        playerCanControl.RuntimeValue = false;
        this.conversation = convo;
        advanceLine();
    }

    public void ChangeConversation(Conversation nextConversation)
    {
        conversationStarted = false;
        conversation = nextConversation;
        advanceLine();
    }
    
    void Start()
    {
        speakerUI = speaker.GetComponent<SpeakerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            advanceLine();
        } else if (Input.GetKeyDown("x"))
        {
            endConversation();
        }
    }

    void advanceConversation()
    {
        if(conversation.question != null)
        {
            Debug.Log("Invoke question");
            speakerUI.dialog.text = "";
            questionEvent.Invoke(conversation.question);
        }
        else if (conversation.nextConversation != null)
        {
            Debug.Log("Next convo");
            ChangeConversation(conversation.nextConversation);
        } else
        {
            endConversation();
        }
    }

    void endConversation()
    {
        conversation = null;
        conversationStarted = false;
        speakerUI.Hide();
        playerCanControl.RuntimeValue = true;
    }

    void initialize()
    {
        conversationStarted = true;
        activeLineIndex = 0;
        speakerUI.Show();
    }

    private void advanceLine()
    {
        if(conversation == null)
        {
            return;
        }
        if(!conversationStarted)
        {
            initialize();
        }
        if(activeLineIndex < conversation.lines.Length)
        {
            displayLine();
        } else
        {
            advanceConversation();
        }
    }

    void displayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        SetDialogContent(speakerUI, line);

        if (activeLineIndex < conversation.lines.Length - 1 && (conversation.question != null || conversation.nextConversation != null))
        {
            speakerUI.hint.SetActive(true);
        }
        else {
            speakerUI.hint.SetActive(false);
        }

        activeLineIndex++;
    }

    void SetDialogContent(SpeakerUI showSpeaker, Line line)
    {
        showSpeaker.Dialog = line.text;
        showSpeaker.fullName.text = line.character.fullName;
        showSpeaker.portrait.sprite = line.character.portrait;
    }
}
