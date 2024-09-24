using UnityEngine;
using UnityEngine.SceneManagement;

public class EasySceneLoader : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
