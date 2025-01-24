using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutoSandBag : MonoBehaviour
{
    [SerializeField] private Image hpImg;
    public float hp = 1;
    public Action die;
    [SerializeField] private GameObject tutoEq;
    private bool isWorking = false;
    private void Start()
    {
        hpImg.rectTransform.sizeDelta = new Vector2(1, hpImg.rectTransform.sizeDelta.y);
    }

    public void WorkingOn()
    {
        isWorking = true;
    }

    public void Damaged()
    {
        if (!isWorking) return;
        hp -= 0.5f;
        hpImg.rectTransform.sizeDelta = new Vector2(hp, hpImg.rectTransform.sizeDelta.y);
        if (hp <= 0)
        {
            Destroy(gameObject,1f);
            die.Invoke();
            //Instantiate(tutoEq, transform.position, Quaternion.identity);
        }
    }
}
