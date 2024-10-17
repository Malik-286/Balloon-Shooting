using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public static Balloon Instance;
    float Radius = 10f;

    [SerializeField] float moveSpeed = 4f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        moveSpeed = Random.Range(3.0f, 3.5f);
    }

    public void GrenadeHit()
    {

        Collider2D[] CollidersinRadius = Physics2D.OverlapCircleAll(transform.position, Radius, LayerMask.NameToLayer("Gameplay"));

        foreach (Collider2D NearbyObjects in CollidersinRadius)
        {
            Rigidbody2D rb = NearbyObjects.GetComponent<Rigidbody2D>();
            if (rb != null)
            {

                rb.AddForce(transform.position * 2f);
                rb.GetComponent<SpriteRenderer>().enabled = false;
                if (rb.GetComponent<Transform>().GetChild(0))
                {
                rb.GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
                }
                if (AudioManager.Instance)
                {
                    AudioManager.Instance.PlayBalloonPopupSoundWEffect();
                }
                if (MissionManager.Instance)
                {
                    MissionManager.Instance.SmashedBallons += 1;
                    MissionManager.Instance.UpdateBalloonsCounter();
                }
                Destroy(rb, 2f);
            }
        }
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
}

