using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

public class Point : MonoBehaviour
{
		/// <summary>
		/// The image reference of the point.
		/// </summary>
		public SpriteRenderer image;

		/// <summary>
		/// Whether a point is collided with the target or not.
		/// </summary>
		public bool targetTriggered;

		// Use this for initialization
		void Start ()
		{
				if (image == null) {
						image = GetComponent<SpriteRenderer> ();
				}

				enabled = true;
				HideResources ();
		}

		void OnTriggerEnter2D(Collider2D col){
				if (col.tag == "Target") {
						targetTriggered = true;
				}

        if (col.tag == "Arrow")
        {
            Destroy(col.gameObject);
			if (AudioManager.Instance)
			{
            AudioManager.Instance.PlayBalloonPopupSoundWEffect();
			}
            Destroy(gameObject);
        }
    }

		void OnTriggerExit2D(Collider2D col){
				if (col.tag == "Target") {
						targetTriggered = false;
				}
        if (col.tag == "Arrow")
        {
            Destroy(col.gameObject);
            if (AudioManager.Instance)
            {
                AudioManager.Instance.PlayBalloonPopupSoundWEffect();
            }
        }
    }

		/// <summary>
		/// Hide the resources of the point.
		/// </summary>
		public void HideResources ()
		{
				image.enabled = false;
		}

		/// <summary>
		/// Show the resources of the point.
		/// </summary>
		public void ShowResources ()
		{
				image.enabled = true;
		}

		/// <summary>
		/// Reset the point
		/// </summary>
		public void Reset ()
		{
				HideResources ();
				transform.position = Vector3.zero;
		}
}
