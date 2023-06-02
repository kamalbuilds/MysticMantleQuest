using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGM.Gameplay
{
    /// <summary>
    /// A simple camera follower class. It saves the offset from the
    ///  focus position when started, and preserves that offset when following the focus.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        public Transform focus;

        public float smoothTime = 1;

        Vector3 offset;

        void Awake()
        {
            offset = focus.position - transform.position;
        }

        void Update()
        {
            var targetPosition = focus.position - offset;
            transform.position = targetPosition;
        }
    }
}
