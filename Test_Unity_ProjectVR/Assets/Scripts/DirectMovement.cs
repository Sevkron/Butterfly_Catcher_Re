﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DirectMovement : MonoBehaviour
{
    public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

    public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean switchMoveTypeAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("SwitchMovementModes");

    private Hand pointerHand = null;
    private Player player;
    public GameObject teleportGO;

    public AudioSource directMoveSource;
    public AudioClip directMoveSound;

    private CharacterController characterController;
    public GameObject m_camera;
    public float speed = 1;
    private float forwardAxis;
    void Start()
    {
        //player = InteractionSystem.Player.instance;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = input.axis.y;
        forwardAxis = input.axis.y;
        //backwards move
        /*if(input.axis.y < 0)
        {
            forwardAxis = 0;
        }*/

        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(0, 0, forwardAxis));
        characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        characterController.center = new Vector3(Player.instance.hmdTransform.localPosition.x, characterController.center.y, Player.instance.hmdTransform.localPosition.z);
    }
}

