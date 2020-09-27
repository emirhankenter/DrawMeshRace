using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours.Obstacles
{
    public class MovingObstacle : MonoBehaviour
    {
        [SerializeField] private Vector3 _endPositionOffset;
        [SerializeField] private Ease _easeType;
        [SerializeField] private float _duration = 1f;
        private void Awake()
        {
            transform.DOLocalMove(transform.localPosition + _endPositionOffset, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(_easeType);
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + _endPositionOffset, 0.1f);

            Gizmos.DrawLine(transform.position, transform.position + _endPositionOffset);
        }
    }
}