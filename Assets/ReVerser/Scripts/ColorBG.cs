using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ReVerser
{
    public class ColorBG : MonoBehaviour
    {
        public static ColorBG Instance { get; private set; }

        [Tooltip("ステージ開始時の色"), SerializeField]
        Colors startColor = Colors.Color1;
        [Tooltip("色の指定"), SerializeField]
        Color[] colorList = new Color[2];

        public static Colors CurrentColor { get; private set; }
        public readonly static UnityEvent<Colors> onSwitchColor = new UnityEvent<Colors>();
        SpriteRenderer spriteRenderer;

        /// <summary>
        /// 色反転スイッチの色リスト
        /// </summary>
        public enum Colors
        {
            Color1,
            Color2,
        }

        private void Awake()
        {
            Instance = this;
            CurrentColor = startColor;
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SetCurrentColor();
        }

        /// <summary>
        /// 色を反転させる
        /// </summary>
        public static void SwitchColor()
        {
            if (CurrentColor == Colors.Color1)
            {
                CurrentColor = Colors.Color2;
            }
            else
            {
                CurrentColor = Colors.Color1;
            }

            SetCurrentColor();
            onSwitchColor.Invoke(CurrentColor);
        }

        /// <summary>
        /// 現在の色をスプライトレンダラーに設定
        /// </summary>
        static void SetCurrentColor()
        {
            Instance.spriteRenderer.color 
                = Instance.colorList[(int)CurrentColor];
        }
    }
}
