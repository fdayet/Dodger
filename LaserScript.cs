using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public GameObject LaserPrefab;
    public GameObject FirePoint;

    private GameObject spawnedLaser;


    void Start()
    {

        spawnedLaser = Instantiate(LaserPrefab, FirePoint.transform) as GameObject;
        DisabledLaser();

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnabledLaser();
        }

        if (Input.GetMouseButton(0))
        {
            UpdateLaser();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DisabledLaser();
        }

    }

    void EnabledLaser()
    {
        spawnedLaser.SetActive(true);
    }

    void UpdateLaser()
    {
        if (FirePoint != null)
        {
            spawnedLaser.transform.position = FirePoint.transform.position;
        }
    }

    void DisabledLaser()
    {
        spawnedLaser.SetActive(false);
    }

}