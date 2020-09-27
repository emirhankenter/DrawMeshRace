using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public delegate void CheckPointDelegate(Vector3 checkpointPosition);
    public class CheckPointTriggerer : MonoBehaviour
    {
        public static event CheckPointDelegate CheckPointTriggered;

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
            if (other.CompareTag("Body"))
            {
                Debug.Log("Boost");
                var car = other.GetComponentInParent<CarBehaviour>();

                CheckPointTriggered?.Invoke(transform.position);

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