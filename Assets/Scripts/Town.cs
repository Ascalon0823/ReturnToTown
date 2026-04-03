using System;
using UnityEngine;

public class Town : MonoBehaviour
{
    private bool _isIn;
    public bool isIn => _isIn && FindFirstObjectByType<Friend>().rb2d.bodyType == RigidbodyType2D.Dynamic;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var body = other.attachedRigidbody;
        if (body && body.name == "Friend")
        {
            _isIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var body = other.attachedRigidbody;
        if (body && body.name == "Friend")
        {
            _isIn = false;
        }
    }
}
