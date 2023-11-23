using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMScript : MinionScript
{

    public Animator Anim;
    public float ChaseDistance;


    [Header("Attack")]
    //[SerializeField] private Collider expolisionHitbox;
    [SerializeField] public float _automaticExplodeRange;
    [SerializeField] public float _activateManualExplodeRange;

    public bool _maunalExplodeActive;
    
    float _explodeTimer;
    int intTimer;

    public ParticleSystem _explosion;
    public TMPro.TextMeshProUGUI _countdownNumber;

    protected override void Start()
    {
       
        
        ChaseDistance = 2;

        _explodeTimer = 3;
       
        base.Start();

    }


    protected override void Update()
    {


        intTimer = (int)_explodeTimer;


        if (_maunalExplodeActive)
        {
            
            _countdownNumber.text = (1+intTimer).ToString();
            
            _explodeTimer -= Time.deltaTime;
            Debug.Log("Active bomb timer: " + _explodeTimer);
            if (_explodeTimer <= 0)
            {
                Debug.Log("Active bomba");
                OnAttack();
            }
        }


        base.Update();
    }
  


    protected override void OnAttack()
    {


        gameObject.GetComponent<HealthManager>().ReduceHealth(100);
        base.OnAttack();
        //Instantiate(_explosion, this.transform);
       
    }
}
