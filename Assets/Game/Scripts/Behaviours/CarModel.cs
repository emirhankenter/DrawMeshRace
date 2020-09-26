using ProBuilder2.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class CarModel : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private MeshCollider _meshCollider;

        private pb_Object _pbObject;
        private pb_BezierShape _pbBezierShape;
        private pb_Entity _pbEntity;

        public void Initialize()
        {
            _meshFilter = gameObject.GetComponent<MeshFilter>();
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
            _meshCollider = gameObject.GetComponent<MeshCollider>();

            _pbObject = gameObject.AddComponent<pb_Object>();
            _pbBezierShape = gameObject.AddComponent<pb_BezierShape>();
            _pbEntity = gameObject.AddComponent<pb_Entity>();
        }

        public void CreateMesh(List<Vector2> points)
        {
            var bezierPoints = BeizerListCreate(points);
            _pbBezierShape.m_Points = bezierPoints;
            _pbBezierShape.m_Radius = 25;
            _pbBezierShape.Refresh();
            _pbObject.ToMesh();

            Mesh sharedMesh = _meshFilter.sharedMesh;
            _meshCollider.sharedMesh = sharedMesh;
            _meshCollider.convex = true;
        }

        private List<pb_BezierPoint> BeizerListCreate(List<Vector2> points)
        {
            List<pb_BezierPoint> beizerPoints = new List<pb_BezierPoint>();
            for (int i = 0; i < points.Count; i++)
            {
                beizerPoints.Add(new pb_BezierPoint(points[i], points[i], points[i], Quaternion.identity));
            }
            return beizerPoints;
        }
    }
}