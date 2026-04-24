using Scellecs.Morpeh;
using UnityEngine;

namespace Project.Code.Gameplay.Components
{
    public struct CameraFollowComponent : IComponent
    { 
        public Transform CameraTransform;
        public Vector3 Offset;
        public float Pitch;
        public float PositionSmoothness;
        public float RotationSmoothness;
    }
}