using System.Collections;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Highscore_Controller : MonoBehaviour
    {

        public Text highscoreListText;

        private XmlDocument highscoreXML;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(getHighscoreFile());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator getHighscoreFile()
        {
            string url = "http://webuser.hs-furtwangen.de/~westphaf/EscapeTheMine/leaderboard.xml";

            WWW www = new WWW(url);
            yield return www;

            highscoreXML = new XmlDocument();
            highscoreXML.LoadXml(www.text);

            XmlNodeList players = highscoreXML.SelectNodes("LeaderBoard/Player");

            foreach (XmlNode player in players)
            {
                highscoreListText.text += player.SelectSingleNode("Name").InnerText + ": " + player.SelectSingleNode("Points").InnerText + " Points"+ "\n";
            }
        }
    }
}
