using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector2 offset;

    [SerializeField]
    private bool followX = true;

    [SerializeField]
    private bool followY = true;

    [SerializeField]
    private float speed = 10;

    public Transform Target { get => target; set => target = value; }

    public bool FollowX { get => followX; set => followX = value; }

    public bool FollowY { get => followY; set => followY = value; }

    private void LateUpdate()
    {
        Vector3 p = transform.position;
        if (followX) p.x = Mathf.Lerp(p.x, target.position.x + offset.x, speed * Time.deltaTime);
        if (followY) p.y = Mathf.Lerp(p.y, target.position.y + offset.y, speed * Time.deltaTime);
        transform.position = p;
    }
}
