using TMPro;
using UnityEngine;

public class VendorUI : MonoBehaviour
{
	[SerializeField] TMP_Text textValue;
	[SerializeField] LayerMask targetlayer;
	int sumOfVending;
	int tempitemvalue;

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<ScrapItem>() != null)
		{
			tempitemvalue = other.gameObject.GetComponent<ScrapItem>().Getvalue();
			Debug.Log($"{tempitemvalue}");
			sumOfVending += tempitemvalue;
			textValue.text = $"Value :  {sumOfVending}";
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
		}
		else
		{
			textValue.text = sumOfVending.ToString();
		}
	}

}
