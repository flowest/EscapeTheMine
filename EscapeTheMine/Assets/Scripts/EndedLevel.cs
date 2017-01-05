using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndedLevel : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Miner_Character")
        {
            SceneManager.LoadScene("Enter_Name");
        }
    }
}
