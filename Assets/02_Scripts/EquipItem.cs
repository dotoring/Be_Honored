using Photon.Pun;
using TMPro;
using UnityEngine;

public class EquipItem : ScrapItem
{
	[SerializeField] EquipType typeOfEquip;
	GameObject parentOfObject;

	Material materialOrigin;
	[SerializeField] Material materialRed;
	bool isOnSlot = false;
	EQUIPSTAT equipStat;

	[SerializeField] TMP_Text TextHp;
	[SerializeField] TMP_Text TextAttack;
	[SerializeField] TMP_Text TextDefence;
	[SerializeField] TMP_Text TextEveda;



	private new void Awake()
	{
		base.Awake();
		materialOrigin = GetComponent<MeshRenderer>().material;
		parentOfObject = transform.parent.gameObject;

	}
	private void OnEnable()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			int[] vars = new int[] { equipStat.hpmax, equipStat.attack, equipStat.defence, equipStat.evade };

			// Random으로 선택할 변수의 개수 (0, 1, 2 중에서 선택)
			int selectedCount = GetWeightedRandomCount();

			// 선택된 변수들에 1~3 사이의 값 부여
			for (int i = 0; i < selectedCount; i++)
			{
				// 0부터 3까지의 랜덤 인덱스를 선택
				int selectedIndex = Random.Range(0, 4);  // 0 ~ 3

				// 해당 인덱스 변수에 1~3 사이의 랜덤 값 부여
				vars[selectedIndex] = Random.Range(1, 4);  // 1, 2, 3 중 랜덤 값
			}

			pv.RPC(nameof(SetState), RpcTarget.AllBuffered, vars);
		}
	}
	protected override void Start()
	{
		base.Start();
		xRGrabInteractable.selectEntered.AddListener((args) =>
		{
			//부모가 있으면 삭제
			if (parentOfObject != null)
			{
				Destroy(parentOfObject);

				//다른 플레이어들에게도 전달
				pv.RPC(nameof(DestroyParent), RpcTarget.OthersBuffered);
			}
		});
	}

	[PunRPC]
	void DestroyParent()
	{
		if(parentOfObject != null)
		{
			transform.parent = null;
			Destroy(parentOfObject);
		}
	}

	int GetWeightedRandomCount()
	{
		// 가중치 설정: 0 -> 50%, 1 -> 35%, 2 -> 15%
		int[] weights = new int[] { 1, 35, 15 };

		// 0부터 100까지의 범위에서 가중치를 반영한 값을 생성
		int totalWeight = weights[0] + weights[1] + weights[2];

		// 랜덤 값 생성 (0에서 totalWeight 사이)
		int randomValue = Random.Range(0, totalWeight);

		// 가중치에 따라 0, 1, 2 중 하나를 선택
		if (randomValue < weights[0])
		{
			return 0;  // 50% 확률
		}
		else if (randomValue < weights[0] + weights[1])
		{
			return 1;  // 35% 확률
		}
		else
		{
			return 2;  // 15% 확률
		}
	}

	[PunRPC]
	void SetState(int[] vars)
	{
		// 결과 출력 (디버그용)
		Debug.Log($"  {typeOfEquip,-10}{vars[0],-10}{vars[1],-10}{vars[2],-10}{vars[3],-10}");
		equipStat.hpmax = vars[0];
		equipStat.attack = vars[1];
		equipStat.defence = vars[2];
		equipStat.evade = vars[3];
		if (typeOfItem == TypeOfItem.EQUIP)
		{
			TextHp.text = vars[0].ToString();
			TextAttack.text = vars[1].ToString();
			TextDefence.text = vars[2].ToString();
			TextEveda.text = vars[3].ToString();
		}
	}

	public void ItemSetter(int value, EquipType equipTypePram)
	{
		base.ItemSetter(value);
		typeOfEquip = equipTypePram;
	}

	private new void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bag"))
		{
			isInBag = true;
			if (bag == null)
			{
				bag = other.gameObject;
			}
		}
		else if (other.gameObject.GetComponent<Equipment>()?.type == typeOfEquip)
		{
			isOnSlot = true;

		}
	}

	private new void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Bag"))
		{
			isInBag = false;
		}
		else if (other.gameObject.GetComponent<Equipment>()?.type == typeOfEquip)
		{
			isOnSlot = false;

		}
	}

	protected override void CheckInBag()
	{
		if (this.isInBag)
		{
			SetInBag();
		}
		else if (isOnSlot)
		{
			PutItOn();
		}
		else
		{
			PullOut();
		}

	}

	private void PutItOn()
	{
		App.Instance.player.ArmorChange(typeOfEquip, equipStat);
		Debug.Log($" equip item in  {typeOfEquip}");
		App.Instance.ChangeEquip.Invoke();
		Destroy(gameObject);

	}
}
