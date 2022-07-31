using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnEdgeOfScreenDragging 
{
    /// <summary>
    /// Called each frame when cursore or touch on screen edge
    /// Only called if something dragged 
    /// </summary>
    /// <param name="edgeVector"></param>
    public void CursorOnEdgeOfScreen(Vector2 edgeVector);

}
