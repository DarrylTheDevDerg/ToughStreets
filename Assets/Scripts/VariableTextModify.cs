using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class VariableTextModify : MonoBehaviour
{
    public string valueName;
    public TextMeshProUGUI text;

    private PlayerAttack pA;


    // Start is called before the first frame update
    void Start()
    {
        pA = FindObjectOfType<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        ReflectValue();
    }

    public void ReflectValue()
    {
        text.text = pA.GetStat(valueName).ToString();
    }
}
