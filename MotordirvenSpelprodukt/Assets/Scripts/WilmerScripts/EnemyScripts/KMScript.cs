
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


    private float timeRemaining;
    private bool isRed = false;

    protected override void Start()
    {

        
        ChaseDistance = 2;

        //_explodeTimer = 3;

        base.Start();


        timeRemaining = _explodeTimer;
        StartCoroutine(PulseCanvas());

    }


    protected override void Update()
    {


        intTimer = (int)_explodeTimer;


        if (_maunalExplodeActive)
        {

            //_countdownNumber.text = (1+intTimer).ToString();



            //if (1 + intTimer % 2 != 1)
            //{
            //    bombImage.color = Color.red;
            //}
            //else bombImage.color = Color.black;

            _explodeTimer -= Time.deltaTime;
            //Debug.Log("Active bomb timer: " + _explodeTimer);
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
            //bombImage.color = Color.red;
        }

        base.Update();
    }



    protected override void OnAttack()
    {


        gameObject.GetComponent<HealthManager>().ReduceHealth(100);
        base.OnAttack();
        //Instantiate(_explosion, this.transform);

    }
    

    IEnumerator BlinkCanvas()
    {
        while (timeRemaining > 0f)
        {
            // Toggle the color of the canvas between red and its original color
            if (isRed)
            {
                //bombImage.color = Color.Lerp(Color.black, Color.red, Mathf.PingPong(Time.time, 1f));
                bombImage.color = Color.red;
            }
            else
            {
                bombImage.color = Color.black;
            }

            // Toggle the flag
            isRed = !isRed;


            // Adjust blinking rate based on the remaining time
            float blinkSpeedFactor = 1f - (timeRemaining / _explodeTimer);
            float blinkSpeed = Mathf.Lerp(1f, 5f, blinkSpeedFactor* blinkSpeedFactor);

            // Wait for a short duration before the next iteration
            yield return new WaitForSeconds(1f / blinkSpeed);
        }
    }

    IEnumerator PulseCanvas()
    {
        //while (timeRemaining > 0f)
        //{
        //    // Calculate the alpha value based on the ping-pong effect
        //    float alpha = Mathf.PingPong(Time.time * (1f + timeRemaining / _explodeTimer), 1f);

        //    // Set the canvas color with the calculated alpha
        //    bombImage.color = new Color(1f, 0f, 0f, alpha);

        //    // Wait for a short duration before the next iteration
        //    yield return null;
        //}
        float initialPulseFrequency = 1f; // Adjust this value for the initial pulsating frequency
        float finalPulseFrequency = 10f;  // Adjust this value for the final pulsating frequency
        float pulseFrequency;

        while (timeRemaining > 0f)
        {
            // Calculate the pulsating frequency based on the remaining time
            float t = 1f - (timeRemaining / _explodeTimer);
            pulseFrequency = Mathf.Lerp(initialPulseFrequency, finalPulseFrequency, t);

            // Calculate the alpha value based on the ping-pong effect with the dynamically adjusted frequency
            float alpha = Mathf.PingPong(Time.time * pulseFrequency, 1f);

            // Set the canvas color with the calculated alpha
            bombImage.color = new Color(1f, 0f, 0f, alpha);

            // Wait for a short duration before the next iteration
            yield return null;
        }
    }
}
