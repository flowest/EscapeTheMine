using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour {

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Start()
    {
        Main.loadConfigXML();
    }
}
