using UnityEngine;
using System.Collections;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

/// <summary>
/// Arrow script.
/// </summary>
public class Arrow : MonoBehaviour
{
		/// <summary>
		/// The left,right clamp point for the arrow.
		/// </summary>
		public Transform leftClampPoint, rightClampPoint;

		/// <summary>
		/// The arrow position.
		/// </summary>
		private Vector3 arrowPosition;

		/// <summary>
		/// Whether the arrow is launched.
		/// </summary>
		[HideInInspector]
		public bool launched;

		/// <summary>
		/// The launch power for the arrow.
		/// </summary>
		[HideInInspector]
		public float power;

		/// <summary>
		/// The arrow head reference.
		/// </summary>
		public ArrowHead arrowHead;

		/// <summary>
		/// The power factor.
		/// </summary>
		private float powerFactor = 2000;

		/// <summary>
		/// The static instance for this class.
		/// </summary>
		public static Arrow instance;

		void Awake ()
		{ 
				//Setting up the instance
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

		/// <summary>
		/// Clamp the position of the arrow.
		/// </summary>
		private void ClampPosition ()
		{
				//Get the position of the arrow
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