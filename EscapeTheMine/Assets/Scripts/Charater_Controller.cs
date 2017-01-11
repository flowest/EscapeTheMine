using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Charater_Controller : MonoBehaviour
    {

        public int minerHealth;
        private int walkSpeed;
        private int rotationSensitivityX = 70;
        private int rotationSensitivityY = 80;
        private int jumpHeight = 15;
        public Text staminaCounterTextField;
        public Text collectedStonesCounterTextField;
        public Text pickUpStoneText;
        public Text healthCounterText;
        private int maxStamina;
        private int currentStamina;
        public int collectedStones = 0;
        public int throwForce;

        private int enhancedWalkSpeed;
        private Gravity_Controller gravityController;
        private Camera_Controller firstPersonCamera;
        public AudioSource thrwoStoneAudioSource;
        public AudioSource walkAudioSource;

        // Use this for initialization
        void Start()
        {
            configureMiner();
            enhancedWalkSpeed = walkSpeed;
            gravityController = new Gravity_Controller(this.gameObject, new Vector3(0, 0.3f, 0), 0.5f);
            firstPersonCamera = this.gameObject.GetComponentInChildren<Camera_Controller>();

            staminaCounterTextField.text = currentStamina.ToString();
            collectedStonesCounterTextField.text = collectedStones.ToString();
            healthCounterText.text = minerHealth.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            gravityController.doPhysics();
            moveCharacter();
            moveCamera();

            if (Input.GetMouseButtonDown(0))
            {
                throwStone();
            }

            updateGUI();

        }

        private void configureMiner()
        {
            MinerConfigData minerConfigData = Main.getConfigData();

            this.minerHealth = minerConfigData.getHealth();
            this.walkSpeed = minerConfigData.getWalkSpeed();
            this.maxStamina = minerConfigData.getMaxStamina();
            this.currentStamina = maxStamina;
            this.throwForce = minerConfigData.getThrowForce();
        }

        private void updateGUI()
        {
            this.staminaCounterTextField.text = currentStamina + " %";
            this.collectedStonesCounterTextField.text = collectedStones.ToString();
            healthCounterText.text = minerHealth.ToString();
        }


        private void throwStone()
        {
            if (collectedStones > 0)
            {
                thrwoStoneAudioSource.Play();
                collectedStones--;
                GameObject throwedStone = (GameObject)Instantiate(Resources.Load("Stone"), new Vector3(this.transform.localPosition.x, this.transform.position.y + 2.5f, this.transform.position.z), this.transform.rotation);
                throwedStone.name = "Stone";
                throwedStone.GetComponent<Rigidbody>().AddForce(firstPersonCamera.transform.forward * throwForce);
            }
        }


        private void moveCamera()
        {
            firstPersonCamera.tiltCamera(this.rotationSensitivityY);
        }

        private void moveCharacter()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                if (currentStamina > 0)
                {
                    currentStamina--;
                    enhancedWalkSpeed = walkSpeed * 2;
                    walkAudioSource.pitch = 2;
                }
                else
                {
                    enhancedWalkSpeed = walkSpeed;
                    walkAudioSource.pitch = 1;
                }
            }
            else
            {
                enhancedWalkSpeed = walkSpeed;
                walkAudioSource.pitch = 1;
                if (currentStamina < maxStamina)
                {
                    currentStamina++;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gravityController.triggerJump(this.jumpHeight);
            }


            this.transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * enhancedWalkSpeed, Space.Self);
            this.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * walkSpeed, Space.Self);

            this.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * rotationSensitivityX, Space.World);

            if (Input.GetAxis("Vertical") == 1 || Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Vertical") == -1 || Input.GetAxis("Horizontal") == -1)
            {
                if (walkAudioSource.isPlaying == false)
                {
                    walkAudioSource.Play();
                }
            }
            else
            {
                walkAudioSource.Stop();
            }
        }

        public void pickUpRaycastHitted(RaycastHit raycastHit)
        {
            pickUpStoneText.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                collectedStones++;
                Destroy(raycastHit.transform.gameObject);
            }
        }

        public void disablePickUpText()
        {
            pickUpStoneText.enabled = false;
        }

        public void applyDamateToMiner()
        {
            minerHealth--;
            if (minerHealth <= 0)
            {
                Debug.Log("Miner died");
            }
        }
    }
}
