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
	private float offTime=3.0f;

	[SerializeField] private float curTimeHeight;
	[SerializeField] private float curTimeVolume;

	[SerializeField]private InputActionProperty xButtonAction;
	[SerializeField]private InputActionProperty yButtonAction;
	[SerializeField]private InputActionProperty aButtonAction;
	[SerializeField]private InputActionProperty bButtonAction;

	private void Update()
	{
		if (sliderHeight.gameObject.activeSelf)
		{
			curTimeHeight += Time.deltaTime;
			if (curTimeHeight >= offTime)
			{
				sliderHeight.gameObject.SetActive(false);
				curTimeHeight = 0;
			}
		}
		if (sliderVolume.gameObject.activeSelf)
		{
			curTimeVolume += Time.deltaTime;
			if (curTimeVolume >= offTime)
			{
				sliderVolume.gameObject.SetActive(false);
				curTimeVolume = 0;
			}
		}
	}

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
		if (!sliderVolume.gameObject.activeSelf)
			sliderVolume.gameObject.SetActive(true);
		else
		{
			curTimeVolume = 0;
			sliderVolume.value -= volumeCaption;
		}
	}
	public void YTest(InputAction.CallbackContext context)
	{
		print("Y버튼눌림");
		if (!sliderVolume.gameObject.activeSelf)
			sliderVolume.gameObject.SetActive(true);
		else

		{
			curTimeVolume = 0;
			sliderVolume.value += volumeCaption;
		}
	}
	public void ATest(InputAction.CallbackContext context)
	{
		print("A버튼눌림");
		if (!sliderHeight.gameObject.activeSelf)
			sliderHeight.gameObject.SetActive(true);
		else

		{
			curTimeHeight = 0;
			sliderHeight.value -= heightCaption;
		}
	}
	public void BTest(InputAction.CallbackContext context)
	{
		print("B버튼눌림");
		if (!sliderHeight.gameObject.activeSelf)
			sliderHeight.gameObject.SetActive(true);
		else

		{
			curTimeHeight = 0;
			sliderHeight.value += heightCaption;
		}
	}
}
