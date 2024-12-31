using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestGoLobby : MonoBehaviourPunCallbacks
{
	[SerializeField] private Button LobbyBtn;
	[SerializeField] private Button OpenDoor;
	[SerializeField] private MonsterSpawner sp;

	private void Start()
	{
		LobbyBtn.onClick.AddListener(() => LessHpMonster());
		OpenDoor.onClick.AddListener(() => MonstersActive());
	}
	public void LessHpMonster()
	{
		Monster[] monsters = GameObject.FindObjectsByType<Monster>(FindObjectsSortMode.None);
		for(int i=0;i<monsters.Length;i++)
		{
			monsters[i].Damaged(1);
		}
	}

	public void MonstersActive()
	{
		Monster[] mon=GameObject.FindObjectsByType<Monster>(FindObjectsSortMode.None);

		foreach(var go in mon)
		{
			go.ActiveSelf();
		}
	}
}
