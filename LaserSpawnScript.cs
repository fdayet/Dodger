using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;


public class LaserSpawnScript : MonoBehaviour
{
    public GameObject LaserRayPrefab;
    public GameObject FirepointPrefab;
    public GameObject FirePoint;
	public GameObject LaserParent;
	public Vector3 posfirepoint;
    public AudioSource laserNoisePlayer;
    public AudioClip laserNoise;

	[SerializeField]
    private int speed = 20;
    private GameObject[] LaserArray = new GameObject[50];
    private GameObject spawnedLaserFirepoint;
    private GameObject spawnedLaserRay;
    private GameObject spawnedLaserRayChild;
    private GameObject spawnedLaserRayChild2;
    private int i;
    private int k;

    public float TimeUntilFirepointEnd;

    Rigidbody LaserRB;

    public SteamVR_Action_Boolean ShootLaserRight;
    public SteamVR_Action_Boolean ShootLaserLeft;
    private SteamVR_Behaviour_Pose behaviourPose;
    public SteamVR_Input_Sources inputSource;
    void Start()
    {
       // //Debug.Log(test.ToString()=="LeftHand");

        spawnedLaserFirepoint = Instantiate(FirepointPrefab, FirePoint.transform) as GameObject;
        DisabledLaser();
        behaviourPose = this.GetComponent<SteamVR_Behaviour_Pose>();
        //inputSource = behaviourPose.inputSource;
    }


    void HandAttachedUpdate(Hand hand)
    {
   

        if (ShootLaserRight.GetStateDown(inputSource) && hand.handType.ToString()=="RightHand")
        {
            EnabledLaser();
            StartCoroutine(DesactivateFirepoint());
            laserNoisePlayer.PlayOneShot(laserNoise, 0.7f);
        }

        if (ShootLaserLeft.GetStateDown(inputSource) && hand.handType.ToString() == "LeftHand")
        {
            EnabledLaser();
            StartCoroutine(DesactivateFirepoint());
            laserNoisePlayer.PlayOneShot(laserNoise, 0.7f);
        }

        //if (ShootLaserLeft.GetStateUp(inputSource))
        //{
            //spawnedLaserFirepoint.SetActive(false);
       // }

    }

	public void OnHandDettached()
	{
		//transform.
	}

    IEnumerator DesactivateFirepoint()
    {
        yield return new WaitForSeconds(TimeUntilFirepointEnd);
        spawnedLaserFirepoint.SetActive(false);
        //trailQueue.Enqueue((LaserRay));

    }


    void EnabledLaser()
    {
        i = 0;
        k = 0;

        while (k == 0)
        {
            if ((LaserArray[i] != null) && (LaserArray[i].activeInHierarchy == false))
            {
                spawnedLaserRay = LaserArray[i];
                spawnedLaserRay.transform.position = FirePoint.transform.position;
                spawnedLaserRay.transform.rotation = FirePoint.transform.rotation;
                spawnedLaserRay.SetActive(true);
                LaserRB = spawnedLaserRay.GetComponent<Rigidbody>();
                LaserRB.isKinematic = false;
                LaserRB.velocity = FirePoint.transform.forward * speed;
                spawnedLaserRayChild = spawnedLaserRay.transform.GetChild(0).gameObject;
                spawnedLaserRayChild.SetActive(true);
                spawnedLaserRayChild2 = spawnedLaserRay.transform.GetChild(1).gameObject;
                spawnedLaserRayChild2.SetActive(false);
                k = 1;
            }
            else if (LaserArray[i] == null)
            {
				Vector3 pos = FirePoint.transform.position;
				Quaternion rot = FirePoint.transform.rotation;
                spawnedLaserRay = Instantiate(LaserRayPrefab, pos, rot, LaserParent.transform) as GameObject;
                LaserArray[i] = spawnedLaserRay;
                spawnedLaserRay.transform.position = FirePoint.transform.position;
                spawnedLaserRay.SetActive(true);
                LaserRB = spawnedLaserRay.GetComponent<Rigidbody>();
                LaserRB.velocity = FirePoint.transform.forward * speed;
                k = 1;
            }
            else 
            {
                i += 1;
            }
        }


		if (FirePoint != null)
		{
			spawnedLaserFirepoint.transform.position = FirePoint.transform.position;
			spawnedLaserFirepoint.transform.localPosition += posfirepoint;

		}

		spawnedLaserFirepoint.SetActive(true);

    }


    void DisabledLaser()
    {
        spawnedLaserFirepoint.SetActive(false);
		if (spawnedLaserRay != null)
		{
			spawnedLaserRay.SetActive(false);
		}

    }

    //tag DestructibleObject pour les objets à détruire
    //tag Desert pour le désert
    //layer DestructibleObject pour les objets à détruire
    //layer Laser pour pistolet, firepoint, laser
    //physic 2D layer DestructibleObject et Laser n'interagissent pas, décoché
    //physic layer laser n'intéragit pas avec layer laser, décoché
}