using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    public Question question;
    public Button choiceButton;
    private List<ChoiceController> choiceControllers = new List<ChoiceController>();

    public void Change(Question question)
    {
        RemoveChoices();
        this.question = question;
        Initialize();
    }

    public void Hide(Conversation conversation)
    {
        RemoveChoices();
        gameObject.SetActive(false);
    }

    private void RemoveChoices()
    {
        foreach(ChoiceController c in choiceControllers)
        {
            Destroy(c.gameObject);
        }
        choiceControllers.Clear();
    }

    private void Initialize()
    {
        choiceButton.gameObject.SetActive(false);
        for (int i = 0; i < question.choices.Length; i++)
        {
            ChoiceController c = ChoiceController.AddChoiceButton(choiceButton, question.choices[i], i);
            choiceControllers.Add(c);
        }
        
    }
}
