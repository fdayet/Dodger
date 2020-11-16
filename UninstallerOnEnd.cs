using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UninstallerOnEnd : MonoBehaviour
{

    public ParticleSystem DestructionEffect;
	public GameObject canon;
    private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }



    private void OnTriggerEnter(Collider other)
    {
       
       if(!canon.GetComponent<Canon>().started)
		{
			//Debug.Log(other.gameObject.layer.ToString());
			//Check for a match with the specific tag on any GameObject that collides with your GameObject
			if (other.gameObject.tag == "Respawn" || other.gameObject.tag == "RespawnLaser")
			{
				//If the GameObject has the same tag as specified, output this message in the console
				/*ParticleSystem explosionEffect = Instantiate(DestructionEffect)
											 as ParticleSystem;
				explosionEffect.transform.position = other.gameObject.transform.position;
				//play it
				explosionEffect.loop = false;
				explosionEffect.Play();
				Destroy(explosionEffect.gameObject, explosionEffect.duration);*/
				other.gameObject.SetActive(false);
				////Debug.Log(canon.gameObjects.Count);
				string type = (other.gameObject.GetComponent("Projectile") as Projectile).type;

				//Debug.Log("Destroy "+ type);
				//canon.gameObjects[(other.gameObject.GetComponent("Projectile") as Projectile).type].Enqueue(other.gameObject);
				ObjectPoolManager.ReturnObjectToPool(type, other.gameObject);
				//Destroy(other.gameObject);
				////Debug.Log(canon.gameObjects.Count);
				// ObjectPoolManager.ReturnObjectToPool("Empty", other.gameObject);
			}
		}
       
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
