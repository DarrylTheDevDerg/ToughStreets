using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimatorParameter
{
    public string Name;
    public bool Value;
}

[System.Serializable]
public class AnimatorTrigger
{
    public string triggerName;
}

public class AnimatorParameters : MonoBehaviour
{
    public List<AnimatorParameter> animatorParams = new List<AnimatorParameter>();

    private Animator animator;  // Reference to the Animator component
    public List<AnimatorTrigger> animatorTriggers = new List<AnimatorTrigger>();

    void Start()
    {
        animator = GetComponent<Animator>();

        SetBool();
    }

    public void SetBool()
    {
        foreach (var param in animatorParams)
        {
            animator.SetBool(param.Name, param.Value);
        }
    }

    public void SetTrigger(string triggerName)
    {
        foreach (var trigger in animatorTriggers)
        {
            if (trigger.triggerName == triggerName)
            {
                animator.SetTrigger(trigger.triggerName);
                break;
            }
        }
    }
}
