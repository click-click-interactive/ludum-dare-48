using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog Action", menuName = "Actions/Dialog Action", order = 1)]
public class DialogAction : Action
{
    public ActionType type = ActionType.Talk;
    public string[] payload;

    public override void call() {
      foreach (string str in payload)
      {
        Debug.Log(str);
      }
    }

    public override string getType() {
      return "Talk";
    }
}
