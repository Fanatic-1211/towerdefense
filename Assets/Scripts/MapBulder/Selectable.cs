using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Selectable : MonoBehaviour, ISelectable
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material highLightMaterial;
    Action<bool> ISelectable.IsSelectedCallback() => isSelectedCallBack;
    private event Action<bool> isSelectedCallBack;
    int highlightMaterialIndex = -1;
    bool isSelected = false;
    
    Coroutine highlightCoroutine;
    (Color, Color) highLightColor = new(new Color(1, 1, 1, 0.2f), new Color(1, 1, 1, 0.5f));
    private void AddHighlightMaterial()
    {
        if (highlightMaterialIndex == -1)
        {
            meshRenderer.materials = meshRenderer.materials.Concat(new Material[] { highLightMaterial }).ToArray();
            highlightMaterialIndex = meshRenderer.materials.Length-1;
        }
    }
    private void DisableHighlight()
    {
        if (highlightMaterialIndex != -1)
        {
            meshRenderer.materials[highlightMaterialIndex].color = Color.clear;
        }
    }
    public void SelectTarget()
    {
        if (isSelected) return;
        AddHighlightMaterial();
        StopHighlight();
        highlightCoroutine = StartCoroutine(HighlightRoutine(highlightMaterialIndex));
        isSelected = true;
    }
    private void StopHighlight()
    {
        if (highlightCoroutine != null)
        {
            StopCoroutine(highlightCoroutine);
        }
        DisableHighlight();

    }
    public void DeselectTarget()
    {
        if (!isSelected) return;
        isSelected = true;
        StopHighlight();
    }
    private IEnumerator HighlightRoutine(int targetMaterialIndex)
    {
        while (true)
        {
            float pingPong = Mathf.PingPong(Time.time, 1);
            Color color = Color.Lerp(highLightColor.Item1, highLightColor.Item2, pingPong);
            meshRenderer.materials[targetMaterialIndex].SetColor("_Color", color);
            yield return null;
        }
    }
}
