using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponizedEnemy : MonoBehaviour
{
    public float enemyHP, atkAmt, moveSpeed;
    public bool isPhysical, isRanged;
    public string playerTag;
    public GameObject projectile;

    private EnemyCount eC;
    private Animator anim;
    private float currentHP;

    // Start is called before the first frame update
    void Start()
    {
        eC = FindObjectOfType<EnemyCount>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CloseInPlayer();
    }

    private void OnDestroy()
    {
        eC.enemiesDefeated++;
    }

    public void AttackType()
    {
        if (isPhysical)
        {

        }
        else
        {

        }
    }

    public void CloseInPlayer()
    {
        if (isPhysical)
        {

        }
        else
        {
            moveSpeed = moveSpeed / 2;
        }
    }
}

[CustomEditor(typeof(WeaponizedEnemy))]
public class WEnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Reference to the target script
        WeaponizedEnemy script = (WeaponizedEnemy)target;

        // Draw the default property for 'showExtraSettings'
        script.isRanged = EditorGUILayout.Toggle("isRanged", script.isRanged);

        // Conditionally show 'extraParameter' only if 'showExtraSettings' is true
        if (script.isRanged)
        {
            script.projectile = (GameObject)EditorGUILayout.ObjectField(script.projectile, typeof(GameObject), true);

            if (script.projectile == null)
            {
                EditorGUILayout.HelpBox("There must be a prefab object containing the projectile to work properly!", MessageType.Error);
            }
        }

        // Apply changes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        if (script.enemyHP == 0)
        {
            EditorGUILayout.HelpBox($"{script.gameObject.name}'s HP must have at least more than 0 to work properly.", MessageType.Warning);
        }

        if (script.atkAmt == 0)
        {
            EditorGUILayout.HelpBox($"{script.gameObject.name}'s ATK must have at least more than 0 to work properly.", MessageType.Warning);
        }

        if (script.moveSpeed == 0)
        {
            EditorGUILayout.HelpBox($"{script.gameObject.name}'s speed must be higher than 0, otherwise, it'll be static.", MessageType.Info);
        }

        if (script.playerTag == null)
        {
            EditorGUILayout.HelpBox("There's no tag written for the player, the script won't be able to work properly without it.", MessageType.Warning);
        }
    }
}

