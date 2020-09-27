using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Behaviours
{
    [RequireComponent(typeof(BoxCollider))]
    public class CoinBehaviour : MonoBehaviour
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

        private void Awake()
        {
            transform.DOLocalMove(transform.localPosition + (Vector3.up * 0.5f), 1f).SetLoops(-1, LoopType.Yoyo);
            transform.DORotate(new Vector3(0, 360, 0), 3, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Body"))
            {
                Debug.Log("Coin Collected");

                Collect();
            }
        }

        private void Collect()
        {
            transform.DOKill();
            transform.DOMoveY(transform.position.y + 2, 0.5f).SetEase(Ease.OutExpo).OnComplete(() => Destroy(gameObject));
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.92f, 0.016f, 0.5f);
            Gizmos.DrawCube(BoxCollider.bounds.center, BoxCollider.bounds.size);
        }
    }
}