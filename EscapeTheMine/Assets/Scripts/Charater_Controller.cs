using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Charater_Controller : MonoBehaviour
    {

        public int minerHealth = 10;
        public int walkSpeed = 2;
        public int rotationSensitivityX = 40;
        public int rotationSensitivityY = 80;
        public int jumpHeight = 15;
        public Text staminaCounterTextField;
        public Text collectedStonesCounterTextField;
        public Text pickUpStoneText;
        public int maxStamina = 100;
        public int stamina = 100;
        public int collectedStones = 0;
        public int throwForce = 700;

        private int enhancedWalkSpeed;
        private Gravity_Controller gravityController;
        private Camera_Controller firstPersonCamera;

        private float raycastLength = 0.25f;
        private float raycastOffsetXorZ = 0.6f;
        private float raycastOffsetY = 0.2f;
        private RaycastHit raycastHitted;


        // Use this for initialization
        void Start()
        {
            enhancedWalkSpeed = walkSpeed;
            gravityController = new Gravity_Controller(this.gameObject, new Vector3(0, 0.3f, 0), 0.5f);
            firstPersonCamera = this.gameObject.GetComponentInChildren<Camera_Controller>();

            staminaCounterTextField.text = stamina.ToString();
            collectedStonesCounterTextField.text = collectedStones.ToString();
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
        

        private void updateGUI()
        {
            this.staminaCounterTextField.text = stamina + " %";
            this.collectedStonesCounterTextField.text = collectedStones.ToString();
        }


        private void throwStone()
        {
            if (collectedStones > 0)
            {
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
                if (stamina > 0)
                {
                    stamina--;
                    enhancedWalkSpeed = walkSpeed * 2;
                }
                else
                {
                    enhancedWalkSpeed = walkSpeed;
                }
            }
            else
            {
                enhancedWalkSpeed = walkSpeed;
                if (stamina < maxStamina)
                {
                    stamina++;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gravityController.triggerJump(this.jumpHeight);
            }


            this.transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * enhancedWalkSpeed, Space.Self);
            this.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * walkSpeed, Space.Self);

            this.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * rotationSensitivityX, Space.World);
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
