using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
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

        foreach (var child in children)
        {
            if (child.Update() != State.Failure)
            {
                return child.Update();
            }
        }

        return State.Failure;
    }
}
