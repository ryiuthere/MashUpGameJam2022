using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFreezeAxisTrigger : MonoBehaviour
{
    [SerializeField]
    private bool freezeX;
    
    [SerializeField] 
    private bool freezeY;

    [SerializeField]
    private CameraFreezeAxisEventChannelSO cameraFreezeAxisChannel;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraFreezeAxisChannel.RaiseEvent(gameObject, freezeX, freezeY);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraFreezeAxisChannel.RaiseEvent(gameObject, false, false);
        }
    }
}
