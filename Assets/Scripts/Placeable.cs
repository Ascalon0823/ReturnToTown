using System;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public bool placed;
    public bool rotated;
    public Vector2 placedPos;
    public Quaternion placedRot;


    public virtual void StartMove()
    {
        
    }
    public virtual void StopMove()
    {
        
    }
    public virtual void Reset()
    {
        transform.position = placedPos;
        transform.rotation = placedRot;
        gameObject.SetActive(true);
    }
}