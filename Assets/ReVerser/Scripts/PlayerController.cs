using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReVerser
{
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("移動速度"), SerializeField]
        float walkSpeed = 4;

        Rigidbody2D rb;
        ISwitch currentSwitch;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Vector2 v = walkSpeed * Input.GetAxisRaw("Horizontal") * Vector2.right;
            rb.velocity = v;

            if (Input.GetButtonDown("Jump"))
            {
                currentSwitch?.Action(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // スイッチか
            if (collision.collider.CompareTag("Switch"))
            {
                OnSwitch(collision);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            // スイッチか
            if (collision.collider.CompareTag("Switch"))
            {
                OffSwitch(collision);
            }
        }

        /// <summary>
        /// スイッチに乗った時の処理
        /// </summary>
        /// <param name="col">接触時のCollision</param>
        void OnSwitch(Collision2D col)
        {
            var inssw = col.collider.GetComponent<ISwitch>();
            if (inssw == null) return;

            // 同じスイッチをすでに持っていたら何もしない
            if (inssw == currentSwitch) return;

            // 別のスイッチにすでに触れていた場合、前のスイッチは無効にする
            if (currentSwitch != null)
            {
                currentSwitch.Deactivate();
            }

            currentSwitch = inssw;
            currentSwitch.Activate();
        }

        void OffSwitch(Collision2D col)
        {
            currentSwitch?.Deactivate();
            currentSwitch = null;
        }
    }
}