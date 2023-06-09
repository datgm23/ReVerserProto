using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReVerser
{
    /// <summary>
    /// 仮の重力制御用スクリプト。
    /// 開発時はなくてよい
    /// </summary>
    public class GravityBehaviour : MonoBehaviour
    {
#if UNITY_EDITOR
        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                rb.gravityScale = -rb.gravityScale;
            }
        }
#endif
    }
}
