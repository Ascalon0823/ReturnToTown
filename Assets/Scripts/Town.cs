using System;
using UnityEngine;

public class Town : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var body = other.GetComponent<Rigidbody2D>();
        if (body && body.name == "Friend")
        {
            Debug.Log(body.name);
        }
    }
}
