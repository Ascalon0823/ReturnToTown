using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Catapult : MonoBehaviour
    {
        public float torque;
        public Transform loadHandle;
        public bool shot;
        public Rigidbody2D body;
        public void Trigger()
        {
            Debug.Log("Shoot");
            body.constraints = RigidbodyConstraints2D.None;
            GetComponent<HingeJoint2D>().motor = new JointMotor2D()
            {
                motorSpeed = -torque,
                maxMotorTorque = 10000
            };
            body.GetComponent<Friend>().forceReceived += Mathf.Abs(torque/10f);
            shot = true;
            body = null;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (shot) return;
            var candidate = other.collider.attachedRigidbody;
            if (candidate&& candidate.name == "Friend"&&candidate.bodyType==RigidbodyType2D.Dynamic)
            {
                body = candidate;
                body.transform.position = loadHandle.position;
                body.transform.rotation = loadHandle.rotation;
                body.linearVelocity = Vector2.zero;
                body.angularVelocity = 0;
                body.constraints = RigidbodyConstraints2D.FreezeRotation;
                Invoke(nameof(Trigger), 3f);
            }
        }
    }
}