﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

public class ButterflyJar : MonoBehaviour
{
    public BehaviorTree butterflyBehaviorTree;



    //public GameObject jar;
    public bool hasButterfly;
    public GameObject ButterflyinJar;
    
    public float scale = 0.2f;
    private YMovement yMoveScript;
    public SharedBool SharedIsIdle;

    private void Start()
    {
        SharedIsIdle = (SharedBool)butterflyBehaviorTree.GetVariable("IsIdle");
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Butterfly") && hasButterfly == false)
        {
            ButterflyinJar = other.gameObject;
            hasButterfly = true;
            butterflyBehaviorTree = ButterflyinJar.GetComponentInParent<BehaviorTree>();
            SharedIsIdle = true;
            ButterflyinJar.GetComponentInParent<NavMeshAgent>().enabled = false;
            yMoveScript = ButterflyinJar.GetComponent<YMovement>();
            yMoveScript.GoToDefaultPos();
            ButterflyinJar.GetComponent<SphereCollider>().enabled = false;
            
            butterflyBehaviorTree.SendEvent<object>("IsCapturedJar", this.gameObject);
            ButterflyinJar.transform.parent.transform.localScale = new Vector3(scale, scale, scale);
            
        }
    }

    public void FreeButterfly()
    {
        StartCoroutine(yMoveScript.Delay());
        Debug.Log("Delay start");
    }

    public void StopCoroutine()
    {
        StopCoroutine(yMoveScript.Delay());
    }
}
