using Project.Code.Gameplay.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Project.Code.Gameplay.Providers
{
    public class CameraProvider : EntityProvider
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector3 _offset = new(0f, 3f, -5f);
        [SerializeField] private float _smoothTime = 0.15f;

        protected override void Initialize()
        {
            ref var cameraFollow = ref World.Default
                .GetStash<CameraFollowComponent>()
                .Add(Entity);
            
            cameraFollow.CameraTransform = _camera.transform;
            cameraFollow.Offset = _offset;
            cameraFollow.SmoothTime = _smoothTime;
        }
    }
}