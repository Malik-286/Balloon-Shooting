using UnityEngine;
using System.Collections;


public class ArrowHead : MonoBehaviour
{

		private string collisionName;

	
		private Transform arrowTransform;

	
		public Sprite[] plusScore;

	
		private static Animator plusScoreEffectAnimator;

	
		private static Animator plusArrowEffectAnimator;

	
		private static SpriteRenderer plusScoreEffectSpriteRenderer;

		public GameObject plusScoreEffectGameObject;

	
		public GameObject plusArrowEffectGameObject;

		GameManager gameManager;

		void Start ()
		{
				gameManager = FindObjectOfType<GameManager>();

				//Setting up the references
				if (arrowTransform == null) {
						arrowTransform = transform.parent;
				}

				if (plusScoreEffectGameObject == null) {
						plusScoreEffectGameObject = GameObject.Find ("PlusScoreEffect");
				}

				if (plusArrowEffectGameObject == null) {
						plusArrowEffectGameObject = GameObject.Find ("PlusArrowEffect");
				}

				//plusScoreEffectAnimator = plusScoreEffectGameObject.GetComponent<Animator> ();
				//plusArrowEffectAnimator = plusArrowEffectGameObject.GetComponent<Animator> ();
				//plusScoreEffectSpriteRenderer = plusScoreEffectGameObject.GetComponent<SpriteRenderer> ();
		}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Balloon")
        {
			if (MissionManager.Instance)
			{
				MissionManager.Instance.SmashedBallons += 1;
				MissionManager.Instance.UpdateBalloonsCounter();
			}
            collision.gameObject.GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			collision.gameObject.GetComponent<Balloon>().StopMoving = true;
            if (AudioManager.Instance)
            {
                AudioManager.Instance.PlayBalloonPopupSoundWEffect();
            }
            Destroy(collision.gameObject,5f);
        }
        if (collision.gameObject.tag == "Grenade")
        {
			if (Grenade.Instance)
			{
                Grenade.Instance.GrenadeHit();
			}
        }

    }
    void OnCollisionEnter2D (Collision2D col)
		{

        //On collision with the target collider
        if (col.transform.tag == "TargetCollider") {
						//Get the first contacts point
						if(col.contacts.Length !=0)
							arrowTransform.transform.position = col.contacts [0].point;
						
						//Hide arrow head
						gameObject.SetActive (false);

						//Set arrow parent to target
						gameObject.transform.parent.SetParent (col.transform);

						//Fix the arrow position
						arrowTransform.GetComponent<Rigidbody2D>().isKinematic = true;
						arrowTransform.GetComponent<Rigidbody2D>().simulated = false;

						//Get the collision name
						collisionName = col.transform.name;

						if (collisionName == "50Point-Collider") {
								//Add 2 extra arrow
								DataManager.NumberOfArrows += 2;
								DataManager.CurrentScore += 50;//Add 50 points
								plusScoreEffectSpriteRenderer.sprite = plusScore [4];
				
								//Show the plus arrow effect
								plusArrowEffectAnimator.SetTrigger("Show");
						} else if (collisionName == "40Point-Collider") {
								DataManager.CurrentScore += 40;//Add 40 points
								plusScoreEffectSpriteRenderer.sprite = plusScore [3];
						} else if (collisionName == "30Point-Collider") {
								DataManager.CurrentScore += 30;//Add 30 point
								plusScoreEffectSpriteRenderer.sprite = plusScore [2];
						} else if (collisionName == "20Point-Collider") {
								DataManager.CurrentScore += 20;//Add 20 points
								plusScoreEffectSpriteRenderer.sprite = plusScore [1];
						} else if (collisionName == "10Point-Collider") {
								DataManager.CurrentScore += 10;//Add 10 points
								plusScoreEffectSpriteRenderer.sprite = plusScore [0];
						}

						//Show the plus score effect
						plusScoreEffectAnimator.SetTrigger ("Show");

						//Play the arrow impact sound effect
						AudioClips.instance.PlayArrowImpactSFX();
			      
						//Fade out the arrow's sprite per time
						GetComponentInParent<Animator> ().SetTrigger ("Hide");

			           //Check the number of arrows
					//	GameManager.instance.CheckArrowsNumber();
						if(gameManager != null)
						{
							gameManager.CheckArrowsNumber ();
						}
				}
		}
}