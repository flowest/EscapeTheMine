using UnityEngine;

namespace Assets.Scripts
{
    public class Gravity_Controller : MonoBehaviour
    {

        public int gravity = 1;
        public int deltaY = 1;

        private readonly GameObject objectToDoPhysicsOn;
        private bool isOnGround = false;
        private RaycastHit raycastHit;

        public Gravity_Controller(GameObject physicsObject)
        {
            this.objectToDoPhysicsOn = physicsObject;
        }

        public void doPhysics()
        {
            if (isOnGround == false)
            {
                deltaY += gravity;
                objectToDoPhysicsOn.transform.Translate(Vector3.down * Time.deltaTime * deltaY, Space.World);
            }

            if (Physics.Raycast(objectToDoPhysicsOn.transform.position + new Vector3(0, 0.3f, 0), Vector3.down, out raycastHit, 0.5f))
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
