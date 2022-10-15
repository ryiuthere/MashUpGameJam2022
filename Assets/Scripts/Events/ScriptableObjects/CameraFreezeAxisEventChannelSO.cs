using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Camera Freeze Axis Event Channel")]
public class CameraFreezeAxisEventChannelSO : EventChannelBaseSO
{
	public UnityAction<GameObject, bool, bool> OnEventRaised;

	public void RaiseEvent(GameObject sender, bool freezeX, bool freezeY)
	{
		OnEventRaised.Invoke(sender, freezeX, freezeY);
	}
}
