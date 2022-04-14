using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarket
{
    public void SetObjects<T>(List<T> buyables, Action<T> callBack) where T : IBuyable;
}
