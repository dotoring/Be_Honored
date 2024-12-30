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

	public Player player;

	protected override void Awake()
	{
		base.Awake();
		player = new();

	}
}


public enum EquipType
{
	HEAD,
	BODY,
	LEG,
	ARM
}

public struct EQUIPSTAT
{
	public int hpmax;
	public int attack;
	public int defence;
	public int evade;
	public static EQUIPSTAT operator +(EQUIPSTAT stat1, EQUIPSTAT stat2)
	{
		return new EQUIPSTAT
		{
			hpmax = stat1.hpmax + stat2.hpmax,
			attack = stat1.attack + stat2.attack,
			defence = stat1.defence + stat2.defence,
			evade = stat1.evade + stat2.evade
		};
	}
}
