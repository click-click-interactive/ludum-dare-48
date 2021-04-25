using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider2D))]
public class DialogTrigger : ActionTrigger
{
    public new DialogAction action;
}
