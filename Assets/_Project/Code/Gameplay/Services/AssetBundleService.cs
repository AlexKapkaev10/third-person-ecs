using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Project.Code.Core.Services
{
    public interface IAssetBundleService : IDisposable
    {
        UniTask<T> LoadAsync<T>(string key) where T : Object;
        UniTask<GameObject> InstantiateAsync(string key, Transform parent = null);
        void Release(string key);
        void ReleaseInstance(GameObject instance);
    }

    public class AssetBundleService : IAssetBundleService
    {
        private readonly Dictionary<string, AsyncOperationHandle> _assetHandles = new();
        private readonly Dictionary<GameObject, string> _instances = new();

        public async UniTask<T> LoadAsync<T>(string key) where T : Object
        {
            if (_assetHandles.TryGetValue(key, out var cached))
            {
                return (T)cached.Result;
            }

            var handle = Addressables.LoadAssetAsync<T>(key);
            await handle.ToUniTask();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(handle);
                throw new InvalidOperationException(
                    $"Failed to load asset '{key}': {handle.OperationException?.Message}");
            }

            _assetHandles[key] = handle;
            return handle.Result;
        }

        public async UniTask<GameObject> InstantiateAsync(string key, Transform parent = null)
        {
            var handle = Addressables.InstantiateAsync(key, parent);
            await handle.ToUniTask();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Addressables.Release(handle);
                throw new InvalidOperationException(
                    $"Failed to instantiate '{key}': {handle.OperationException?.Message}");
            }

            var instance = handle.Result;
            _instances[instance] = key;
            return instance;
        }

        public void Release(string key)
        {
            if (!_assetHandles.Remove(key, out var handle))
            {
                return;
            }

            Addressables.Release(handle);
        }

        public void ReleaseInstance(GameObject instance)
        {
            if (!_instances.Remove(instance))
            {
                return;
            }

            Addressables.ReleaseInstance(instance);
        }

        public void Dispose()
        {
            foreach (var instance in _instances.Keys)
            {
                if (instance != null)
                {
                    Addressables.ReleaseInstance(instance);
                }
            }
            _instances.Clear();

            foreach (var handle in _assetHandles.Values)
            {
                Addressables.Release(handle);
            }
            _assetHandles.Clear();
        }
    }
}
