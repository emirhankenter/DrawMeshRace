using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class WheelBehaviour : MonoBehaviour
    {
        [SerializeField] private WheelCollider _wheelCollider;

        private void FixedUpdate()
        {
            _wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
            //_wheelCollider.transform.position = position;
            _wheelCollider.transform.rotation = rotation;
        }
    }
}