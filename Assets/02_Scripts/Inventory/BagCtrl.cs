using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BagCtrl : MonoBehaviour
{
	public static BagCtrl Instance;
	XRGrabInteractable xRGrabInteractable;

	[SerializeField] int maxWeight;
	[SerializeField] int curWeight = 0;

	[SerializeField] TextMeshProUGUI weightTxt;
	List<ScrapItem> scraps = new List<ScrapItem>();

	[SerializeField] Material bagMat;

	private void Awake()
	{
		xRGrabInteractable = GetComponent<XRGrabInteractable>();
		App.Instance.Resetposition += DestroyBag;

		if(Instance != null && Instance != this)
		{
			Destroy(this.gameObject);
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		App.Instance.interactorManager.AddListener(SetInteractionMgr);

		RefreshText();
	}

	public void SetInteractionMgr(XRInteractionManager mgr)
	{
		xRGrabInteractable.interactionManager = mgr;
	}

	void DestroyBag()
	{
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		App.Instance.Resetposition -= DestroyBag;
		App.Instance.interactorManager.RemoveListener(SetInteractionMgr);

	}

	public bool CheckWeight(ScrapItem item)
	{
		if (scraps.Contains(item))
		{
			return true;
		}

		return (maxWeight - curWeight) >= item.weight;
	}

	public bool IsInBag(ScrapItem scrap)
	{
		return scraps.Contains(scrap);
	}

	public void AddScrap(ScrapItem scrap)
	{
		if (!scraps.Contains(scrap))
		{
			scraps.Add(scrap);
			IncreaseWeigth(scrap.weight);
		}
	}

	public void RemoveScrap(ScrapItem scrap)
	{
		if (scraps.Contains(scrap))
		{
			scraps.Remove(scrap);
			ReduceWeight(scrap.weight);
		}
	}

	public void IncreaseWeigth(int weight)
	{
		curWeight += weight;
		RefreshText();
	}
	public void ReduceWeight(int weight)
	{
		curWeight -= weight;
		RefreshText();
	}

	void RefreshText()
	{
		weightTxt.text = $"weight : {curWeight}/{maxWeight}";
	}

	public void ChangeBagMat(bool b)
	{
		bagMat.color = b ? new Color(0, 1, 0, 0.2f) : new Color(1, 0, 0, 0.2f);
	}

	public void ResetBagMat()
	{
		bagMat.color = new Color(1, 1, 1, 0.2f);
	}
}
