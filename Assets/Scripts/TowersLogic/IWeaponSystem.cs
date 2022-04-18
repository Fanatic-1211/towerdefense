using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponSystem
{
    public void SetNewTarget(ITargetable targetable);
    public void ClearTargets();

}
