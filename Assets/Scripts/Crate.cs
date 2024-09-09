using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public float crateHP;
    private CrateDrop cD;

    private Vector3 currentPos;
    private Quaternion currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        cD = GetComponent<CrateDrop>();
    }

    // Update is called once per frame
    void Update()
    {

        if (crateHP <= 0)
        {
            cD.DropItems();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        crateHP -= damage;
    }
}
