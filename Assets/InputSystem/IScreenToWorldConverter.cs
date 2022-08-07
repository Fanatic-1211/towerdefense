using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.GameSystem
{
    public interface IScreenToWorldConverter
    {
        public Vector3 FromScreenToWorld(Vector2 screenCoord);
        public Vector3 FromScreenDeltaToWorldDelta(Vector2 screenCoord);
    }
}
