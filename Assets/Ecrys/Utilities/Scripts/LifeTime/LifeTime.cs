namespace Ecrys.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using UnityEngine;

    public class LifeTime : ILifeTime
    {
        private static readonly LifeTime _editorLifeTime = new();
        private static int _uniqueId;
        
        public static readonly ILifeTime TerminatedLifetime;
        public static ILifeTime EditorLifeTime => _editorLifeTime;

        private List<IDisposable> disposables = new();
        private List<object> referencies = new();
        private List<Action> cleanupActions = new();
        private CancellationTokenSource _cancellationTokenSource;
        
        public readonly int id;
        
        public bool isTerminated;
        
        #region static data
        
        static LifeTime()
        {
            var completedLifetime = new LifeTime();
            completedLifetime.Release();
            TerminatedLifetime = completedLifetime;
        }

        public static LifeTime Create()
        {
            return new LifeTime();
        }
        
        #endregion
        
        public LifeTime()
        {
            id = _uniqueId++;
        }
        
        /// <summary>
        /// is lifetime terminated
        /// </summary>
        public bool IsTerminated => isTerminated;

        public CancellationToken Token
        {
            get
            {
                if (isTerminated)
                    return new CancellationToken(true);
                _cancellationTokenSource ??= new CancellationTokenSource();
                return _cancellationTokenSource.Token;
            }
        }
        
        /// <summary>
        /// cleanup action, call when life time terminated
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILifeTime AddCleanUpAction(Action cleanAction) 
        {
            if (cleanAction == null)
                return this;
            
            //call cleanup immediate. lite time already ended
            if (isTerminated) {
                cleanAction?.Invoke();
                return this;
            }
            cleanupActions.Add(cleanAction);
            return this;
        }
    
        /// <summary>
        /// add child disposable object
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILifeTime AddDispose(IDisposable item)
        {
            if (item == null) return this;
            
            if (isTerminated) {
                item.Dispose();
                return this;
            }
            
            disposables.Add(item);
            return this;
        }

        /// <summary>
        /// save object from GC
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILifeTime AddRef(object o)
        {
            if (isTerminated)
                return this;
            referencies.Add(o);
            return this;
        }

        /// <summary>
        /// restart lifetime
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Restart()
        {
            Release();
            isTerminated = false;
        }
        
        /// <summary>
        /// invoke all cleanup actions
        /// </summary>
        public void Release()
        {
            if (isTerminated)
                return;
            
            isTerminated = true;
            
            for (var i = cleanupActions.Count-1; i >= 0; i--)
            {
                try
                {
                    cleanupActions[i]?.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            
            for (var i = disposables.Count-1; i >= 0; i--) 
            {
                try
                {
                    disposables[i].Dispose();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            cleanupActions.Clear();
            disposables.Clear();
            referencies.Clear();

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        #region type convertion

        public static implicit operator CancellationToken(LifeTime lifeTime) => lifeTime.Token;

        #endregion

#if UNITY_EDITOR

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void OnDomainReload()
        {
            Application.quitting -= OnApplicationQuitting;
            Application.quitting += OnApplicationQuitting;
            _editorLifeTime?.Release();
        }
        
        public static void OnApplicationQuitting()
        {
            Application.quitting -= OnApplicationQuitting;
            _editorLifeTime?.Release();
        }
        
#endif
    }
}