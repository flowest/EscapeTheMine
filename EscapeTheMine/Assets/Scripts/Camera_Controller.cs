using UnityEngine;

namespace Assets.Scripts
{
    public class Camera_Controller : MonoBehaviour
    {

        private float timer = 2f;

        private float rotationX = 0;
        private RaycastHit raycastPickUp;

        private Light flashLight;

        void Start()
        {
            flashLight = this.transform.GetComponentInChildren<Light>();
        }

        void Update()
        {

            timer -= Time.deltaTime;

            if (timer < 0)
            {
                if (flashLight.enabled == true)
                {
                    flashLight.enabled = false;
                }
                else if (flashLight.enabled == false)
                {
                    flashLight.enabled = true;
                }
                timer = Random.value / 3;
            }
        }

        public void tiltCamera(int rotationSensitivity)
        {
            rotationX += Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -70, 60);

            transform.localEulerAngles = new Vector3(-rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);

            sendPickUpRaycast();
        }

        private void sendPickUpRaycast()
        {

            if (Physics.Raycast(this.transform.position, transform.forward, out raycastPickUp, 2))
            {
                if (raycastPickUp.transform.name == "Stone")
                {
                    SendMessageUpwards("pickUpRaycastHitted", raycastPickUp);
                }
            }
            else
            {
                SendMessageUpwards("disablePickUpText");
            }
        }
    }
}
