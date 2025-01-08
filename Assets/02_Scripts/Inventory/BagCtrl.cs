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
		App.Instance.interactorManager.AddListener((mgr) =>
		{
			SetInteractionMgr(mgr);
		});

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
	}

	public bool CheckWeight(int weight)
	{
		if ((maxWeight - curWeight) >= weight)
		{
			return true;
		}
		else
		{
			return false;
		}
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
}
