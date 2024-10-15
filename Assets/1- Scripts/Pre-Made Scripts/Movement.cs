using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

public class Movement : MonoBehaviour
{
		public Type type;
		private Type currentType;
		public float speed = 1;
		public float offset = 1;
		private Vector3 pos;
		private float distanceReached;

		// Use this for initialization
		void Start ()
		{
				currentType = type;
				SelectRandomMovement ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				pos = transform.position;
				distanceReached += Time.deltaTime;

				if (currentType == Type.VERTICAL) {
						pos.y += Time.deltaTime * speed;
						if (distanceReached >= offset) {
								distanceReached = 0;
								Flip ();
						}
				} else if (currentType == Type.HORIZONTAL) {
						pos.x += Time.deltaTime * speed;
						if (distanceReached >= offset) {
								distanceReached = 0;
								Flip ();
						}
				}
				transform.position = pos;
		}

		private void Flip ()
		{
				speed *= -1;
		}

		public void SelectRandomMovement ()
		{
				if (type != Type.RANDOM) {
						return;
				}

				int random = Random.Range (1, 3);

				if (random == 1) {
						currentType = Type.HORIZONTAL;
				} else if (random == 2) {
						currentType = Type.VERTICAL;
				} 
		}

		public enum Type
		{
				VERTICAL,
				HORIZONTAL,
				RANDOM}

		;
}
