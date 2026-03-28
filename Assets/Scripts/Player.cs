using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public Vector2 moveDir;

    public float moveSpeed;

    public Rigidbody2D body2D;

    public Rigidbody2D holdingFriend;

    public float yeetPower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        moveDir.y = 0;
        body2D.MovePosition(body2D.position + moveDir*moveSpeed * Time.fixedDeltaTime);
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

        if (holdingFriend)
        {
            holdingFriend.bodyType = RigidbodyType2D.Dynamic;
            holdingFriend.transform.parent = null;
            var yeetDir = new Vector2(Mathf.Sign(moveDir.x == 0 ? -1 : moveDir.x), 1f);
            
            var yeetForce = yeetDir * yeetPower;
            
            var friend = holdingFriend.GetComponent<Friend>();
            friend.begin = true;
            holdingFriend.AddForce(yeetForce, ForceMode2D.Impulse);
            friend.forceReceived += yeetForce.magnitude;
            holdingFriend = null;
            return;
        }
        var friendCollider = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Friend"));
        if (!friendCollider )
        {
            return;
        }
        holdingFriend = friendCollider.attachedRigidbody;
        if (holdingFriend)
        {
            holdingFriend.bodyType = RigidbodyType2D.Kinematic;
            holdingFriend.transform.parent = transform;
            holdingFriend.transform.localPosition = Vector3.up;
            holdingFriend.transform.localRotation = Quaternion.identity;
            var friend = holdingFriend.GetComponent<Friend>();
            friend.begin = false;
        }
    }
}
