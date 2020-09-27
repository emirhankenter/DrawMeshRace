using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class ChestBehaviour : MonoBehaviour
    {
        public static event Action ChestClaimed;

        [SerializeField] private ParticleSystem _coinParticle;
        [SerializeField] private GameObject _top;

        private bool _isOpen;
        public void Open()
        {
            if (!_isOpen)
            {
                _top.transform.DORotate(new Vector3(90, 0, 0), 1f, RotateMode.LocalAxisAdd)
                    .OnComplete(() => 
                    {
                        _coinParticle.Play();
                        ChestClaimed?.Invoke();
                    });
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
                gameObject.GetComponent<Collider>().enabled = false;

                transform.DORotate(new Vector3(45, 0, 0), 0.2f, RotateMode.LocalAxisAdd).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutCirc);
                Open();
            }
        }
    }
}