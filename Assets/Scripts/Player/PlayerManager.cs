﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IListener {
    
    Ray ray;
    RaycastHit hit;

    float h, v;
    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;

    Animator anim;

    public GameObject Bullet;
    private Transform gun;
    

    private float verticalVelocity;
    private float gravity = 14.0f;
    private float jumpForce = 10.0f;

    private Vector3 moveDirection = Vector3.zero;

    CharacterController controller;
    // Use this for initialization
    void Start () {
        anim = GetComponentInChildren<Animator>();
        gun = GameObject.Find("attackArea").transform; // 총알 나가는 위치
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_PLAYER_DAMAGED, this);
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_PLAYER_ATTACK, this);
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_PLAYER_JUMP, this);

        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", (h * h + v * v));
        
        //Vector3 speed = rigidbody.velocity;
        
        if(controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveVector = Vector3.zero;

        moveVector.x = v * 5.0f;
        moveVector.y = verticalVelocity;
        moveVector.z = h * 5.0f;
        controller.Move(moveVector * Time.deltaTime);
        //if (h != 0f && v != 0f)
        //{
        //    transform.Translate(Vector3.forward * moveSpeed * v * Time.deltaTime, Space.Self);
        //    transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed * h);
        //}
    }

    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch(Event_Type)
        {
            case EVENT_TYPE.GAME_PLAYER_DAMAGED:
                break;
            case EVENT_TYPE.GAME_PLAYER_ATTACK:
                Instantiate(Bullet, gun.position, gun.rotation);
                break;
            case EVENT_TYPE.GAME_PLAYER_JUMP:
                Debug.Log("jump");
                verticalVelocity = jumpForce;
                break;
        }
    }

    public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = -stickPos.y;
    }
}
