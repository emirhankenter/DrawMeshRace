using ProBuilder2.Common;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Extensions;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class CarModel : MonoBehaviour
    {
        [SerializeField] private Transform _follow;

        [SerializeField] private Transform _frontWheels;
        [SerializeField] private Transform _backWheels;

        [SerializeField] private Material _bodyMaterial;

        private List<Vector2> _controlPoints;

        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private MeshCollider _meshCollider;

        private pb_Object _pbObject;
        private pb_BezierShape _pbBezierShape;
        private pb_Entity _pbEntity;

        private void Awake()
        {
            _meshFilter = gameObject.GetComponent<MeshFilter>();
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
            _meshCollider = gameObject.GetComponent<MeshCollider>();

            _pbObject = gameObject.AddComponent<pb_Object>();
            _pbEntity = gameObject.AddComponent<pb_Entity>();
            _pbBezierShape = gameObject.AddComponent<pb_BezierShape>();
        }

        public void CreateMesh(List<Vector2> points)
        {
            points = points.Inverse().Scale(2f);
            _controlPoints = points;

            var bezierPoints = CreateBezierList(points);
            _pbBezierShape.m_Points = bezierPoints;
            _pbBezierShape.m_Radius = 25;
            _pbBezierShape.Refresh();
            _pbObject.ToMesh();

            Mesh sharedMesh = _meshFilter.sharedMesh;
            _meshCollider.sharedMesh = sharedMesh;
            _meshCollider.convex = true;
            _meshRenderer.material = _bodyMaterial;
        }

        private List<pb_BezierPoint> CreateBezierList(List<Vector2> points)
        {
            List<pb_BezierPoint> beizerPoints = new List<pb_BezierPoint>();
            for (int i = 0; i < points.Count; i++)
            {
                beizerPoints.Add(new pb_BezierPoint(points[i], points[i], points[i], Quaternion.identity));
            }
            return beizerPoints;
        }

        public void PlaceWheels()
        {
            var back = _controlPoints.First();
            var front = _controlPoints.Last();

            _backWheels.transform.localPosition = new Vector3(0, front.y, front.x) * transform.lossyScale.x;
            _frontWheels.transform.localPosition = new Vector3(0, back.y, back.x) * transform.lossyScale.x;

            var forward = (_frontWheels.transform.localPosition - _backWheels.transform.localPosition).normalized * -1;

            transform.parent.forward = forward;
            _backWheels.parent.forward = forward;
            _frontWheels.parent.forward = forward;
        }
    }
}