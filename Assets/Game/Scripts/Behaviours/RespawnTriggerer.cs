using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class RespawnTriggerer : MonoBehaviour
    {
        private BoxCollider _boxCollider;
        private BoxCollider BoxCollider
        {
            get
            {
                if (_boxCollider == null)
                {
                    _boxCollider = gameObject.GetComponent<BoxCollider>();
                }
                return _boxCollider;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Wheel"))
            {
                Debug.Log("Respawn");
                var car = other.GetComponentInParent<CarBehaviour>();

                if (!car) return;

                car.Respawn();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 1, 0.5f);
            Gizmos.DrawCube(BoxCollider.bounds.center, BoxCollider.bounds.size);
        }
    }
}