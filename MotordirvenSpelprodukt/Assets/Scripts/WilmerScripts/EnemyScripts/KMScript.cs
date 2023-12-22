
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class KMScript : MinionScript
{

    public Animator Anim;
    public float ChaseDistance;

    [Header("Attack")]
    //[SerializeField] private Collider expolisionHitbox;
    [SerializeField] public float _automaticExplodeRange;
    [SerializeField] public float _exploadRange;

    public bool _maunalExplodeActive;
    //private bool _blink;
    public float _explodeTimer;
    //int intTimer;

    public ParticleSystem _explosion;
    public TMPro.TextMeshProUGUI _countdownNumber;
    HealthManager hpManager;

    [SerializeField] public Image bombImage;

    private float _pulseTimer;
    private float _timer;
    private float timeRemaining;
    private bool isRed = false;

    protected override void Start()
    {
        _timer = 0;
        _pulseTimer = _explodeTimer;
        ChaseDistance = 2;
        CurrentState = EnemyState.chasing;

        //_explodeTimer = 3;

        hpManager = GetComponent<HealthManager>();

        base.Start();


        timeRemaining = _explodeTimer;
        StartCoroutine(PulseCanvas());

    }

    protected override void HandleChase()
    {
        RB.velocity = transform.forward * MovementSpeed;
        RB.AddForce(Vector3.down * RB.mass * 9.81f, ForceMode.Force);
        FacePlayer();
    }


    protected override void Update()
    {
        base.Update();
        CurrentState = EnemyState.chasing;
        _timer += Time.deltaTime;

        if (DistanceToPlayer < _automaticExplodeRange)
        {
            _maunalExplodeActive = true;
        }   
        
        if (_maunalExplodeActive)
        {
            _explodeTimer -= Time.deltaTime;

            if (_explodeTimer <= 0)
            {
                //Debug.Log("Active bomba");
            
                gameObject.SetActive(false);

                Destroy(gameObject);
          
            }
        }

        // Update the countdown timer
        timeRemaining -= Time.deltaTime;

        // You can perform additional actions based on the countdown, if needed
        if (timeRemaining <= 0f)
        {
            // Handle when the countdown reaches zero
            StopCoroutine(PulseCanvas());
            bombImage.color = Color.red;
        }
        if (DistanceToPlayer <= _exploadRange)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            

        }
    }


    private void OnDestroy()
    {

        
        Explode();
        base.OnDestroy();

    }


    public void Explode()
    {
        PlayExplosionSound();

        MovementSpeed = 0;
        ParticleSystemManager.Instance.PlayParticleFromPool
            (ParticleSystemManager.ParticleEffects.Explosion, transform);
        OnAttack();
        
        Debug.Log("Deactivating KM");
    }


    

    IEnumerator PulseCanvas()
    {

        float initialPulseFrequency = 1f; // Adjust this value for the initial pulsating frequency
        float finalPulseFrequency = 10f;  // Adjust this value for the final pulsating frequency
        float pulseFrequency;

        while (timeRemaining > 0f)
        {
            // Calculate the pulsating frequency based on the remaining time
            float t = 1f - (timeRemaining / _pulseTimer);
            pulseFrequency = Mathf.Lerp(initialPulseFrequency, finalPulseFrequency, t);

            // Calculate the alpha value based on the ping-pong effect with the dynamically adjusted frequency
            float alpha = Mathf.PingPong(_timer * pulseFrequency, 1f);

            // Set the canvas color with the calculated alpha
            bombImage.color = new Color(1f, 0f, 0f, alpha);

            // Wait for a short duration before the next iteration
            yield return null;
        }
    }

    #region FMOD

    private void PlayExplosionSound()
    {
        string eventPath;
        int random = Random.Range(1, 4);

        if (random == 1)
        {
            eventPath = "event:/explosion1";
        }
        else if (random == 2)
        {
            eventPath = "event:/explosion2";
        }
        else
        {
            eventPath = "event:/explosion3";
        }

        FMOD.Studio.EventInstance explosion = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(explosion, this.transform);
        explosion.start();
        explosion.release();
    }


    #endregion
}
