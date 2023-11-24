using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TowerBlueprint 
{
    public GameObject prefabs;
    public int cost;
    public TowerType type;

	public int GetSellAmount()
	{
		return cost / 2;
	}
}

public enum TowerType
{
    Melee,
    Range
}