using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public static Balloon Instance;
    public GameObject Particle;
    [SerializeField] float moveSpeed = 4f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        Particle = transform.GetChild(transform.childCount - 1).gameObject;
    }

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
}

