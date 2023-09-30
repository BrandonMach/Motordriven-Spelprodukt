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
        //for (int i = current; i < children.Count; ++i)
        //{
        //    current = i;
        //    var child = children[current];

        //    switch (child.Update())
        //    {
        //        case State.Running:
        //            return State.Running;
        //        case State.Success:
        //            return State.Success;
        //        case State.Failure:
        //            continue;
        //    }
        //}

        //return State.Failure;

        foreach (var item in children)
        {
            if(item.Update() != State.Failure)
            {
                return item.Update();
            }
        }

        return State.Failure;


    }




}
