using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;

    void Start()
    {
        moveSpeed = Random.Range(3.0f, 3.5f);
    }

    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

        if (MissionManager.Instance)
        {
            if (MissionManager.Instance.PanelsActivated)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
         if (collider.gameObject.tag == "Arrow")
        {
            if (MissionManager.Instance)
            {
                MissionManager.Instance.SmashedBallons = + 1;
                MissionManager.Instance.UpdateBalloonsCounter();
            }
            Destroy(collider.gameObject);  
            AudioManager.Instance.PlayBalloonPopupSoundWEffect();  
            Destroy(gameObject);  
        }
    }
}

