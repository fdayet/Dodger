using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjectBehaviour : MonoBehaviour
{
    // Holds the info about its object pool
    public ObjectPool Pool { get; set; }

    Rigidbody rb;

    // Provides funtionaoity to the instantiated objects
    private void OnEnable()
    {
        //ApplyForce();
        //StartCoroutine(DestroyPooledObject());
    }

    IEnumerator DestroyPooledObject()
    {
        yield return new WaitForSeconds(2f);
        // returns the object to its pool instead of destroy
        ObjectPoolManager.ReturnObjectToPool(Pool.Name, this.gameObject);
    }

    // Apply small force in upward direction 
    public void ApplyForce()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.AddForce(transform.up * 200.0f);
        rb.useGravity = true;
    }
}