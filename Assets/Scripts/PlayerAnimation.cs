using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Punch Triggers")]
    public string triggerP1;
    public string triggerP2;
    public string triggerP3;

    [Header("Kick Triggers")]
    public string triggerK1;
    public string triggerK2;
    public string triggerK3;

    private Animator pAn;

    private void Awake()
    {
        pAn = GetComponent<Animator>();
    }

    public void Punch1()
    {
        pAn.SetTrigger(triggerP1);
    }

    public void Punch2()
    {
        pAn.SetTrigger(triggerP2);
    }

    public void Punch3()
    {
        pAn.SetTrigger(triggerP3);
    }

    public void Kick1()
    {
        pAn.SetTrigger(triggerK1);
    }

    public void Kick2()
    {
        pAn.SetTrigger(triggerK2);
    }

    public void Kick3()
    {
        pAn.SetTrigger(triggerK3);
    }
}
