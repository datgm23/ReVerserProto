using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReVerser
{
    /// <summary>
    /// 色反転スイッチの制御クラス
    /// </summary>
    public class ColorSwitch : MonoBehaviour, ISwitch
    {
        public void Action(PlayerController player)
        {
            Debug.Log($"Action({player})");
        }

        public void Activate()
        {
            Debug.Log("Activate()");
        }

        public void Deactivate()
        {
            Debug.Log("Deactivate()");
        }
    }
}