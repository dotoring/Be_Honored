using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
	[SerializeField] int width; //가로
	[SerializeField] int length; //세로
	[SerializeField] int exceptCount; //제외할 모듈 수
	[SerializeField] float gridLength; //모듈 폭

	List<List<DungeonRoomModule>> modules = new List<List<DungeonRoomModule>>();

	[SerializeField] GameObject[] modulePrefs;
	[SerializeField] GameObject wallPref;
	[SerializeField] GameObject doorPref;

	[SerializeField] PlayerSpawnMgr spawnMgr;
	[SerializeField] PhotonView photonView;
	[SerializeField] NavMeshCtrl navMeshCtrl;
	[SerializeField] MainFactory mainFactory;
	[SerializeField] ExitMgr exitMgr;
	[SerializeField] DungeonMgr dungeonMgr;

	private void Awake()
	{
		//DungeonRoomModule타입 포톤에 등록하기
		PhotonPeer.RegisterType(typeof(DungeonRoomModule), 0, DungeonRoomModule.Serialize, DungeonRoomModule.Deserialize);
	}

	private void Start()
	{
		//방장만 실행
		if (photonView.IsMine)
		{
			//생성될 모듈들 설정
			SetModules();

			//생성된 모듈 통신을 위해 1차원 배열로 바꾸기
			DungeonRoomModule[] oneDimModules = MakeModulesToArray(modules);

			//모듈들 생성, 나중에 입장하는 플레이어에게도 실행시켜주기
			photonView.RPC(nameof(GenModulesRPC), RpcTarget.AllBuffered, oneDimModules);
			//모듈간 통로 생성, 나중에 입장하는 플레이어에게도 실행시켜주기
			photonView.RPC(nameof(GenWaysRPC), RpcTarget.AllBuffered, oneDimModules);
		}
	}

	//2차원 리스트 1차원 배열로 바꾸는 함수
	DungeonRoomModule[] MakeModulesToArray(List<List<DungeonRoomModule>> _modules)
	{
		List<DungeonRoomModule> flattened = new List<DungeonRoomModule>();

		foreach (var m in modules)
		{
			foreach (var module in m)
			{
				flattened.Add(module);
			}
		}

		DungeonRoomModule[] result = flattened.ToArray();
		return result;
	}

	//중복 제외 랜덤 뽑기 함수
	List<int> RandomWithoutDup(int min, int max, int count)
	{
		List<int> nums = new List<int>();

		for (int i = 0; i < count;)
		{
			bool isSame = false;
			int rand = Random.Range(min, max);

			//중복 검사
			for (int j = 0; j < nums.Count; j++)
			{
				if (nums[j] == rand)
				{
					isSame = true;
					break;
				}
			}

			//중복이면 다시
			if (isSame)
			{
				continue;
			}
			else
			{
				//제외 리스트에 추가
				nums.Add(rand);
				i++;
			}
		}

		return nums;
	}

	void SetModules()
	{
		//가로, 세로 길이 그리드의 던전모듈들 생성
		for (int i = 0; i < width; i++)
		{
			List<DungeonRoomModule> temp = new List<DungeonRoomModule>();
			for (int j = 0; j < length; j++)
			{
				temp.Add(new DungeonRoomModule(i, j));
			}
			modules.Add(temp);
		}

		List<int> points = new List<int>();
		while (true)
		{
			//비활성화 할 모듈 선정
			points = RandomWithoutDup(0, width * length, exceptCount);

			//모듈 고립상황 예외처리
			if (points.Contains(1) && points.Contains(3) ||
				points.Contains(1) && points.Contains(5) ||
				points.Contains(3) && points.Contains(7) ||
				points.Contains(5) && points.Contains(7))
			{
				points.Clear();
				continue;
			}

			break;
		}

		//제외할 위치의 모듈 제외 설정
		foreach (int num in points)
		{
			switch (num)
			{
				case 0:
					modules[0][0].isExcepted = true;
					break;
				case 1:
					modules[0][1].isExcepted = true;
					break;
				case 2:
					modules[0][2].isExcepted = true;
					break;
				case 3:
					modules[1][0].isExcepted = true;
					break;
				case 4:
					modules[1][1].isExcepted = true;
					break;
				case 5:
					modules[1][2].isExcepted = true;
					break;
				case 6:
					modules[2][0].isExcepted = true;
					break;
				case 7:
					modules[2][1].isExcepted = true;
					break;
				case 8:
					modules[2][2].isExcepted = true;
					break;
			}
		}

		////비활성화 할 모듈 선정
		//List<Coordinate> points = new List<Coordinate>();
		//for(int i = 0; i < exceptCount;)
		//{
		//	bool isSame = false;

		//	//x, y값 랜덤 설정
		//	int ranX = Random.Range(0, width);
		//	int ranY = Random.Range(0, width);

		//	//points리스트에서 중복 검사
		//	for(int j = 0; j < points.Count; j++)
		//	{
		//		if (points[j].x == ranX && points[j].y == ranY)
		//		{
		//			isSame = true;
		//			break;
		//		}
		//	}

		//	//중복이 있으면
		//	if(isSame)
		//	{
		//		//다시 뽑기
		//		continue;
		//	}
		//	else
		//	{
		//		//중복 없으면 추가하기
		//		points.Add(new Coordinate(ranX, ranY));
		//		i++;
		//	}
		//}

		////제외할 위치의 모듈 제외 설정
		//foreach(Coordinate coord in points)
		//{
		//	modules[coord.x][coord.y].isExcepted = true;
		//}

		//생성될 모듈들의 타입 정하기
		List<ModuleType> types = new List<ModuleType> { ModuleType.Start, ModuleType.End, ModuleType.Boss };
		int baseNum = types.Count;
		for (int i = 0; i < (width * length) - exceptCount - baseNum; i++)
		{
			var enumValues = System.Enum.GetValues(enumType: typeof(ModuleType));
			ModuleType moduleType = (ModuleType)enumValues.GetValue(Random.Range(3, enumValues.Length));
			types.Add(moduleType);
		}

		foreach (var a in modules)
		{
			foreach (var module in a)
			{
				if (!module.isExcepted)
				{
					int rand = Random.Range(0, types.Count);
					module.moduleType = types[rand];
					types.RemoveAt(rand);
				}
			}
		}
	}

	[PunRPC]
	void GenModulesRPC(DungeonRoomModule[] _modules)
	{
		int id = 0;
		foreach (DungeonRoomModule module in _modules)
		{
			if (!module.isExcepted)
			{
				Vector3 position = new Vector3(module.x * gridLength, 0, module.y * gridLength);
				GameObject moduleObject = null;
				switch (module.moduleType)
				{
					case ModuleType.Start:
						moduleObject = Instantiate(modulePrefs[(int)ModuleType.Start], position, Quaternion.identity);
						Transform spawnPoint = moduleObject.GetComponent<StartModule>().GetSpawnPoint();
						spawnMgr.SetSpawnPoint(spawnPoint);
						break;
					case ModuleType.End:
						moduleObject = Instantiate(modulePrefs[(int)ModuleType.End], position, Quaternion.identity);
						moduleObject.GetComponentInChildren<ExitPortal>().SetExitMgr(exitMgr);
						break;
					case ModuleType.Monsters:
						moduleObject = Instantiate(modulePrefs[(int)ModuleType.Monsters], position, Quaternion.identity);
						moduleObject.GetComponent<MonsterSpawner>().SetFactory(mainFactory);
						break;
					case ModuleType.Scraps:
						moduleObject = Instantiate(modulePrefs[(int)ModuleType.Scraps], position, Quaternion.identity);
						moduleObject.GetComponent<ScrapSpawner>().SetFactory(mainFactory);
						break;
					case ModuleType.Boss:
						moduleObject = Instantiate(modulePrefs[(int)ModuleType.Boss], position, Quaternion.identity);
						moduleObject.GetComponent<MonsterSpawner>().SetFactory(mainFactory);
						break;
				}
				moduleObject.GetComponent<ModuleMgr>().moduleId = id++;
			}
		}
		navMeshCtrl.BakeSurface();
	}

	[PunRPC]
	void GenWaysRPC(DungeonRoomModule[] _modules)
	{
		//1차원 배열 다시 2차원으로 만들어주기
		List<List<DungeonRoomModule>> tempModules = new List<List<DungeonRoomModule>>();
		int count = 0;
		for (int i = 0; i < width; i++)
		{
			List<DungeonRoomModule> temp = new List<DungeonRoomModule>();
			for (int j = 0; j < length; j++)
			{
				temp.Add(_modules[count++]);
			}
			tempModules.Add(temp);
		}

		//BFS로 통로 만들기
		Queue<DungeonRoomModule> q = new Queue<DungeonRoomModule>();
		for (int i = 0; i < 3; i++)
		{
			//제외되지 않은 모듈을 처음으로 넣기
			if (!tempModules[i][0].isExcepted)
			{
				q.Enqueue(tempModules[i][0]);
				break;
			}
		}

		while (q.Count > 0)
		{
			DungeonRoomModule now = q.Dequeue();
			//방문 설정
			if(now.isVisited)
			{
				continue;
			}
			else
			{
				now.isVisited = true;
			}

			int[] dx = { 0, 0, -1, 1 };
			int[] dy = { 1, -1, 0, 0 };
			//상하좌우 순으로 탐색
			for (int i = 0; i < dx.Length; i++)
			{
				try
				{
					//모듈 가져오기
					DungeonRoomModule next = tempModules[now.x + dx[i]][now.y + dy[i]];
					//방문한 곳이 아니고 제외된 곳이 아니면
					if (!next.isVisited && !next.isExcepted)
					{
						//다음 모듈 방향에 문 생성, 다음 모듈 큐에 넣기
						Vector3 point = new Vector3(now.x * gridLength + dx[i] * (gridLength / 2), 0, now.y * gridLength + dy[i] * (gridLength / 2));
						if (i < 2)
						{
							if (PhotonNetwork.IsMasterClient)
							{
								PhotonNetwork.InstantiateRoomObject("Door", point, Quaternion.Euler(0, 90, 0));
							}
						}
						else
						{
							if (PhotonNetwork.IsMasterClient)
							{
								PhotonNetwork.InstantiateRoomObject("Door", point, Quaternion.identity);
							}
							//Instantiate(doorPref, point, Quaternion.identity);
						}
						q.Enqueue(next);
					}
					//방향이 제외된 모듈이면
					if (next.isExcepted)
					{
						//모듈 방향에 벽 세우기
						Vector3 point = new Vector3(now.x * gridLength + dx[i] * (gridLength / 2), 0, now.y * gridLength + dy[i] * (gridLength / 2));
						if (i < 2)
						{
							Instantiate(wallPref, point, Quaternion.Euler(0, 90, 0));
						}
						else
						{
							Instantiate(wallPref, point, Quaternion.identity);
						}
					}
				}
				catch
				{
					//규격 외 방향이면 벽 세우기
					Vector3 point = new Vector3(now.x * gridLength + dx[i] * (gridLength / 2), 0, now.y * gridLength + dy[i] * (gridLength / 2));
					if (i < 2)
					{
						Instantiate(wallPref, point, Quaternion.Euler(0, 90, 0));
					}
					else
					{
						Instantiate(wallPref, point, Quaternion.identity);
					}
					continue;
				}
			}
		}

	}
}

public class Coordinate
{
	public int x;
	public int y;

	public Coordinate(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}
