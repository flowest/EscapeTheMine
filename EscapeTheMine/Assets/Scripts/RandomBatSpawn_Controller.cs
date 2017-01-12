using UnityEngine;

namespace Assets.Scripts
{
    public class RandomBatSpawn_Controller : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            int randomPosition = (int)Random.Range(0, transform.childCount);
            Transform randomTransform = this.transform.GetChild(randomPosition).transform;
            GameObject randomSpawnedBat = (GameObject)Instantiate(Resources.Load("Bat"), randomTransform,false);
            randomSpawnedBat.GetComponent<Bat_Controller>().MinerHeadTransform = GameObject.Find("Character_Camera").transform;
            randomSpawnedBat.transform.Translate(new Vector3(0,0,0),Space.Self);
            randomSpawnedBat.name = "Bat";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
