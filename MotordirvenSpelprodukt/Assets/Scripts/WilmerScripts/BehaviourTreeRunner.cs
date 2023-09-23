using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;
    private MinionScript enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<MinionScript>();

        tree = tree.Clone(enemyScript);
        
        tree.Bind(GetComponent<NavMeshAgent>());
    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
