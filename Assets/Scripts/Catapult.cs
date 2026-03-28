using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Catapult : MonoBehaviour
    {
        public float torque;
        public Transform loadHandle;
        public bool shot;
        public void Trigger()
        {
            Debug.Log("Shoot");
            GetComponent<Rigidbody2D>().AddTorque(torque);
            shot = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (shot) return;
            var body  = other.collider.GetComponent<Rigidbody2D>();
            if (body&& body.name == "Friend"&&body.bodyType==RigidbodyType2D.Dynamic)
            {
                body.transform.position = loadHandle.position;
                body.transform.rotation = loadHandle.rotation;
                body.linearVelocity = Vector2.zero;
                Invoke(nameof(Trigger), 3f);
            }
        }
    }
}