using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable 
{
    public Action<bool> IsSelectedCallback();
    public void SelectTarget();
    public void DeselectTarget();
}
