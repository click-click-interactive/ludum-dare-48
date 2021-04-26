using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kill Action", menuName = "Actions/Kill Action", order = 1)]
public class KillAction : Action
{
    public ActionType type = ActionType.Kill;
    public string[] payload;

    public override void call() {
      foreach (string str in payload)
      {
        Debug.Log(str);
      }
    }

    public override string getType() {
      return "Kill";
    }
}
