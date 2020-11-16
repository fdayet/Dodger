using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserImpactScript : MonoBehaviour

{
    public GameObject GlobalLaserRay;
    public GameObject ImpactParticule;
    public GameObject LaserRay;
    public Material newMaterialRef;
    public GameObject ScorchPrefab;
    public AudioClip destroyBlock;

	private GameObject SpawnScorch;
    private GameObject SpawnScorchChild;
    private GameObject[] ScorchArray = new GameObject[50];
    private GameObject CollisionObject;
    //Renderer rend;
    Rigidbody RBlaser;
    private int i;
    private int k;

    void Start()
    {
        ImpactParticule.SetActive(false);

    }

    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.tag == "RespawnLaser")
        {
            //Debug.Log("avant");
            RBlaser = GlobalLaserRay.GetComponent<Rigidbody>();
            RBlaser.isKinematic = true;
            CollisionObject = col.gameObject;
            ImpactParticule.SetActive(true);
            LaserRay.SetActive(false);
            CollisionObject.GetComponent<AudioSource>().PlayOneShot(destroyBlock);
            CollisionObject.GetComponent<MeshRenderer>().enabled = false;
            CollisionObject.GetComponent<BoxCollider>().enabled = false;
			CollisionObject.GetComponent<AudioSource>().enabled = false;
			StartCoroutine(LateDestroy(CollisionObject));
        }
        else if (col.gameObject.tag == "Desert")
        {
            //Instantiate(ScorchPrefab, t.contacts[0].point, Quaternion.identity);
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.tag == "Desert")
        {
            i = 0;
            k = 0;
			
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);

            while (k == 0)
            {
                if ((ScorchArray[i] != null) && (ScorchArray[i].activeInHierarchy == false))
                {
                    //Debug.Log("on reutilise");
                    SpawnScorch = ScorchArray[i];
                    SpawnScorch.transform.position = pos;
                    SpawnScorch.transform.rotation = rot;
                    SpawnScorch.SetActive(true);
                    SpawnScorchChild = SpawnScorch.transform.GetChild(0).gameObject;
                    SpawnScorchChild.SetActive(true);
                    RBlaser = GlobalLaserRay.GetComponent<Rigidbody>();
                    RBlaser.isKinematic = true;
                    LaserRay.SetActive(false);
                    //Debug.Log("slt");
                    k = 1;
                }
                else if (ScorchArray[i] == null)
                {
                    //Debug.Log("on crée");
                    SpawnScorch =Instantiate(ScorchPrefab, pos, rot);
                    RBlaser = GlobalLaserRay.GetComponent<Rigidbody>();
                    RBlaser.isKinematic = true;
                    LaserRay.SetActive(false);
                    ScorchArray[i] = SpawnScorch;
                    k = 1;
                }
                else
                {
                    i += 1;
                }
            }
        }
    }

    


    IEnumerator LateDestroy(GameObject CollisionObject)
    {
        yield return new WaitForSeconds(2);
		string type = (CollisionObject.gameObject.GetComponent("Projectile") as Projectile).type;
		ObjectPoolManager.ReturnObjectToPool(type, CollisionObject.gameObject);
		//Destroy(CollisionObject.gameObject);
	}

    /*
    public void OnTriggerExit(Collider col)
    {
    
    }
    */
}
