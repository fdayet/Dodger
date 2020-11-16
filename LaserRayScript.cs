using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LaserRayScript : MonoBehaviour
{
    public GameObject LaserRay;


    public int TimeUntilLaserend;

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
        yield return new WaitForSeconds(TimeUntilLaserend);
        LaserRay.SetActive(false);
		//trailQueue.Enqueue((LaserRay));
	}



}
