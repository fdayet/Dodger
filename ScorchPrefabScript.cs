using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScorchPrefabScript : MonoBehaviour
{
    public GameObject ScorchPrefab;

    [SerializeField]
    private int TimeUntilScorchend=4;

    void Start()
    {
    }
    void Update()
    {

        StopCoroutine(Shoot());
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(TimeUntilScorchend);
        ScorchPrefab.SetActive(false);
        //trailQueue.Enqueue((LaserRay));

    }



}
