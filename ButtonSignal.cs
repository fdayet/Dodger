using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSignal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
    }
    
    public void enableButton()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
    }
}
