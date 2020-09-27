using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class BoostTriggerer : MonoBehaviour
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wheel"))
            {
                var rb = other.GetComponentInParent<Rigidbody>();
                var car = other.GetComponentInParent<CarBehaviour>();

                if (!rb && !car) return;

                car.Boost();
                Debug.Log("Boosted");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 1, 0.5f);
            Gizmos.DrawCube(BoxCollider.bounds.center, BoxCollider.bounds.size);
        }
    }
}