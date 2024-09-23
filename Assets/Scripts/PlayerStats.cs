using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int healthPoints;
    public string gameOverScreen, deathAnim;

    private Animator an;
    private PlayerMovement pM;

    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        pM = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0)
        {
            pM.enabled = false;
            an.SetTrigger(deathAnim);
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene(gameOverScreen);
    }
}
