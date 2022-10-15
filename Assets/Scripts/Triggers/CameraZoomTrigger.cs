using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomTrigger : MonoBehaviour
{
    [SerializeField]
    private float zoomLevel;

    [SerializeField]
    private CameraZoomEventChannelSO cameraZoomChannel;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraZoomChannel.RaiseEvent(gameObject, true, zoomLevel);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraZoomChannel.RaiseEvent(gameObject, false, zoomLevel);
        }
    }
}
