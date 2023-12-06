
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
    [SerializeField] public float _activateManualExplodeRange;

    public bool _maunalExplodeActive;
    //private bool _blink;
    public float _explodeTimer;
    int intTimer;

    public ParticleSystem _explosion;
    public TMPro.TextMeshProUGUI _countdownNumber;


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

        //_explodeTimer = 3;

        base.Start();


        timeRemaining = _explodeTimer;
        StartCoroutine(PulseCanvas());

    }


    protected override void Update()
    {


        intTimer = (int)_explodeTimer;
        _timer += Time.deltaTime;
        
        if (_maunalExplodeActive)
        {

            _explodeTimer -= Time.deltaTime;

            if (_explodeTimer <= 0)
            {
                Debug.Log("Active bomba");
                OnAttack();
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

        base.Update();
    }



    protected override void OnAttack()
    {

        ParticleSystemManager.Instance.PlayParticleFromPool
            (ParticleSystemManager.ParticleEffects.Explosion, transform);
        gameObject.GetComponent<HealthManager>().ReduceHealth(100);
        base.OnAttack();
        //Instantiate(_explosion, this.transform);

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
}
