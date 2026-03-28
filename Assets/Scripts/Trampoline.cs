using System;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public int count = 1;
    public float addtionalPower;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var rigid = other.attachedRigidbody;
        if (rigid && rigid.name == "Friend"&&rigid.bodyType==RigidbodyType2D.Dynamic)
        {
            if (Vector2.Dot(rigid.linearVelocity.normalized, transform.up) > 0) return;
            var dot = Mathf.Abs(Vector2.Dot(transform.up, -rigid.linearVelocity.normalized));
            var reflectForce = 2 * dot * (Vector2)transform.up + rigid.linearVelocity.normalized;
            Debug.DrawLine(rigid.position,rigid.position + rigid.linearVelocity,Color.red,10f);
            var force = -rigid.linearVelocity + reflectForce * rigid.linearVelocity.magnitude +
                        (Vector2)transform.up * addtionalPower;
            rigid.GetComponent<Friend>().forceReceived += force.magnitude;
            rigid.AddForce( force, ForceMode2D.Impulse);
            count--;
             if (count <= 0)
             {
                 Destroy(gameObject);
             }
        }
    }
}
