using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserFirepointScript : MonoBehaviour
{

    public GameObject LaserFirepoint;


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

        yield return new WaitForSeconds(1/2);
        LaserFirepoint.SetActive(false);

    }
}
