using System;
using UnityEngine;

public class Friend : MonoBehaviour
{
    public float score = 0;
    public float flyingDist;
    public float impulse;
    public bool begin = false;
    public Vector2 lastPos;
    public Rigidbody2D rb2d;
    public float forceReceived;
    public Town town;
    public Collider2D c;
    public Collider2D ground;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (begin)
            impulse += other.contacts[0].normalImpulse;
    }

    public bool TouchOnGround()
    {
        return Physics2D.IsTouching(c, ground);
    }
    private void FixedUpdate()
    {
        if (!begin)
        {
            score = 0;
            flyingDist = 0;
            forceReceived = 0;
            impulse = 0;
            return;
        }
        Debug.DrawLine(rb2d.position,rb2d.position + rb2d.linearVelocity,Color.red,1f);
        flyingDist += (rb2d.position - lastPos).magnitude;
        lastPos = rb2d.position;
        score = flyingDist * Mathf.Max(1f, impulse) * Mathf.Max(1f, forceReceived);
        if (begin && rb2d.linearVelocity.magnitude < .01f && TouchOnGround() && town.isIn)
        {
            Debug.Log("Score: " + score);
        }
    }
}
