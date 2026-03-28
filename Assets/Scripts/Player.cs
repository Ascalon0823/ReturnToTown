using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 moveDir;

    public float moveSpeed;

    public Rigidbody2D body2D;

    public Friend friend;

    public float yeetPower;

    public float faceDir;
    public Camera mainCam;
    public Camera Focus;
    public Vector2 cursorPos;
    public Vector2 cursorWorldPos;
    public bool onUI;
    public Tool tool;

    public Transform playerStart;

    public Transform friendStart;

    public List<Placeable> placed = new ();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Focus.enabled = false;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        onUI = EventSystem.current.IsPointerOverGameObject();
        cursorWorldPos = mainCam.ScreenToWorldPoint(cursorPos);
    }

    private void FixedUpdate()
    {
        moveDir.y = 0;
        if (moveDir.x != 0)
        {
            faceDir = Mathf.Sign(moveDir.x);
        }

        body2D.MovePosition(body2D.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>().normalized;
    }

    public void OnJump(InputValue value)
    {
        if (!value.isPressed)
        {
            return;
        }

        if (friend && friend.transform.parent == transform)
        {
            friend.rb2d.bodyType = RigidbodyType2D.Dynamic;
            friend.transform.parent = null;
            var yeetDir = new Vector2(faceDir, 1f);

            var yeetForce = yeetDir * yeetPower;

            friend.begin = true;
            friend.rb2d.AddForce(yeetForce, ForceMode2D.Impulse);
            friend.rb2d.AddTorque(-yeetForce.magnitude);
            friend.forceReceived += yeetForce.magnitude;
            Focus.enabled = true;
            return;
        }

        if (friend)
        {
            friend.rb2d.bodyType = RigidbodyType2D.Kinematic;
            friend.rb2d.transform.parent = transform;
            friend.rb2d.transform.localPosition = Vector3.up;
            friend.rb2d.transform.localRotation = Quaternion.identity;
            friend.begin = false;
        }
    }

    public void OnPoint(InputValue value)
    {
        cursorPos = value.Get<Vector2>();
    }

    public void OnAttack(InputValue value)
    {
        if (onUI)
        {
            return;
        }

        tool.Use(cursorWorldPos);
    }

    public void Reset()
    {
        transform.position = playerStart.position;
        transform.rotation = playerStart.rotation;
        if (friend )
        {
            friend.rb2d.bodyType = RigidbodyType2D.Dynamic;
            friend.rb2d.transform.parent = null;
            friend.rb2d.position = friendStart.position;
            friend.rb2d.rotation = friendStart.rotation.eulerAngles.z;
            friend.rb2d.linearVelocity = Vector2.zero;
            friend.rb2d.angularVelocity = 0;
            friend.score = 0;
            friend.flyingDist = 0;
            friend.forceReceived = 0;
            friend.impulse = 0;
            friend.begin = false;
            
        }

        foreach (var item in placed)
        {
            item.Reset();
        }
    }
}