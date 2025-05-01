namespace Ecrys.Utilities
{
    using System;
    using System.Threading;
    using UnityEngine;
    
    public class LifeTimeBehaviour :
        MonoBehaviour,
        ILifeTime
    {
        private readonly LifeTime _lifeTime = new();
        private readonly LifeTime _disableLifeTime = new();

        public ILifeTime AddCleanUpAction(Action cleanAction) => _lifeTime.AddCleanUpAction(cleanAction);

        public ILifeTime AddDispose(IDisposable item) => _lifeTime.AddDispose(item);

        public ILifeTime AddRef(object o) => _lifeTime.AddRef(o);

        public ILifeTime DisableLifeTime => _disableLifeTime;

        public bool IsTerminated => _lifeTime.IsTerminated;

        public CancellationToken Token => _lifeTime.Token;

        private void OnEnable()
        {
            _disableLifeTime.Release();
        }

        private void OnDisable()
        {
            _disableLifeTime.Release();
        }

        private void OnDestroy()
        {
            _lifeTime.Release();
            _disableLifeTime.Release();
        }
    }
}