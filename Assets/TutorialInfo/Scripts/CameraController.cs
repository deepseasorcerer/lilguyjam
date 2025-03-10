using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public Transform player;
        public float cameraDistance = 5f;
        public float cameraHeight = 2f;
        public float sensitivity = 100f;
        public float minYAngle = -30f;
        public float maxYAngle = 70f;

        private float mouseX, mouseY;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            mouseX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            mouseY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, minYAngle, maxYAngle);

            transform.position = player.position - Quaternion.Euler(mouseY, mouseX, 0) * Vector3.forward * cameraDistance;
            transform.position += Vector3.up * cameraHeight;

            transform.LookAt(player.position + Vector3.up * 1.5f);
        }
    }
}
