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
        Debug.Log("Change question");
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

            if (i > 0)
            {
                Navigation nav = c.button.navigation;
                nav.selectOnUp = choiceControllers[i - 1].button;
                c.button.navigation = nav;

                nav = choiceControllers[i - 1].button.navigation;
                nav.selectOnDown = c.button;
                choiceControllers[i - 1].button.navigation = nav;
            }

            choiceControllers.Add(c);
        }
        gameObject.SetActive(true);
    }
}
