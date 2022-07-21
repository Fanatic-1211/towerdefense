using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.GameSystem.Input
{
    public class DoubleClickAddon : MonoBehaviour
    {
        private int currentClickCount = 0;
        private float cancelDoubleClickDistance = 10f;
        private float cancelDoubleClickTime = 0.5f;
        private float currentAwaitTime = 0;
        private float currentDistanceFromLastClick = 0;
        public int ClickCount()
        {
            return currentClickCount;
        }
        private void CancelDoubleClick()
        {
            currentClickCount = 0;
            currentAwaitTime = 0;
            currentDistanceFromLastClick = 0;
         
        }
        public void OnClick()
        {
            currentClickCount++;
            currentAwaitTime = 0;
        }
        public void OnDeltaMove(Vector2 delta)
        {
            currentDistanceFromLastClick += delta.magnitude;
            if (currentDistanceFromLastClick > cancelDoubleClickDistance) {
                CancelDoubleClick();
            }
        }
        private void Update()
        {
            if (currentClickCount == 0)
                return;
            if(currentAwaitTime < cancelDoubleClickTime)
            {
                currentAwaitTime += Time.unscaledDeltaTime;
            }
            else
            {
                CancelDoubleClick();
            }


        }
    }
}