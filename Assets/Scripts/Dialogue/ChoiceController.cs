using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public class ConversationChangeEvent : UnityEvent<Conversation> { }

[System.Serializable]
public class ChoiceMouseEnterEvent : UnityEvent<PointerEventData> {}

public class ChoiceController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Choice choice;
    public ConversationChangeEvent conversationChangeEvent;
    public Button button;
    public Color hoverBackgroundColor = Color.white;
    public Color hoverTextColor = Color.black;

    public static ChoiceController AddChoiceButton(Button choiceButtonTemplate, Choice choice, int index)
    {
        int buttonSpacing = -30;
        Button button = Instantiate(choiceButtonTemplate);

        button.transform.SetParent(choiceButtonTemplate.transform.parent);
        button.transform.localScale = Vector3.one;
        button.transform.localPosition = new Vector3(0, index * buttonSpacing, -1);
        button.name = "Choice " + (index + 1);
        button.gameObject.SetActive(true);

        ChoiceController choiceController = button.GetComponent<ChoiceController>();
        choiceController.button = button;
        choiceController.choice = choice;
        return choiceController;
    }
        
    // Start is called before the first frame update
    void Start()
    {
        if(conversationChangeEvent == null)
        {
            conversationChangeEvent = new ConversationChangeEvent();
        }
        GetComponent<Button>().GetComponentInChildren<TMP_Text>().text = choice.choiceText;
    }

    public void MakeChoice()
    {
        conversationChangeEvent.Invoke(choice.conversation);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("Hover");
        GetComponent<Image>().color = hoverBackgroundColor;
        GetComponentInChildren<TMP_Text>().color = hoverTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = hoverTextColor;
        GetComponentInChildren<TMP_Text>().color = hoverBackgroundColor;
    }
}
