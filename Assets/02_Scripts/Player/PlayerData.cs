using System;
using UnityEngine;
public class EqStat
{
	public PlayerEquipMent _armor;
	public EQUIPSTAT bodyStat;
	public int gold;
}


public partial class Player
{

	//public EqStat playerStat;

	// Stat 데이터를 JSON 형식으로 저장
	void SaveStat(EqStat playerStat)
	{
		string json = JsonUtility.ToJson(playerStat);
		PlayerPrefs.SetString("PlayerStat", json);
		PlayerPrefs.Save();
		Debug.Log("Stat data saved.");
	}

	// 데이터 로드
	public void LoadPlayerData()
	{
		EqStat playerStat = new();
		if (PlayerPrefs.HasKey("PlayerStat"))
		{
			string json = PlayerPrefs.GetString("PlayerStat");
			playerStat = JsonUtility.FromJson<EqStat>(json);
			Debug.Log("Stat data loaded.");
			if (bodyStat.hpmax < 50)
			{
				bodyStat.hpmax = 50;
			}
			LoadData(playerStat);
		}
		else
		{
			Debug.Log("No saved data found.");
			bodyStat.hpmax = 50;
		}
	}

	void LoadData(EqStat playerStatData)
	{
		_armor.Head = playerStatData._armor.Head;
		_armor.Body = playerStatData._armor.Body;
		_armor.Leg = playerStatData._armor.Leg;
		_armor.Arm = playerStatData._armor.Arm;
		bodyStat = playerStatData.bodyStat;
		App.Instance.gold = playerStatData.gold;
	}

	public void SavePlayerData()
	{
		EqStat stat = new();
		stat._armor = new PlayerEquipMent
		{
			Head = _armor.Head,
			Body = _armor.Body,
			Leg = _armor.Leg,
			Arm = _armor.Arm,
		};
		stat.bodyStat = bodyStat;
		stat.gold = App.Instance.gold;

		// Stat 데이터 저장

		SaveStat(stat);
	}

}
