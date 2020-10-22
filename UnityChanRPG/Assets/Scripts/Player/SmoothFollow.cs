using UnityEngine;

#pragma warning disable 649
namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{
		// The target we are following
		[SerializeField]
		private Transform target;
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 1.0f;
		// the height we want the camera to be above the target
		[SerializeField]
		public float height = 0.2f;

        //Y축 기준 좌우로 회전할 각도
        public float currentRotationAngleY;
        //X축 기준 상하로 회전할 각도
        public float currentRotationAngleX;

        //public float CurrentRotationAngle { get { return currentRotationAngle; } }

        [SerializeField]
		private float rotationDamping;
		[SerializeField]
		private float heightDamping;

		// Use this for initialization
		void Start() 
		{
            target = GameObject.Find("Player").transform;
		}

		// Update is called once per frame
		void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target)
				return;

			// Calculate the current rotation angles
			//var wantedRotationAngle = target.eulerAngles.y;
			var wantedHeight = target.position.y + height; 

			//var currentRotationAngle = transform.eulerAngles.y;
			var currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			//currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
            

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            //var currentRotationAngle = joyCamSc.GetCurrAngle();
            var currentRotation = Quaternion.Euler(0, currentRotationAngleY, 0); // (0, angleY, 0)

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);

            // 카메라의 위치 높이값 범위
            height = Mathf.Clamp(height, -1.3f, 1.3f);

            // Always look at the target
            transform.LookAt(target);
		}
	}
}