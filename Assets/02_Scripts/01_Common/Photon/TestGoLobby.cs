using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestGoLobby : MonoBehaviourPunCallbacks
{
	[SerializeField] private Button LobbyBtn;

	private void Start()
	{
		LobbyBtn.onClick.AddListener(() => LessHpMonster());
	}
	public void LessHpMonster()
	{
		Monster[] monsters = GameObject.FindObjectsByType<Monster>(FindObjectsSortMode.None);
		for(int i=0;i<monsters.Length;i++)
		{
			monsters[i].Damaged(1);
		}
	}
}
