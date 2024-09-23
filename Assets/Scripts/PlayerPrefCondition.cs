using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPrefCondition : MonoBehaviour
{
    public string playerPrefToReference;
    public int valueNeeded;
    public bool checkInStart;
    public UnityEvent[] toDo;

    private int receivedValue;

    // Start is called before the first frame update
    void Start()
    {
        if (checkInStart)
        {
            receivedValue = PlayerPrefs.GetInt(playerPrefToReference, 0);

            if (receivedValue == valueNeeded)
            {
                EventManagement();
            }
        }
    }

    void EventManagement()
    {
        foreach (var item in toDo)
        {
            item.Invoke();
        }
    }

    public void CheckOnDemand()
    {
        receivedValue = PlayerPrefs.GetInt(playerPrefToReference, 0);

        if (receivedValue == valueNeeded)
        {
            EventManagement();
        }
    }
}
