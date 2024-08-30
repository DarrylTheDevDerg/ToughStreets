using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMgmt : MonoBehaviour
{
    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateObjects()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }

    public void DeactivateObjects()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
}
