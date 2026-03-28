using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class FocusCam : MonoBehaviour
    {
        public Friend friend;

        private void LateUpdate()
        {
            transform.position = friend.transform.position + new Vector3(0, 0, -10);
        }
    }
}