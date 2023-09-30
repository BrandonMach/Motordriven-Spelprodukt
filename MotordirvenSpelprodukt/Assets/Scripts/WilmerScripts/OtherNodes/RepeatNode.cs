using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : DecoratorNode
{
    public bool RestartOnFailure;
    public bool RestartOnSuccess;
    int _iterationCount = 0;
    public int maxRepeats = 0;
    protected override void OnStart()
    {
        _iterationCount = 0;
    }
    protected override void OnStop()
    {

    }
    //protected override State OnUpdate()
    //{
    //    child.Update();
    //    return State.Running; // Måste ändras
    //}
    protected override State OnUpdate()
    {
        
        if (child == null)
        {
            return State.Failure;
        }

        switch (child.Update())
        {
            case State.Running:
                break;
            case State.Failure:
                if (RestartOnFailure)
                {
                    _iterationCount++;
                    if (_iterationCount >= maxRepeats && maxRepeats > 0)
                    {
                        return State.Failure;
                    }
                    else
                    {
                        return State.Running;
                    }
                }
                else
                {
                    return State.Failure;
                }
            case State.Success:
                if (RestartOnSuccess)
                {
                    _iterationCount++;
                    if (_iterationCount >= maxRepeats && maxRepeats > 0)
                    {
                        return State.Success;
                    }
                    else
                    {
                        return State.Running;
                    }
                }
                else
                {
                    return State.Success;
                }
        }
        return State.Running;
    }
}
