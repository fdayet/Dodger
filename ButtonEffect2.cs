//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonEffect2 : MonoBehaviour
    {
        public AudioClip sound;
        public void OnButtonDown(Hand fromHand)
        {
            GetComponent<AudioSource>().PlayOneShot(sound);
            fromHand.TriggerHapticPulse(1000);
        }

        public void OnButtonUp(Hand fromHand)
        {

        }

    }
}