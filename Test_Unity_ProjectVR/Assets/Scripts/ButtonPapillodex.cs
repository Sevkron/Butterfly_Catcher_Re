﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonPapillodex : MonoBehaviour
    {
        //private Canvas m_PapillodexCanvas;
        private Animator papillodexAnimator;
        public GameObject m_Papillodex;
        private bool CanvasOpen = false;
        private bool isPressed;
        private AudioSource audioSource;
        private AudioManager audioManager;
        public Hand leftHand;
        public Hand rightHand;
        //IEnumerator ClosingSequence;

        void Awake()
        {
            if(leftHand == null || rightHand == null)
            {
                //Debug.Log("Setup Hands");
                Debug.LogError("Setup Hands");
            }
            if  (audioManager == null)
            {
                audioManager = FindObjectOfType<AudioManager>();
            }

            if(m_Papillodex == null)
            {
                Debug.LogError("Setup Canvas Papillodex");
            }
            papillodexAnimator = m_Papillodex.GetComponent<Animator>();

            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            
        }

        public void OnButtonDown(Hand fromHand)
        {
            fromHand.TriggerHapticPulse(1000);
            fromHand.otherHand.useControllerHoverComponent = false;
            CanvasOpen = !CanvasOpen;
            isPressed = true;
            //AQDebug.Log("Pressed");

            if(CanvasOpen == false && isPressed == true)
            {
                m_Papillodex.SetActive(true);
                audioManager.Play("ButtonPapillodex", audioSource);
                audioManager.Play("OpenCanvas", null);
                leftHand.useHoverCapsule = false;
                rightHand.useHoverCapsule = false;
                //leftHand.GetComponent<GrabDistance>().GrabUpdate(false, 0);
                //rightHand.GetComponent<GrabDistance>().GrabUpdate(false, 0);
                leftHand.GetComponent<GrabDistance>().enabled = false;
                rightHand.GetComponent<GrabDistance>().enabled = false;
                isPressed = false;
            }
            else if (CanvasOpen == true && isPressed == true)
            {
                audioManager.Play("ButtonPapillodex", audioSource);
                audioManager.Play("CloseCanvas", null);
                papillodexAnimator.SetTrigger("Close");
                //StartCoroutine(WaitForAnimation(5));
                //m_Papillodex.SetActive(false);
                leftHand.useHoverCapsule = true;
                rightHand.useHoverCapsule = true;
                //leftHand.GetComponent<GrabDistance>().enabled = true;
                //rightHand.GetComponent<GrabDistance>().enabled = true;
                Debug.Log("Closing Papillodex");
                isPressed = false;
            }
        }

        

        IEnumerator WaitForAnimation(float animationTime)
        {
            
            //animationTime = papillodexAnimator.GetCurrentAnimatorStateInfo(0).IsName;
            yield return new WaitForSeconds(animationTime);
        }
    }
}