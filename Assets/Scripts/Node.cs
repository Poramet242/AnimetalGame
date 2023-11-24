using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    public TowerType nodeType;

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject tower;
    [HideInInspector]
    public TowerBlueprint towerBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildTowerManager buildManager;

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildTowerManager.instance;

    }

    private void Update()
    {
        ColorChange();
    }

    void ColorChange()
    {
        rend.material.color = hoverColor;

        if (buildManager.CanBuild)
        {
            if (buildManager.GetTowerToBuild().type != nodeType)
            {
                rend.material.color = notEnoughMoneyColor;
            }
            else if (buildManager.GetTowerToBuild().type == nodeType && !buildManager.HasMoney)
            {
                rend.material.color = notEnoughMoneyColor;
            }
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (tower != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        if (buildManager.GetTowerToBuild().type != nodeType)
            return;

        BuildTower(buildManager.GetTowerToBuild());
    }
    public void BuildTower(TowerBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefabs, GetBuildPosition(), Quaternion.identity);
        tower = _turret;

        towerBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
        FindObjectOfType<SoundManager>().PlaySounded("Constructing Robot");

        Debug.Log("Turret build!");
    }

    public void SellTower()
    {
        PlayerStats.Money += towerBlueprint.GetSellAmount();

        Destroy(tower);
        towerBlueprint = null;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
            return;
        /*if (!buildManager.HasMoney || buildManager.GetTowerToBuild().type != nodeType)
        {
            rend.material.color = notEnoughMoneyColor;
        }
        else
        {
            rend.material.color = hoverColor;
        }*/
    }
    private void OnMouseExit()
    {
        //rend.material.color = startColor;   
    }
}
