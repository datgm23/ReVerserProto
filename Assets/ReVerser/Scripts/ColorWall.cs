using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReVerser
{
    public class ColorWall : MonoBehaviour
    {
        [Tooltip("色の種類"), SerializeField]
        ColorBG.Colors myColor = ColorBG.Colors.Color1;

        void Start()
        {
        
        }
    }
}
