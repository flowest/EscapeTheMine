﻿using UnityEngine;

namespace Assets.Scripts
{
    public class Bat_Controller : MonoBehaviour
    {

        public Transform MinerHeadTransform;
        public int batHealth;
        public int batSpeed;
        public float raycastLength;

        private RaycastHit raycastBatHit;
        private Charater_Controller minerCharacterController;

        // Use this for initialization
        void Start()
        {
            minerCharacterController = MinerHeadTransform.transform.GetComponentInParent<Charater_Controller>();
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.LookAt(MinerHeadTransform);

            raycastToMiner();
        }

        private void raycastToMiner()
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastBatHit, raycastLength))
            {
                if (raycastBatHit.transform.name == "Miner_Character")
                {
                    this.transform.Translate(Vector3.forward * batSpeed * Time.deltaTime, Space.Self);
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.name == "Stone")
            {
                Destroy(collision.transform.gameObject);
                applyDamageBat();
            }

            else if (collision.transform.name == "Miner_Character")
            {
                minerCharacterController.applyDamateToMiner();
                Destroy(this.transform.gameObject);
            }
        }

        private void applyDamageBat()
        {
            batHealth--;
            if (batHealth <= 0)
            {
                Destroy(this.transform.gameObject);
            }
        }
    }
}
