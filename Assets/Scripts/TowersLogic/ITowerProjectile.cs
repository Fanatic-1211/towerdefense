using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerProjectile 
{
    public ITowerProjectile CreateProjectile(Vector3 from, ITargetable targetable);
    public void DestroyProjectile(bool silent = false);
}
