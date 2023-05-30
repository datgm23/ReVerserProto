using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReVerser
{
    /// <summary>
    /// スイッチの実装
    /// </summary>
    public interface ISwitch
    {
        /// <summary>
        /// プレイヤーがスイッチ操作をした時に呼び出す
        /// </summary>
        /// <param name="player">プレイヤーのインスタンス</param>
        void Action(PlayerController player);

        /// <summary>
        /// プレイヤーがスイッチに乗った時に呼び出し
        /// </summary>
        void Activate();

        /// <summary>
        /// プレイヤーがスイッチから離れたときに呼び出し
        /// </summary>
        void Deactivate();
    }
}