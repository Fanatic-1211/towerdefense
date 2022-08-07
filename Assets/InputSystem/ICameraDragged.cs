using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.GameSystem
{
    public interface ICameraMoved
    {
        public void GetOnCameraWasMovedDeltaActionSubscribe(Action<Vector3> deltaEvent);
        public void GetOnCameraWasMovedDeltaActionUnsubscribe(Action<Vector3> deltaEvent);
    }
}