using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector3 target;
	public float delay;
    public string type;

    public void Fire()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        //transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }
}
