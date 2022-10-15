using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [Header("Event Channels")]
    [SerializeField]
    private CameraZoomEventChannelSO cameraZoomChannel;

    [SerializeField]
    private CameraTargetEventChannelSO cameraTargetChannel;

    [SerializeField]
    private CameraFreezeAxisEventChannelSO cameraFreezeAxisChannel;

    [Header("References")]
    [SerializeField]
    private Camera currentCamera;
    
    [SerializeField]
    private CameraFollow cameraFollow;
    
    [SerializeField]
    private Transform player;

    private float defaultZoomLevel;

    private GameObject cameraZoomTrigger;
    
    private Coroutine cameraZoomCoroutine;
    
    private GameObject cameraTargetTrigger;

    private void Awake()
    {
        cameraZoomChannel.OnEventRaised += OnSetCameraZoom;
        cameraTargetChannel.OnEventRaised += OnSetCameraTarget;
        cameraFreezeAxisChannel.OnEventRaised += OnFreezeCameraAxis;
    }

    private void Start()
    {
        defaultZoomLevel = currentCamera.orthographicSize;

        if (cameraFollow.Target == null)
        {
            cameraFollow.Target = player;
        }
    }

    private IEnumerator SetCameraZoomCoroutine(float zoom, float time)
    {
        float normalizedTime = 0;
        float size = currentCamera.orthographicSize;

        do
        {
            normalizedTime += Time.deltaTime / time;
            currentCamera.orthographicSize = Mathf.Lerp(size, zoom, normalizedTime);
            yield return null;
        } while (normalizedTime < 1);
    }

    private void OnSetCameraZoom(GameObject sender, bool state, float zoom)
    {
        if (state)
        {
            if (cameraZoomCoroutine != null)
            {
                StopCoroutine(cameraZoomCoroutine);
            }

            cameraZoomTrigger = sender;
            cameraZoomCoroutine = StartCoroutine(SetCameraZoomCoroutine(zoom, 1));
        }
        else
        {
            if (cameraZoomTrigger == sender)
            {
                if (cameraZoomCoroutine != null)
                {
                    StopCoroutine(cameraZoomCoroutine);
                }

                cameraZoomTrigger = sender;
                cameraZoomCoroutine = StartCoroutine(SetCameraZoomCoroutine(defaultZoomLevel, 1));
            }
        }
    }

    private void OnFreezeCameraAxis(GameObject sender, bool freezeX, bool freezeY)
    {
        cameraFollow.FollowX = !freezeX;
        cameraFollow.FollowY = !freezeY;
    }

    private void OnSetCameraTarget(GameObject sender, bool state, Transform target)
    {
        if (state)
        {
            cameraTargetTrigger = sender;
            cameraFollow.Target = target;
        }
        else
        {
            if (cameraTargetTrigger == sender)
            {
                cameraTargetTrigger = null;
                cameraFollow.Target = player;
            }
        }
    }
}
