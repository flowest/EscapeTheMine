using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Abyss_Controller : MonoBehaviour
{

    private bool isCaughtInAbyss = false;
    private float timeCaughtInAbyssMilliseconds = 0;
    private float timeBeforeSceneSwitchMilliseconds = 2;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Miner_Character")
        {
            isCaughtInAbyss = true;
        }
    }

    void Update()
    {
        if (isCaughtInAbyss)
        {
            timeCaughtInAbyssMilliseconds += Time.deltaTime;
            if (timeCaughtInAbyssMilliseconds > timeBeforeSceneSwitchMilliseconds)
            {
                SceneManager.LoadScene("Game_Over");
            }
        }
    }
}
