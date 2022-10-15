using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Camera Target Event Channel")]
public class CameraTargetEventChannelSO : EventChannelBaseSO
{
	public UnityAction<GameObject, bool, Transform> OnEventRaised;

	public void RaiseEvent(GameObject sender, bool state, Transform value)
	{
		OnEventRaised.Invoke(sender, state, value);
	}
}
