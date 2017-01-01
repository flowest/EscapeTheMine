using UnityEngine;

namespace Assets.Scripts
{
    public class Camera_Controller : MonoBehaviour
    {

        private float rotationX = 0;

        public void tiltCamera(int rotationSensitivity)
        {
            //this.transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSensitivity, Space.Self);

            rotationX += Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -50, 60);

            transform.localEulerAngles = new Vector3(-rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }
}
