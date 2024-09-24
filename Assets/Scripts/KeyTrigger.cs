using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyTrigger : MonoBehaviour
{
    public KeyCode key;
    public UnityEvent[] toDo;

    private bool once;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && !once)
        {
            foreach (var e in toDo)
            {
                e.Invoke();
            }

            once = true;
        }
    }
}
