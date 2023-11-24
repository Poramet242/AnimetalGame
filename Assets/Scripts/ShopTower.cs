using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTower : MonoBehaviour
{
    BuildTowerManager buildManager;

    public TowerBlueprint TowerArrowBlueprint;
    public TowerBlueprint TowerRockBlueprint;
    public TowerBlueprint TowerMagicBlueprint;

    private void Start()
    {
        buildManager = BuildTowerManager.instance;
    }
    public void SelectTowerArrow()
    {
        Debug.Log("Build Tower Arrow Selected");
        buildManager.SelectTowerTobuild(TowerArrowBlueprint);
    }
    public void SelectTowerRock()
    {
        Debug.Log("Build Tower Rock Selected");
        buildManager.SelectTowerTobuild(TowerRockBlueprint);
    }
    public void SelectTowerMagic()
    {
        Debug.Log("Build Tower Magic Selected");
        buildManager.SelectTowerTobuild(TowerMagicBlueprint);
    }

}
