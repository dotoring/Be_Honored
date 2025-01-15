using System;
using UnityEngine;
using UnityEngine.UI;

public class TutoSandBag : MonoBehaviour
{
    [SerializeField] private Image hpImg;
    public float hp = 1;
    public Action die;

    private void Start()
    {
        hpImg.rectTransform.sizeDelta = new Vector2(1, hpImg.rectTransform.sizeDelta.y);
    }

    public void Damaged()
    {
        hp -= 0.5f;
        hpImg.rectTransform.sizeDelta = new Vector2(hp, hpImg.rectTransform.sizeDelta.y);
        if (hp <= 0)
        {
            Destroy(gameObject,1f);
        }
    }
}
