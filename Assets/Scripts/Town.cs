using System;
using UnityEngine;

public class Town : MonoBehaviour
{
    public bool isIn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var body = other.GetComponent<Rigidbody2D>();
        if (body && body.name == "Friend"&&body.bodyType==RigidbodyType2D.Dynamic)
        {
            isIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var body = other.GetComponent<Rigidbody2D>();
        if (body && body.name == "Friend"&&body.bodyType==RigidbodyType2D.Dynamic)
        {
            isIn = false;
        }
    }
}
