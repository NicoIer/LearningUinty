
namespace Control
{
    using UnityEngine;

    public class CameraControl:MonoBehaviour
    {
        [SerializeField]private Camera mainCamera;
        [SerializeField]private Transform target;
        [Header("Attribute")]
        [SerializeField]private float upDistance;//垂直方向距离
        [SerializeField]private float rightDistance;//水平方向距离
        [SerializeField]private float smooth = 2f;
        private void Awake()
        {
            mainCamera = Camera.main;
            if (target == null)
            {
                target = transform;
            }
        }
        
        private void FixedUpdate()
        {
            FollowTarget();//
        }

        private void FollowTarget()
        {
            var targetPosition = target.position + Vector3.up * upDistance + Vector3.right * rightDistance;
            targetPosition.z = mainCamera.transform.position.z;
            var originPosition = mainCamera.transform.position;
            mainCamera.transform.position = Vector3.Lerp(originPosition, targetPosition, Time.deltaTime * smooth);
        }

    }
}


 
