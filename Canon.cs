using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    //public Dictionary<string, Queue<GameObject>> gameObjects;
    public GameObject[] prefabArray;
    public GameObject[] target;
    public float projectileSpeed = 0.2f;
    public float intervalle = 1f;
    private GameObject projectilePrefab;
    private MeshRenderer meshRenderer;
	List<(GameObject, Vector3, Vector3, float, float)> setup;
    private Vector3 current_position;
    public int poolSize;

    public bool started = false;
    public float difficultyFactor = 1.0f;
    // Start is called before the first frame update


    // (Projectile type, Position de départ, Position cible, vitesse, delay avant prochaine instance)



    GameObject BuildProjectile((GameObject, Vector3, Vector3, float, float) t, float difactor)
	{
        //GameObject projectile = Instantiate(t.Item1, t.Item2, Quaternion.identity) ;
        string type = (t.Item1.GetComponent("Projectile") as Projectile).type;
        GameObject projectile = ObjectPoolManager.GetObjectFromPool(type, transform.position, Quaternion.identity);
		projectile.GetComponent<MeshRenderer>().enabled = true;
		projectile.GetComponent<BoxCollider>().enabled = true;
		projectile.GetComponent<AudioSource>().enabled = true;
		//projectile.transform.position = transform.position;
		(projectile.GetComponent("Projectile") as Projectile).target = t.Item3;
		(projectile.GetComponent("Projectile") as Projectile).speed = t.Item4 * difactor;
		(projectile.GetComponent("Projectile") as Projectile).delay = t.Item5;
		projectile.SetActive(true);
		return projectile;
	}

    Vector3 t(int targetId)
    {
        if (targetId <= target.Length)
            return target[targetId - 1].transform.position;
        return target[0].transform.position;
    }

    GameObject p(int projectileId)
    {
        if (projectileId <= prefabArray.Length)
            return prefabArray[projectileId - 1];
        return prefabArray[0];
    }

    void Start()
    {
        setup = new List<(GameObject, Vector3, Vector3, float, float)>
            {
                // (Id projectile, Position de départ, Position cible, vitesse, delay avant prochaine instance)
               
               
                
                (p(1), transform.position, t(1), 0.1f, 5f),
                (p(1), transform.position, t(2), 0.1f, 3f),
                (p(1), transform.position, t(3), 0.1f, 10f),

                (p(2), transform.position, t(1), 0.1f, 3f),
                (p(2), transform.position, t(2), 0.1f, 3f),
                (p(2), transform.position, t(3), 0.1f, 15f),

                (p(1), transform.position, t(1), 0.2f, 3f),
                (p(1), transform.position, t(2), 0.2f, 3f),
                (p(1), transform.position, t(3), 0.2f, 2f),

                (p(1), transform.position, t(1), 0.2f, 0f),
                (p(1), transform.position, t(3), 0.2f, 2f),

                (p(1), transform.position, t(1), 0.2f, 0f),
                (p(1), transform.position, t(3), 0.2f, 1f),
                (p(2), transform.position, t(2), 0.2f, 4f),

                (p(2), transform.position, t(1), 0.2f, 0f),
                (p(2), transform.position, t(3), 0.2f, 1f),
                (p(1), transform.position, t(2), 0.2f, 2f),

                (p(1), transform.position, t(1), 0.2f, 0f),
                (p(2), transform.position, t(2), 0.2f, 0f),
                (p(1), transform.position, t(3), 0.2f, 2f),
                (p(2), transform.position, t(1), 0.2f, 0f),
                (p(1), transform.position, t(2), 0.2f, 0f),
                (p(1), transform.position, t(3), 0.2f, 2f),

                (p(1), transform.position, t(1), 0.2f, 0f),
                (p(1), transform.position, t(2), 0.2f, 0f),
                (p(2), transform.position, t(3), 0.2f, 2f),

                (p(1), transform.position, t(1), 0.2f, 1.5f),
                (p(2), transform.position, t(2), 0.25f, 0.6f),
                (p(2), transform.position, t(3), 0.295f, 2f),

                (p(1), transform.position, t(1), 0.20f, 1.5f),
                (p(2), transform.position, t(2), 0.28f, 1.5f),
                (p(1), transform.position, t(1), 0.20f, 1.5f),
                (p(1), transform.position, t(3), 0.25f, 1.5f),
                (p(1), transform.position, t(1), 0.20f, 1.5f),
                (p(2), transform.position, t(3), 0.20f, 1.5f),
                (p(1), transform.position, t(2), 0.20f, 1.5f),
                (p(1), transform.position, t(2), 0.20f, 1.5f),
                (p(1), transform.position, t(3), 0.25f, 1.5f),
                (p(2), transform.position, t(2), 0.20f, 1.5f),
                (p(1), transform.position, t(1), 0.21f, 1.5f),
                (p(2), transform.position, t(3), 0.20f, 1.5f),
                (p(1), transform.position, t(1), 0.23f, 1.5f),
                (p(1), transform.position, t(2), 0.20f, 1.5f),
                (p(2), transform.position, t(1), 0.23f, 1.5f),
                (p(1), transform.position, t(2), 0.20f, 3f),

                (p(1), transform.position, t(1), 0.23f, 0f),
                (p(2), transform.position, t(2), 0.23f, 0f),
                (p(1), transform.position, t(3), 0.23f, 2f),

                (p(2), transform.position, t(1), 0.23f, 0f),
                (p(1), transform.position, t(2), 0.23f, 0f),
                (p(1), transform.position, t(3), 0.23f, 2f),

                (p(1), transform.position, t(1), 0.23f, 0f),
                (p(1), transform.position, t(2), 0.23f, 0f),
                (p(2), transform.position, t(3), 0.23f, 2f),

                (p(2), transform.position, t(3), 0.3f, 2f),
                (p(2), transform.position, t(3), 0.3f, 2f),
                (p(2), transform.position, t(3), 0.3f, 2f),

                (p(2), transform.position, t(2), 0.3f, 2f),
                (p(2), transform.position, t(2), 0.3f, 2f),
                (p(2), transform.position, t(2), 0.3f, 2f),
                (p(2), transform.position, t(2), 0.3f, 2f),
                (p(2), transform.position, t(2), 0.3f, 2f),
                (p(2), transform.position, t(2), 0.3f, 2f),

                (p(1), transform.position, t(1), 0.2f, 2f),
                (p(1), transform.position, t(2), 0.2f, 2f),
                (p(1), transform.position, t(3), 0.2f, 2f),

                (p(1), transform.position, t(1), 0.2f, 0f),
                (p(1), transform.position, t(3), 0.2f, 2f),

                /*(p(1), transform.position, t(1), 0.2f, 0f),
                (p(1), transform.position, t(3), 0.2f, 1f),
                (p(2), transform.position, t(2), 0.2f, 4f),

                (p(2), transform.position, t(1), 0.2f, 0f),
                (p(2), transform.position, t(3), 0.2f, 1f),
                (p(1), transform.position, t(2), 0.2f, 2f),

                (p(1), transform.position, t(1), 0.2f, 0f),
                (p(2), transform.position, t(2), 0.2f, 0f),
                (p(1), transform.position, t(3), 0.2f, 2f),
                (p(2), transform.position, t(1), 0.2f, 0f),
                (p(1), transform.position, t(2), 0.2f, 0f),
                (p(1), transform.position, t(3), 0.2f, 2f),

                (p(1), transform.position, t(1), 0.2f, 0f),
                (p(1), transform.position, t(2), 0.2f, 0f),
                (p(2), transform.position, t(3), 0.2f, 2f),

                (p(1), transform.position, t(1), 0.2f, 1.5f),
                (p(2), transform.position, t(2), 0.25f, 0.6f),
                (p(2), transform.position, t(3), 0.295f, 2f),

                (p(1), transform.position, t(1), 0.20f, 1.5f),
                (p(2), transform.position, t(2), 0.28f, 1.5f),
                (p(1), transform.position, t(1), 0.20f, 1.5f),
                (p(1), transform.position, t(3), 0.25f, 1.5f),
                (p(1), transform.position, t(1), 0.20f, 1.5f),
                (p(2), transform.position, t(3), 0.20f, 1.5f),
                (p(1), transform.position, t(2), 0.20f, 1.5f),
                (p(1), transform.position, t(2), 0.20f, 1.5f),
                (p(1), transform.position, t(3), 0.25f, 1.5f),
                (p(2), transform.position, t(2), 0.20f, 1.5f),
                (p(1), transform.position, t(1), 0.21f, 1.5f),
                (p(2), transform.position, t(3), 0.20f, 1.5f),
                (p(1), transform.position, t(1), 0.23f, 1.5f),
                (p(1), transform.position, t(2), 0.20f, 1.5f),
                (p(2), transform.position, t(1), 0.23f, 1.5f),
                (p(1), transform.position, t(2), 0.20f, 3f),

                (p(1), transform.position, t(1), 0.23f, 0f),
                (p(2), transform.position, t(2), 0.23f, 0f),
                (p(1), transform.position, t(3), 0.23f, 2f),

                (p(2), transform.position, t(1), 0.23f, 0f),
                (p(1), transform.position, t(2), 0.23f, 0f),
                (p(1), transform.position, t(3), 0.23f, 2f),

                (p(1), transform.position, t(1), 0.23f, 0f),
                (p(1), transform.position, t(2), 0.23f, 0f),
                (p(2), transform.position, t(3), 0.23f, 2f),

                (p(2), transform.position, t(3), 0.3f, 2f),
                (p(2), transform.position, t(3), 0.3f, 2f),
                (p(2), transform.position, t(3), 0.3f, 2f),

                (p(2), transform.position, t(2), 0.3f, 2f),
                (p(2), transform.position, t(2), 0.3f, 2f),
                (p(2), transform.position, t(2), 0.3f, 2f),
                (p(2), transform.position, t(2), 0.3f, 2f),
                (p(2), transform.position, t(2), 0.3f, 2f),
                (p(2), transform.position, t(2), 0.3f, 2f),*/



            };

        foreach (GameObject prefab in prefabArray)
        {
            prefab.SetActive(false);
        }

        float time = 0f;
        foreach ((GameObject, Vector3, Vector3, float, float) t in setup)
        {
            time += t.Item5;
        }
        //Debug.Log("Level time = " + time);
        //startFire();
    }
    public void startFire()
    {
        if(started == false)
        {
            
            current_position = transform.position;
            
            started = true;
            StartCoroutine("Fire");
        }
        
    }

	
    public void stopFire()
    {
        StopCoroutine("Fire");
		started = false;

	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stopFire();
            //Debug.Log("Stop fire");
        }
    }


    IEnumerator Fire()
    {
        
        yield return new WaitForSeconds(2f);
        int i = 0;
        while (i<setup.Count)
        {
			//GameObject projectile = gameObjects.Dequeue();
			Debug.Log(i + 1);
            GameObject projectile = BuildProjectile(setup[i], difficultyFactor);
            float delay = (projectile.GetComponent("Projectile") as Projectile).delay;
            projectile.GetComponent<Fade>().FadeObjectFunction();
			i++;
            //MakeProjectile();
            yield return new WaitForSeconds(delay);
        }
    }
}
