using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMStartManualExplode : ActionNode
{
    protected override void OnStart()
    {
        _kamikazeScript = _enemyObject.GetComponent<KMScript>();
        _playerScript = Player.Instance;


    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        //if (Vector3.Distance(_kamikazeScript.transform.position, _playerScript.transform.position) <= _kamikazeScript._activateManualExplodeRange)
        //{
        if (_kamikazeScript.DistanceToPlayer < _kamikazeScript._exploadRange)
        {
            //ParticleSystemManager.Instance.PlayParticleFromPool
                //(ParticleSystemManager.ParticleEffects.Explosion, _kamikazeScript.transform);
            _kamikazeScript.Explode();
            //_kamikazeScript.Anim.Play("Dive");
            //_kamikazeScript.Anim.SetTrigger("Dive");
         
            //Debug.Log("Kamikaze");
            return State.Success;
        }
        else
        {
            return State.Failure;
        }
    }
}
