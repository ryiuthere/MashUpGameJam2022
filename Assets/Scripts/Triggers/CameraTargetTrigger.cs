using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private CameraTargetEventChannelSO cameraTargetChannel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraTargetChannel.RaiseEvent(gameObject, true, target);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraTargetChannel.RaiseEvent(gameObject, false, target);
        }
    }
}
