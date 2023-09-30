using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : CompositeNode
{
    int current;
    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        var child = children[current];
        switch (child.Update())
        {
            case State.Running:
                return State.Running;
                break;
            case State.Failure:
                return State.Failure;
                current++;
                break;
            case State.Success:
                return State.Success;
                break;
        }

        return current == children.Count ? State.Success : State.Failure;
    }

    
}
