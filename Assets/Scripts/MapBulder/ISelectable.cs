using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable 
{
    public Action<bool> TileSelected();
    public void SelectTarget();
    public void DeselectTarget();
}
