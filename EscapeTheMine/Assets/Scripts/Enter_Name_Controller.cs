using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Enter_Name_Controller : MonoBehaviour
    {

        public InputField player_name;

        public void enterToHighscore()
        {
            StartCoroutine(postToServer());
        }

        private IEnumerator postToServer()
        {
            string url = "http://webuser.hs-furtwangen.de/~westphaf/EscapeTheMine/leaderboard.php";
            WWWForm form = new WWWForm();
            form.AddField("PlayerName", player_name.text);
            form.AddField("PointsReached", 99);

            WWW www = new WWW(url, form);
            yield return www;
            SceneManager.LoadScene("Highscore");
        }
    }
}
