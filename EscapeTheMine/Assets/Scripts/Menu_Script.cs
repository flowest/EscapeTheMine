using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour {

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
