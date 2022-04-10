using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuyable 
{
    public Sprite GetItemSprite();
    public int GetCost();
    public bool Locked();
    public string GetTowerName();

}
