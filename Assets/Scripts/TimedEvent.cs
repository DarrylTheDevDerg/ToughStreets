using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : MonoBehaviour
{
    public UnityEvent[] eventsTargeted;
    public float timeThreshold;

    private float currentTime;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > timeThreshold)
        {
            EventActivation();
        }
    }

    public void EventActivation()
    {
        foreach (var e in eventsTargeted)
        {
            e.Invoke();
        }
    }
}