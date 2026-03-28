using System;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public bool placed;
    public Vector2 placedPos;
    public Quaternion placedRot;

    public void Place()
    {
        placed = true;
        placedPos = transform.position;
        placedRot = transform.rotation;
    }
    public virtual void Reset()
    {
        transform.position = placedPos;
        transform.rotation = placedRot;
        gameObject.SetActive(true);
    }
}