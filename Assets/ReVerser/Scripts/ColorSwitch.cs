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
        [Tooltip("スイッチを押してから有効になるまでの秒数"), SerializeField]
        float reactivateSeconds = 1f;

        Animator anim;

        /// <summary>
        /// スイッチの無効な残り秒数
        /// </summary>
        float waitTime;

        /// <summary>
        /// 自分が有効な時、true
        /// </summary>
        bool isActive;

        void Awake()
        {
            anim = GetComponent<Animator>();
            anim.SetBool("IsActivate", false);
        }

        void Update()
        {
            if (isActive && (waitTime > 0f))
            {
                waitTime -= Time.deltaTime;

                if (waitTime <= 0f)
                {
                    anim.SetBool("IsActivate", true);
                }
            }
        }

        public void Action(PlayerController player)
        {
            // 待ち時間中のため、処理無効
            if (waitTime > 0) return;

            waitTime = reactivateSeconds;
            anim.SetBool("IsActivate", false);
            ColorBG.SwitchColor();
        }

        public void Activate()
        {
            anim.SetBool("IsActivate", true);
            isActive = true;
        }

        public void Deactivate()
        {
            anim.SetBool("IsActivate", false);
            isActive = false;
        }
    }
}