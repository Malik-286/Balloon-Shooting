using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public static Grenade Instance;

    [SerializeField] float moveSpeed = 4f;
    float Radius = 10f;
    [SerializeField] int MaxKills =0;

    [SerializeField] GameObject explosionParticleEffect;


    private void Awake()
    {
        if (Instance == null)
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
        CreateExplosionParticleEffect();
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayGernadeSoundEffect();
        }
        Collider2D[] CollidersinRadius = Physics2D.OverlapCircleAll(transform.position, Radius, LayerMask.NameToLayer("Gameplay"));

  
        foreach (Collider2D NearbyObjects in CollidersinRadius)
        {
            Rigidbody2D rb = NearbyObjects.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (MaxKills <= 4)
                {
                    MaxKills++;
                    rb.AddForce(transform.position * 2f);
                    rb.GetComponent<SpriteRenderer>().enabled = false;

                    if (rb.GetComponent<Balloon>())
                    {
                        rb.GetComponent<Balloon>().Particle.SetActive(true);
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

    void CreateExplosionParticleEffect()
    {
        GameObject particlesClone = Instantiate(explosionParticleEffect, transform.position, Quaternion.identity);
        Destroy(particlesClone, 0.2f);
    }

 
}
