using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Read Action", menuName = "Actions/Read Action", order = 1)]
public class ReadAction : Action
{
    public ActionType type = ActionType.Read;
    public string[] payload;

    public override void call() {
      foreach (string str in payload)
      {
        Debug.Log("Read: " + str);
      }
    }

    public override string getType() {
      return "Read";
    }
}
