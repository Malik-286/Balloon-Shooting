using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonsPool : MonoBehaviour
{
    public static BalloonsPool instance;

    void Awake()
    {
        instance = this;
    }




    public int totalBallons = 50;

    [SerializeField] GameObject[] balloonsPrefabs;
    [SerializeField] Transform[] poolPositions;

    [SerializeField] float waitTime = 2.0f;

    void Start()
    {
        StartCoroutine(BalloonsPooling());
    }

    IEnumerator BalloonsPooling()
    {
        while (totalBallons >= 1) 
        {
            totalBallons--;
            GameObject balloonClone = Instantiate(balloonsPrefabs[Random.Range(0, balloonsPrefabs.Length)], poolPositions[Random.Range(0, poolPositions.Length)].position,Quaternion.identity);

            yield return new WaitForSeconds(waitTime);
            Destroy(balloonClone, 3.0f);
        }
    }
}

