using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Charater_Controller : MonoBehaviour
    {

        public int walkSpeed = 2;
        public int rotationSensitivityX = 40;
        public int rotationSensitivityY = 80;
        public int jumpHeight = 15;
        public Text staminaCounterTextField;
        public Text collectedStonesCounterTextField;
        public int stamina = 100;
        public int collectedStones = 0;

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
            gravityController = new Gravity_Controller(this.gameObject);
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

            checkForCollisionWithWalls();

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
            }
        }

        private void checkForCollisionWithWalls()
        {
            Vector3 resetPosition = this.transform.position;
            float resetDistanceFromWall = raycastOffsetXorZ + raycastLength;

            if (Physics.Raycast(this.transform.position + new Vector3(raycastOffsetXorZ * -1, raycastOffsetY, 0), Vector3.left, out raycastHitted, raycastLength))
            {
                resetPosition.x = raycastHitted.point.x + resetDistanceFromWall;
            }
            if (Physics.Raycast(this.transform.position + new Vector3(0, raycastOffsetY, raycastOffsetXorZ), Vector3.forward, out raycastHitted, raycastLength))
            {
                resetPosition.z = raycastHitted.point.z - resetDistanceFromWall;
            }
            if (Physics.Raycast(this.transform.position + new Vector3(raycastOffsetXorZ, raycastOffsetY, 0), Vector3.right, out raycastHitted, raycastLength))
            {
                resetPosition.x = raycastHitted.point.x - resetDistanceFromWall;
            }
            if (Physics.Raycast(this.transform.position + new Vector3(0, raycastOffsetY, raycastOffsetXorZ * -1), Vector3.back, out raycastHitted, raycastLength))
            {
                resetPosition.z = raycastHitted.point.z + resetDistanceFromWall;
            }

            this.transform.position = resetPosition;
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
                if (stamina < 100)
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
    }
}
