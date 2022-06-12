using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Environment.Map
{
    public abstract class MapDataSerializerBase : MonoBehaviour
    {
        public abstract IMapData GetMapData();
        public abstract void SerializeMap();
    }

}

