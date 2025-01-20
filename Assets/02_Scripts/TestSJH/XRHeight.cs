using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class XRHeight : MonoBehaviour
{
	[SerializeField] private XROrigin xrOrigin;
	[SerializeField] private Slider sliderHeight;
	[SerializeField] private Slider sliderVolume;

	[SerializeField] private AudioSource au;
	private float heightCaption=0.1f;
	private float volumeCaption=0.1f;



	public InputActionAsset inputActions; // XRI Default Input Actions 파일
	[SerializeField]private InputActionProperty xButtonAction;
	[SerializeField]private InputActionProperty yButtonAction;
	[SerializeField]private InputActionProperty aButtonAction;
	[SerializeField]private InputActionProperty bButtonAction;

	private void Awake()
	{
		au=Player.Instance.GetComponent<AudioSource>();
	}

	private void Start()
	{
		sliderHeight.onValueChanged.AddListener((value) => xrOrigin.CameraYOffset = 1.4f + value * 0.6f);
		sliderHeight.onValueChanged.AddListener((value) => App.Instance.xrHeightValue=value);

		sliderVolume.onValueChanged.AddListener((value) => au.volume = value);
		sliderVolume.onValueChanged.AddListener((value) => App.Instance.audioValue = value);
	}
	private void OnEnable()
	{
		sliderHeight.value = App.Instance.xrHeightValue;
		xrOrigin.CameraYOffset = 1.4f + sliderHeight.value * 0.6f;

		sliderVolume.value = App.Instance.audioValue;
		au.volume = sliderVolume.value;

		xButtonAction.action.started += XTest;
		yButtonAction.action.started += YTest;
		aButtonAction.action.started += ATest;
		bButtonAction.action.started += BTest;

		xButtonAction.action.Enable();
		yButtonAction.action.Enable();
		aButtonAction.action.Enable();
		bButtonAction.action.Enable();
	}
	private void OnDisable()
	{
		xButtonAction.action.started -= XTest;
		yButtonAction.action.started -= YTest;
		aButtonAction.action.started -= ATest;
		bButtonAction.action.started -= BTest;

		xButtonAction.action.Disable();
		yButtonAction.action.Disable();
		aButtonAction.action.Disable();
		bButtonAction.action.Disable();
	}
	public void XTest(InputAction.CallbackContext context)
	{
		print("X버튼눌림");
		sliderVolume.value -= volumeCaption;
	}
	public void YTest(InputAction.CallbackContext context)
	{
		print("Y버튼눌림");
		sliderVolume.value += volumeCaption;
	}
	public void ATest(InputAction.CallbackContext context)
	{
		print("A버튼눌림");
		sliderHeight.value -= heightCaption;
	}
	public void BTest(InputAction.CallbackContext context)
	{
		print("B버튼눌림");
		sliderHeight.value += heightCaption;
	}
}
