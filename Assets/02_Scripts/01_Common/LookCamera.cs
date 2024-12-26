using Unity.Behavior;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LookCamera : MonoBehaviour
{
    public Camera mainCam; // 바라볼 카메라 (주로 Main Camera)
	[SerializeField] private Image hpImg;
	[SerializeField] private BehaviorGraphAgent agent;
	BlackboardVariable<float> hpval;
	BlackboardVariable<GameObject> Selfval;

    private void Start()
    {
		
		// 기본적으로 Main Camera를 바라보도록 설정
		if (mainCam == null)
            mainCam = Camera.main;
    }

    private void LateUpdate()
    {
		agent.BlackboardReference.GetVariable("Hp", out hpval);
		agent.BlackboardReference.GetVariable("Self", out Selfval);
		float hpPer = hpval.Value / Selfval.Value.GetComponent<Monster>().hp;
		hpImg.rectTransform.sizeDelta = new Vector2(hpPer, hpImg.rectTransform.sizeDelta.y);
		// 캔버스가 카메라를 바라보도록 설정
		if (mainCam != null)
        {
            transform.LookAt(mainCam.transform);
            transform.Rotate(0, 180, 0); // UI가 뒤집히지 않도록 Y축 회전 추가
        }
    }
}
