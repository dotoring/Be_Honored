using System;
using UnityEngine;

public class UIPositioner : MonoBehaviour
{
    GameObject targetObject; // 3D 오브젝트
    Canvas uiCanvas; // 연결된 UI Canvas
    public Vector3 offset = new Vector3(0.125f, 1.19f, 0.79f); // Canvas 위치의 오프셋

    private void Awake()
    {
        targetObject = transform.parent.gameObject;
        uiCanvas = GetComponent<Canvas>();
    }

    void Update()
    {
        // 3D 오브젝트의 위치에 따라 UI Canvas를 오브젝트 위쪽에 배치

        // 오브젝트의 위치 + 오프셋을 UI Canvas의 위치로 설정
        uiCanvas.transform.position = targetObject.transform.position + offset;

        // UI Canvas는 항상 회전하지 않도록 회전값을 고정
        uiCanvas.transform.rotation = Quaternion.identity; // 회전을 초기화 (기본 회전)
    }
}