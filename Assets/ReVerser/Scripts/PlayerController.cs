using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEngine;

namespace ReVerser
{
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("移動速度"), SerializeField]
        float walkSpeed = 4;
        [Tooltip("乗り越えや降りられる段差"), SerializeField]
        float canWalkStep = 0.4f;

        Rigidbody2D rb;
        CapsuleCollider2D capsuleCollider;
        ISwitch currentSwitch;

        /// <summary>
        /// プレイヤーの移動速度
        /// </summary>
        Vector2 velocity;

        /// <summary>
        /// 重力落下チェック時の接触フラグ
        /// </summary>
        ContactFilter2D gravityContactFilter;
        /// <summary>
        /// 接触チェック時の接触対象を受けとる配列
        /// </summary>
        RaycastHit2D[] results = new RaycastHit2D[8];

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            capsuleCollider = rb.GetComponent<CapsuleCollider2D>();
            velocity = Vector2.zero;

            // WallとSwitchレイヤーのみを対象にして、法線のフィルタを有効にする
            gravityContactFilter = new ContactFilter2D();
            gravityContactFilter.useLayerMask = true;
            gravityContactFilter.layerMask = LayerMask.GetMask("Wall", "Switch");
            gravityContactFilter.useNormalAngle = true;
        }

        /// <summary>
        /// 重力落下処理
        /// </summary>
        /// <returns>true=着地 / false=空中</returns>
        bool GravityFall()
        {
            // 重力加速
            velocity.y += Time.deltaTime * rb.gravityScale * Physics2D.gravity.y;
            // 今回の移動量を求める
            float moveY = Time.deltaTime * velocity.y - canWalkStep * rb.gravityScale;

            // 上下の接触判定
            int count = GravityCast(moveY);
            if (count == 0)
            {
                // 空中
                velocity.x = 0;
                return false;
            }

            // 頭をぶつけているか着地
            velocity.y = 0;
            AdjustMoveY(count, moveY);

            return true;
        }

        /// <summary>
        /// Y移動で頭の位置や足の位置がめり込まないように補正。
        /// </summary>
        /// <param name="count">接触数</param>
        void AdjustMoveY(int count, float moveY)
        {
            float adjustY = Mathf.Abs(moveY);

            for (int i = 0; i < count; i++)
            {
                adjustY = Mathf.Min(adjustY, results[i].distance);
                Debug.Log($"[{i}] deltaTime={Time.deltaTime} dist={results[i].distance} fraction={results[i].fraction} adust={adjustY} moveY={moveY}");
            }

            var p = rb.position;
            p.y += -Mathf.Sign(rb.gravityScale)* adjustY;
            rb.position = p;
        }

        /// <summary>
        /// 指定のYの移動距離を受けとって、接触判定をする。
        /// </summary>
        /// <param name="moveY">移動距離</param>
        /// <returns>接触数</returns>
        int GravityCast(float moveY)
        {
            if (moveY > 0)
            {
                // 上昇中は、下方向の面の対象
                gravityContactFilter.SetNormalAngle(270 - 89, 270 + 89);
            }
            else
            {
                // 下降中は、上方向の面の対象
                gravityContactFilter.SetNormalAngle(90 - 89, 90 + 89);
            }
            return rb.Cast(Vector2.up, gravityContactFilter, results, moveY);
        }

        void FixedUpdate()
        {
            if (GravityFall())
            {
                // 着地しているときのみ、左右操作可能
                velocity.x = walkSpeed * Input.GetAxisRaw("Horizontal");

                // スイッチ操作
                if (Input.GetButtonDown("Jump"))
                {
                    currentSwitch?.Action(this);
                }
            }

            rb.velocity = velocity;
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