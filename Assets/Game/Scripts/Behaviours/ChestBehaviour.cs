using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class ChestBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject _top;

        private bool _isOpen;
        public void Open()
        {
            if (!_isOpen)
            {
                _top.transform.DORotate(new Vector3(90, 0, 0), 1f, RotateMode.LocalAxisAdd);
                _isOpen = true;
            }
        }

        public void Close()
        {
            if (_isOpen)
            {
                _top.transform.DORotate(new Vector3(-90, 0, 0), 1f, RotateMode.LocalAxisAdd);
                _isOpen = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Body"))
            {
                var rb = collision.gameObject.GetComponentInParent<Rigidbody>();
                rb.isKinematic = true;
                Open();
            }
        }
    }
}