using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Actions/Dialog Action", order = 1)]
public abstract class Action : ScriptableObject, CallableAction
{
    public abstract void call();

    public abstract string getType();
}
