using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class App : Singleton<App>
{
	public MonsterStat Warrior1;
	public MonsterStat Archer1;
	public Action Resetposition;

	public Observable<XRInteractionManager> interactorManager;
}
