using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VendorUI : MonoBehaviour
{
	[SerializeField] TMP_Text textValue;
	[SerializeField] private TMP_Text textGold;
	[SerializeField] LayerMask targetlayer;
	int sumOfVending;
	int tempitemvalue;
	List<GameObject> preprereItem;

	private void OnEnable()
	{
		preprereItem = new();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<ScrapItem>() != null)
		{
			tempitemvalue = other.gameObject.GetComponent<ScrapItem>().Getvalue();
			Debug.Log($"{tempitemvalue}");
			sumOfVending += tempitemvalue;
			textValue.text = $"Value :  {sumOfVending}";
			preprereItem.Add(other.gameObject);
		}
		else
		{
			textValue.text = sumOfVending.ToString();
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.GetComponent<ScrapItem>() != null)
		{
			tempitemvalue = other.gameObject.GetComponent<ScrapItem>().Getvalue();
			Debug.Log($"{tempitemvalue}");
			sumOfVending -= tempitemvalue;
			textValue.text = $"Value :  {sumOfVending}";
			preprereItem.Remove(other.gameObject);
		}
		else
		{
			textValue.text = sumOfVending.ToString();
		}
	}

	public void SellItem()
	{
		App.Instance.gold += sumOfVending;
		sumOfVending = 0;
		foreach (var item in preprereItem)
		{
			Destroy(item);
		}
		preprereItem = new();
		textValue.text = $"Value :  {sumOfVending}";
		textGold.text = $"Gold :  {App.Instance.gold}";
	}

}
