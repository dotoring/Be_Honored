using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class App : Singleton<App>
{
	public MonsterStat warrior1;
	public Action Resetposition;
	public List<String> inventory;
	public Observable<XRInteractionManager> interactorManager;
}
