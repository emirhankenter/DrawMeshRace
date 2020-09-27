using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Behaviours
{
    public class RoadBehaviour : MonoBehaviour
    {
        [SerializeField] private RoadMeshCreator _roadMeshCreator;
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshCollider _meshCollider;

        public void UpdatePath()
        {
            _roadMeshCreator.UpdatePath();
            _meshCollider.sharedMesh = _meshFilter.sharedMesh;
        }
    }
}