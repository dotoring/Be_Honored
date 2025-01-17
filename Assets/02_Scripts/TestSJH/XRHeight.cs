using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class XRHeight : MonoBehaviour
{
	[SerializeField] private XROrigin xrOrigin;
	[SerializeField] private Slider slider;

	private void Start()
	{
		slider.onValueChanged.AddListener((value) => xrOrigin.CameraYOffset = 1.6f + value * 4.0f);
		slider.onValueChanged.AddListener((value) => App.Instance.xrHeightValue=value);
	}
	private void OnEnable()
	{
		slider.value = App.Instance.xrHeightValue;
	}
}
