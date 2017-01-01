using UnityEngine;

namespace Assets.Scripts
{
    public class Charater_Controller : MonoBehaviour
    {

        public int walkSpeed = 2;
        public int rotationSensitivityX = 40;
        public int rotationSensitivityY = 80;
        public int jumpHeight = 15;

        private int enhancedWalkSpeed;
        private Gravity_Controller gravityController;
        private Camera_Controller firstPersonCamera;

        // Use this for initialization
        void Start()
        {
            enhancedWalkSpeed = walkSpeed;
            gravityController = new Gravity_Controller(this.gameObject);
            firstPersonCamera = this.gameObject.GetComponentInChildren<Camera_Controller>();
        }

        // Update is called once per frame
        void Update()
        {
            gravityController.doPhysics();
            moveCharacter();
            moveCamera();

        }

        private void moveCamera()
        {
            firstPersonCamera.tiltCamera(this.rotationSensitivityY);
        }

        private void moveCharacter()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                enhancedWalkSpeed = walkSpeed * 2;
            }
            else
            {
                enhancedWalkSpeed = walkSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gravityController.triggerJump(this.jumpHeight);
            }

            this.transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * enhancedWalkSpeed, Space.Self);
            this.transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * walkSpeed, Space.World);

            this.transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * rotationSensitivityX, Space.World);
        }
    }
}
