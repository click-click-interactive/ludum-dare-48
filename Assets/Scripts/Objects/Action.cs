using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject, CallableAction
{
    public abstract void call();

    public abstract string getType();
}
