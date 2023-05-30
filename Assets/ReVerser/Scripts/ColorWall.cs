using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReVerser
{
    public class ColorWall : MonoBehaviour
    {
        [Tooltip("色の種類"), SerializeField]
        ColorBG.Colors myColor = ColorBG.Colors.Color1;

        Collider2D myCollider;

        void Start()
        {
            myCollider = GetComponent<Collider2D>();
            ColorBG.onSwitchColor.AddListener(SwitchColor);
            SwitchColor(ColorBG.CurrentColor);
        }

        private void OnDestroy()
        {
            ColorBG.onSwitchColor.RemoveListener(SwitchColor);
        }

        void SwitchColor(ColorBG.Colors color)
        {
            myCollider.enabled = (myColor != color);
        }
    }
}
