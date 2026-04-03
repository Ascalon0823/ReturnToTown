using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class FloatingText : MonoBehaviour
    {
        public RectTransform rect;
        public float offset;

        public void Start()
        {
            offset = rect.anchoredPosition.y;
        }

        private void LateUpdate()
        {
            rect.anchoredPosition = new Vector2(transform.localPosition.x, offset+Mathf.Sin(Time.time * 1.5f) *3f);
        }
    }
}