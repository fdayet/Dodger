using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class RayPickUp : MonoBehaviour
{
    public Hand hand;
    public float distance;
    public float laserWidth = 0.1f;

    private bool draw;

    public SteamVR_Action_Boolean instanciateAction;
    private SteamVR_Behaviour_Pose behaviourPose;
    public SteamVR_Input_Sources inputSource;


    private Interactable _hoveringInteractable;
    private LineRenderer laserLineRenderer;
    private GameObject currentObject;
    private void Start()
    {
        draw = false;
        hand = transform.parent.GetComponent<Hand>();
        laserLineRenderer = GetComponent<LineRenderer>();
        Vector3[] initLaserPositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        laserLineRenderer.SetPositions(initLaserPositions);
        laserLineRenderer.startWidth = laserWidth;
        laserLineRenderer.endWidth = laserWidth;
        GetComponent<LineRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (instanciateAction.GetStateDown(inputSource))
        {
			//Debug.Log("UpdateGood");
            draw = true;
            GetComponent<LineRenderer>().enabled = true;
        }
        if (instanciateAction.GetStateUp(inputSource))
        {
            draw = false;
            GetComponent<LineRenderer>().enabled = false;
        }
        if (draw)
        {
            RaycastHit Hit;
            Ray landingRay = new Ray(transform.position, transform.forward);
            Vector3 endPosition = transform.position + (distance * transform.forward);
            if (Physics.Raycast(landingRay, out Hit, distance))
            {
                //Debug.Log(Hit.transform.name);

                if (Hit.transform != currentObject)
                {
                    if (currentObject != null)
                    {
                        hand.HoverUnlock(currentObject.GetComponent<Interactable>());
                        currentObject = null;
                    }
                    if (Hit.transform.CompareTag("Holdable"))
                    {
                        //Debug.Log(Hit.transform.name);
                        hand.HoverLock(Hit.transform.GetComponent<Interactable>());
                        currentObject = Hit.transform.gameObject;
                    }
                }
            }
            else
            {
                if (currentObject != null)
                {
                    hand.HoverUnlock(currentObject.GetComponent<Interactable>());
                    currentObject = null;
                }
            }

            laserLineRenderer.SetPosition(0, transform.position);
            laserLineRenderer.SetPosition(1, endPosition);
        }
    }
}
