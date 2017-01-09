using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Enter_Name_Controller : MonoBehaviour
    {

        public InputField player_name;
        public Text killedBatsCounterText;

        void Start()
        {
            killedBatsCounterText.text = "Killed Bats: " + Main.getKilledBats();
        }


        public void enterToHighscore()
        {
            StartCoroutine(postToServer());
        }

        private IEnumerator postToServer()
        {
            string url = "http://webuser.hs-furtwangen.de/~westphaf/EscapeTheMine/leaderboard.php";
            WWWForm form = new WWWForm();
            form.AddField("PlayerName", player_name.text);
            form.AddField("PointsReached", Main.getKilledBats());

            WWW www = new WWW(url, form);
            yield return www;
            SceneManager.LoadScene("Highscore");
        }
    }
}
