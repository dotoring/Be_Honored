using Unity.Behavior;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Photon.Pun;

public class LookCamera : MonoBehaviour
{
    public Camera mainCam; // 바라볼 카메라 (주로 Main Camera)

	[SerializeField] private Image hpImg;

    private void Start()
	{ 	
		// 기본적으로 Main Camera를 바라보도록 설정
		if (mainCam == null)
            mainCam = Camera.main;

    }

	public void UpdateUI(float val)
	{
		hpImg.rectTransform.sizeDelta = new Vector2(val, hpImg.rectTransform.sizeDelta.y);
	}

	private void LateUpdate()
    {
		
		// 캔버스가 카메라를 바라보도록 설정
		if (mainCam != null)
        {
            transform.LookAt(mainCam.transform);
            transform.Rotate(0, 180, 0); // UI가 뒤집히지 않도록 Y축 회전 추가
        }
    }
}
