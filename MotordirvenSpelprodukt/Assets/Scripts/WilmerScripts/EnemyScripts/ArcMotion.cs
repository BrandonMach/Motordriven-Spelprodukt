using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMotion : MonoBehaviour
{
    private Vector3 endPoint;
    public Transform startPoint;
    public float arcHeight = 2.0f;
    public float speed = 2.0f;

    public float journeyLength;
    public float startTime;
    public bool isRunning = false;

    public Vector3 EndPoint { get => endPoint; set => endPoint = value; }

    void Start()
    {

    }

    void Update()
    {
        if (isRunning)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float journeyFraction = distanceCovered / journeyLength;

            Vector3 currentPos = Vector3.Lerp(startPoint.position, EndPoint, journeyFraction);
            currentPos.y = Mathf.Lerp(startPoint.position.y, EndPoint.y, journeyFraction) + Mathf.Sin(journeyFraction * Mathf.PI) * arcHeight;

            transform.position = currentPos;

            

            if (journeyFraction >= 1.0f)
            {
                // Object has reached its destination
                // You can add additional logic here if needed
            } 
        }
    }

    public void StartArcMotion()
    {
        EndPoint = Player.Instance.transform.position;
        journeyLength = Vector3.Distance(startPoint.position, EndPoint);
        startTime = Time.time;

        // Enable a flag to start the arc motion
        isRunning = true;
    }
    public void StopArcMotion()
    {
        isRunning = false;
    }
}
