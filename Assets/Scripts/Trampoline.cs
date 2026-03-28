using System;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public int count = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var rigid =  other.GetComponent<Rigidbody2D>();
        if (rigid && rigid.name == "Friend"&&rigid.bodyType==RigidbodyType2D.Dynamic)
        {
            var dot = Mathf.Abs(Vector2.Dot(transform.up, -rigid.linearVelocity.normalized));
            var reflectForce = 2 * dot * (Vector2)transform.up + rigid.linearVelocity.normalized;
            rigid.AddForce( -rigid.linearVelocity + reflectForce * rigid.linearVelocity.magnitude + (Vector2)transform.up * 10f  , ForceMode2D.Impulse);
            count--;
             if (count <= 0)
             {
                 Destroy(gameObject);
             }
        }
    }
}
