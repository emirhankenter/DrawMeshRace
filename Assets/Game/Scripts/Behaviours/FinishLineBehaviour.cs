using Assets.Game.Scripts.Behaviours;
using Assets.Game.Scripts.Controllers;
using Mek.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public delegate void FinishDelegate();
    public class FinishLineBehaviour : MonoBehaviour
    {
        public static event FinishDelegate FinishLinePassed;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wheel"))
            {
                var car = other.GetComponentInParent<CarBehaviour>();

                FinishLinePassed?.Invoke();

                if (!car) return;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 1, 0.5f);
            Gizmos.DrawCube(BoxCollider.bounds.center, BoxCollider.bounds.size);
        }
    }
}