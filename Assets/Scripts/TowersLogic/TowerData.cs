using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TDAsset/CreateTowerData")]
public class TowerData : ScriptableObject, IBuyable
{
    [SerializeField] string towerName;
    [SerializeField] Tower towerPrefab;
    [SerializeField] int cost = 75;
    [SerializeField] Sprite towerSprite;
    public int TowerCost => cost;
    public Tower TowerPrefab => towerPrefab;

    public Sprite GetItemSprite() => towerSprite;

    public int GetCost() => cost;

    public bool Locked() => false;

    public string GetTowerName() => towerName;

}
