using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Camera Zoom Event Channel")]
public class CameraZoomEventChannelSO : EventChannelBaseSO
{
	public UnityAction<GameObject, bool, float> OnEventRaised;

	public void RaiseEvent(GameObject sender, bool state, float value)
	{
		OnEventRaised.Invoke(sender, state, value);
	}
}
