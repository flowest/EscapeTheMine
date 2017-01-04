using UnityEngine;

namespace Assets.Scripts
{
    public class Gravity_Controller : MonoBehaviour
    {

        public int gravity = 1;
        public int deltaY = 1;

        private readonly GameObject objectToDoPhysicsOn;
        private readonly Vector3 offsetVector;
        private readonly float raycastLength;
        private bool isOnGround = false;
        private RaycastHit raycastHit;

        public Gravity_Controller(GameObject physicsObject, Vector3 offsetVector, float raycastLength)
        {
            this.objectToDoPhysicsOn = physicsObject;
            this.offsetVector = offsetVector;
            this.raycastLength = raycastLength;
        }

        public void doPhysics()
        {
            if (isOnGround == false)
            {
                deltaY += gravity;
                objectToDoPhysicsOn.transform.Translate(Vector3.down * Time.deltaTime * deltaY, Space.World);
            }

            if (Physics.Raycast(objectToDoPhysicsOn.transform.position + offsetVector, Vector3.down, out raycastHit, raycastLength))
            {
                objectToDoPhysicsOn.transform.position = raycastHit.point;
                isOnGround = true;
                deltaY = 1;
            }

            else
            {
                isOnGround = false;
            }
        }

        public void triggerJump(int jumpHeight)
        {
            if (isOnGround)
            {
                isOnGround = false;
                deltaY = -jumpHeight;
            }
        }

    }
}
