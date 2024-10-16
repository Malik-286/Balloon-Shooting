using UnityEngine;
using System.Collections;


public class Arrow : MonoBehaviour
{

		public Transform leftClampPoint, rightClampPoint;


		private Vector3 arrowPosition;


		[HideInInspector]
		public bool launched;

	
		[HideInInspector]
		public float power;


		public ArrowHead arrowHead;


		private float powerFactor = 2200;

	
		public static Arrow instance;

		void Awake ()
		{ 
				if (instance == null) {
						instance = this;
				}
		}

		void Start ()
		{
				Transform arrowClamPoints = BowController.instance.transform.Find ("ArrowClampPoints");

				if (rightClampPoint == null) {
						rightClampPoint = arrowClamPoints.Find ("RightClampPoint");
				}

				if (leftClampPoint == null) {
						leftClampPoint = arrowClamPoints.Find ("LeftClampPoint");
				}

				if (arrowHead == null) {
						arrowHead = GameObject.FindObjectOfType<ArrowHead>();
				}
		}

		void Update ()
		{
				if (!launched) {
						ClampPosition ();
						CalculatePower ();
				}
		}

 
		private void ClampPosition ()
		{
 				arrowPosition = transform.position;
				//Clamp the x-position between min and max points
				arrowPosition.x = Mathf.Clamp (arrowPosition.x, Mathf.Min (rightClampPoint.position.x, leftClampPoint.position.x), Mathf.Max (rightClampPoint.position.x, leftClampPoint.position.x));
				//Clamp the y-position between min and max points
				arrowPosition.y = Mathf.Clamp (arrowPosition.y, Mathf.Min (rightClampPoint.position.y, leftClampPoint.position.y), Mathf.Max (rightClampPoint.position.y, leftClampPoint.position.y));
				//Set new position for the arrow
				transform.position = arrowPosition;
		}

		/// <summary>
		/// Calculate the launch power for the arrow.
		/// </summary>
		private void CalculatePower ()
		{
				power = Vector2.Distance (transform.position, rightClampPoint.position) * powerFactor;
		}

		/// <summary>
		/// Destory the arrow and create new one.
		/// </summary>
		public void DestroyArrow ()
		{
				if (Target.instance != null) {
						//Set a random position for the target
						Target.instance.SetRandomPosition ();
				}

				//Create new arrow
				BowController.instance.CreateArrow ();

				//Destroy the current arrow
				Destroy (gameObject);
		}
}