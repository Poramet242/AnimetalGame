using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTowerManager : MonoBehaviour
{
	private TowerBlueprint towerToBuild;
	private Node selectedNode;
	private int maxBuildTower = 8;
	public static BuildTowerManager instance;
	public GameObject buildEffect;
	public NodeUi nodeUI;

	public bool CanBuild { get { return towerToBuild != null; } }
	public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost; } }

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}
	public void SelectNode(Node node)
	{
		if (selectedNode == node)
		{
			DeselectNode();
			return;
		}

		selectedNode = node;
		towerToBuild = null;

		nodeUI.SetTarget(node);
	}

	public void DeselectNode()
	{
		selectedNode = null;
		nodeUI.Hide();
	}

	public void SelectTowerTobuild(TowerBlueprint turret)
	{
		towerToBuild = turret;
		DeselectNode();
	}

	public TowerBlueprint GetTowerToBuild()
	{
		return towerToBuild;
	}
}
