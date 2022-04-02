using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabler : MonoBehaviour
{
    [SerializeField] bool dontUpdate = false;
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    TextMeshPro label;
    Vector2Int gridPosition = new Vector2Int();
    Waypoint waypoint;
    private void Awake()
    {
        label = this.GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
        label.color = defaultColor;
        DisplayCoordinates();
    }
    private void Update()
    {
        if (!Application.isPlaying && !dontUpdate)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        ColorCoordinates();
        ToggleLabels();
    }
    private void ColorCoordinates()
    {
        label.color = !waypoint.IsPlaceable ? blockedColor : defaultColor ;
    }
    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }
    private void DisplayCoordinates()
    {
        gridPosition.x = (int)(this.transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        gridPosition.y = (int)(this.transform.position.z / UnityEditor.EditorSnapSettings.move.y);
        label.text = gridPosition.ToString();

    }
    private void UpdateObjectName()
    {
        if(this.transform.parent.gameObject.name != gridPosition.ToString())
            this.transform.parent.gameObject.name = gridPosition.ToString();
    }
}
