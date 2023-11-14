using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMotion : MonoBehaviour
{
    private Transform endPoint;
    public Transform startPoint;
    public float arcHeight = 2.0f;
    public float speed = 2.0f;

    public float journeyLength;
    public float startTime;
    public bool isRunning = false;

    public Transform EndPoint { get => endPoint; set => endPoint = value; }

    void Start()
    {
        
        //journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        //startTime = Time.time;
    }

    void Update()
    {
        if (isRunning)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float journeyFraction = distanceCovered / journeyLength;

            Vector3 arcMidPoint = startPoint.position + (EndPoint.position - startPoint.position) * 0.5f;
            arcMidPoint += Vector3.up * arcHeight;

            Vector3 currentPos = Vector3.Lerp(startPoint.position, EndPoint.position, journeyFraction);
            currentPos.y = Mathf.Lerp(startPoint.position.y, EndPoint.position.y, journeyFraction) + Mathf.Sin(journeyFraction * Mathf.PI) * arcHeight;

            transform.position = currentPos;

            if (journeyFraction >= 1.0f)
            {
                // Object has reached its destination
                // You can add additional logic here if needed
            } 
        }
    }
    //public void StartArcMotion(Transform player)
    //{
    //    endPoint = player;
    //    journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
    //    startTime = Time.time;

    //    // Enable a flag to start the arc motion
    //    isRunning = true;
    //}
    //public void StopArcMotion()
    //{

    //    isRunning = false;
    //}
}
