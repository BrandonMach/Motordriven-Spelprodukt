using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogNode : ActionNode
{
    public string message;
    protected override void OnStart()
    {
        Debug.Log($"Onstart{message}");
    }
    protected override void OnStop()
    {
        Debug.Log($"Onstop{message}");
    }
    protected override State OnUpdate()
    {
        Debug.Log($"OnUpdate{message}");

        Debug.Log($"Blackboard:{blackboard.moveToPosition}");

        blackboard.moveToPosition.x += 1;

        return State.Success;
    }
}
