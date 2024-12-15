using UnityEngine;

public class positiontestcode : MonoBehaviour
{
	public Transform attack;

	public Transform ball1;
	public Transform ball2;
	public Transform ball3;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			ball1.parent = attack;
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			ball2.parent = attack;
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			ball3.parent = attack;
		}
	}


}
