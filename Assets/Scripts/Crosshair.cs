using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void Awake()
    {
        // Hides the normal mouse cursor
        Cursor.visible = false;
    }

    private void Start()
    {
        var renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = mouseWorldPosition;
    }    

    void OnDestroy()
    {
        Cursor.visible = true;
    }
}
