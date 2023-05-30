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

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Vector2 v = walkSpeed * Input.GetAxisRaw("Horizontal") * Vector2.right;
            rb.velocity = v;
        }
    }
}