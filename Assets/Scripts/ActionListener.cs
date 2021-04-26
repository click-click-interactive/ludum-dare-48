using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionListener : MonoBehaviour
{
    public GameObject hintPrefab;
    public Canvas canvas;
    public BoolVariable isPlayingDialog;
    private GameObject hintInstance;
    private CallableAction actionTarget;
    private GameObject triggerInstance;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayingDialog.RuntimeValue) {
            GetComponent<Collider2D>().enabled = false;
            if (hintInstance) Destroy(hintInstance);
        } else {
            GetComponent<Collider2D>().enabled = true;
        }

      if (actionTarget != null && Input.GetKeyDown(KeyCode.F) && triggerInstance != null)
      {
          Destroy(hintInstance);
          triggerInstance.SendMessage("StartConversation");
          actionTarget.call();
      }
    }

    void OnTriggerEnter2D(Collider2D other) {
      ActionTrigger actionTrigger = other.GetComponent<ActionTrigger>();
      if (actionTrigger && actionTrigger.action) {
          this.actionTarget = actionTrigger.action;
          this.triggerInstance = other.gameObject;
          hintPrefab.GetComponentInChildren<TMP_Text>().text = this.actionTarget.getType();
          hintInstance = Instantiate(
              hintPrefab,
              other.gameObject.transform.position + (Vector3.right * 3),
              canvas.gameObject.transform.rotation,
              canvas.gameObject.transform
          );
      }
    }

    void OnTriggerExit2D(Collider2D other)
    {
      ActionTrigger actionTrigger = other.GetComponent<ActionTrigger>();
      if (actionTrigger) {
          if(this.triggerInstance == other.gameObject)
          {
              this.triggerInstance = null;
          }
          if (this.hintInstance) {
              Destroy(hintInstance);
          }
      }
  }
}
