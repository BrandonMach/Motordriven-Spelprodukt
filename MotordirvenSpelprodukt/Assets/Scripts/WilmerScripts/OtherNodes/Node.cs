using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public abstract class Node : ScriptableObject
{
    public enum State { Running, Failure, Success}
    
    [HideInInspector] public State state = State.Running;
    [HideInInspector] public bool started = false;
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;
    [HideInInspector] public Blackboard blackboard;
    [HideInInspector] public NavMeshAgent agent;
    [TextArea] public string description;
    protected MMScript _meleeMinionScript;
    //protected RangedMinionScript rangedMinionScript;
    protected GameObject _enemyObject;
    protected CMPScript _championScript;
    protected CMP1Script _champion1Script;
    protected KMScript _kamikazeScript;



    public GameObject EnemyObject
    {
        //get { return enemyScript; }
        set { _enemyObject = value; }
    }
    public MMScript MeleeMinionScript
    {
        //get { return enemyScript; }
        set { _meleeMinionScript = value; }
    }

    public State Update()
    {
        if (!started) 
        { 
            OnStart(); 
            started = true; 
        }

        state = OnUpdate();
        
        if(state == State.Failure || state == State.Success) 
        { 
            OnStop();
            started = false; 
        }

        return state;
    }

    public virtual Node Clone()
    {
        return Instantiate(this);
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate();
}
