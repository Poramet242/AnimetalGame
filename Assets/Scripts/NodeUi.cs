using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUi : MonoBehaviour
{
	public GameObject ui;
	public Text sellAmount;
	private Node target;

	public void SetTarget(Node _target)
	{
		target = _target;

		transform.position = target.GetBuildPosition();

		sellAmount.text = "$ " + target.towerBlueprint.GetSellAmount();
		ui.SetActive(true);
	}

	public void Hide()
	{
		ui.SetActive(false);
	}

	public void Sell()
	{
		target.SellTower();
		BuildTowerManager.instance.DeselectNode();
	}

}
