namespace Ecrys.Utilities
{
    using System;
    using R3;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public static class Lifetime_Extensions
    {

        public static ILifeTime LogOnRelease(this ILifeTime lifeTime,string message)
        {
            lifeTime.AddCleanUpAction(() => Debug.Log(message));
            return lifeTime;
        }
    
        public static ILifeTime DestroyWith(this ILifeTime lifeTime, GameObject gameObject)
        {
            if (!gameObject) return lifeTime;
            DestroyWith(gameObject,lifeTime);
            return lifeTime;
        }
 
        public static ILifeTime BindEvent<TArg,TArg2>(this ILifeTime lifeTime,Action<TArg,TArg2> source,Action<TArg,TArg2> listener)
        {
            if (source == null || listener == null || lifeTime.IsTerminated) return lifeTime;
        
            Observable.FromEvent(x => source+=listener,
                    x => source-=listener)
                .Subscribe()
                .AddTo(lifeTime);
        
            return lifeTime;
        }
    
        public static ILifeTime BindEvent<TArg>(this ILifeTime lifeTime,Action<TArg> source,Action<TArg> listener)
        {
            if (source == null || listener == null || lifeTime.IsTerminated) return lifeTime;
        
            Observable.FromEvent(x => source+=listener,
                    x => source-=listener)
                .Subscribe()
                .AddTo(lifeTime);
        
            return lifeTime;
        }
    
        public static ILifeTime BindEvent(this ILifeTime lifeTime,Action source,Action listener)
        {
            if (source == null || listener == null || lifeTime.IsTerminated) return lifeTime;
        
            Observable.FromEvent(x => source+=listener,
                    x => source-=listener)
                .Subscribe()
                .AddTo(lifeTime);
        
            return lifeTime;
        }
    
        public static ILifeTime AddTo(this ILifeTime lifeTime,Action action,Action cancellationAction)
        {
            if (lifeTime.IsTerminated) return lifeTime;
        
            action?.Invoke();
            lifeTime.AddCleanUpAction(cancellationAction);
            return lifeTime;
        }
    
        public static T AddTo<T>(this T disposable, ILifeTime lifeTime)
            where T : IDisposable
        {
            if (disposable != null)
                lifeTime.AddDispose(disposable);
            return disposable;
        }

        public static T DestroyWith<T>(this T asset, ILifeTime lifeTime)
            where T : Object
        {
            if (asset == null) return asset;
        
            switch (asset)
            {
                case Component component:
                {
                    DestroyComponentWith(component, lifeTime);
                    break;
                }
                case GameObject gameObject:
                {
                    DestroyObjectWith(gameObject, lifeTime);
                    break;
                }
                default:
                {
                    DestroyAssetWith(asset, lifeTime);
                    break;
                }
            }

            return asset;
        }
    
        public static Component DestroyComponentWith(Component asset, ILifeTime lifeTime)
        {
            if (!asset) return asset;
            DestroyObjectWith(asset.gameObject,lifeTime);
            return asset;
        }
    
        public static Object DestroyAssetWith(Object asset, ILifeTime lifeTime)
        {
            if (!asset) return asset;
            lifeTime.AddCleanUpAction(() => CheckDestroy(asset));
            return asset;
        }

        public static GameObject DestroyObjectWith(GameObject gameObject, ILifeTime lifeTime)
        {
            if (!gameObject) return gameObject;
            lifeTime.AddCleanUpAction(() => CheckDestroy(gameObject));
            return gameObject;
        }

        public static void CheckDestroy(Object asset)
        {
            if (asset == null) return;
            Object.Destroy(asset);
        }
    
        public static ILifeTime DestroyWith(this ILifeTime lifeTime, Component component)
        {
            if (!component) return lifeTime;
            lifeTime.AddCleanUpAction(() =>
            {
                if (component == null) return;
                var gameObject = component.gameObject;
                Object.Destroy(gameObject);
            });
            return lifeTime;
        }

        public static bool IsTerminatedLifeTime(this ILifeTime lifeTime)
        {
            return lifeTime == null || lifeTime.IsTerminated || lifeTime == LifeTime.TerminatedLifetime;
        }

        public static ILifeTime GetLifeTime(this object source)
        {
            if (source == null)
                return LifeTime.TerminatedLifetime;
        
            switch (source)
            {
                case ILifeTime lifeTime:
                    return lifeTime;
                case Component component:
                    return component.GetAssetLifeTime();
                case GameObject gameObject:
                    return gameObject.GetAssetLifeTime();
            }
        
            return LifeTime.TerminatedLifetime;
        }
    
        public static ILifeTime GetAssetLifeTime(this GameObject gameObject, bool terminateOnDisable = false)
        {
            var lifetimeComponent = gameObject.GetComponent<LifeTimeBehaviour>(); 

            lifetimeComponent = lifetimeComponent != null
                ? lifetimeComponent
                : gameObject.AddComponent<LifeTimeBehaviour>();
            
            return terminateOnDisable 
                ? lifetimeComponent.DisableLifeTime 
                : lifetimeComponent;
        }

        public static ILifeTime DestroyOnCleanup(this LifeTime lifeTime, GameObject gameObject)
        {
            lifeTime.AddCleanUpAction(() =>
            {
                if (gameObject)
                    UnityEngine.Object.Destroy(gameObject);
            });
            return lifeTime;
        }
        
        public static ILifeTime DestroyOnCleanup(this LifeTime lifeTime, Component component, bool onlyComponent = false)
        {
            if (!onlyComponent)
            {
                return lifeTime.DestroyOnCleanup(component.gameObject);
            }
            
            lifeTime.AddCleanUpAction(() =>
            {
                if (component)
                    UnityEngine.Object.Destroy(component);
            });
            
            return lifeTime;
        }

        public static ILifeTime GetAssetLifeTime(this Component component, bool terminateOnDisable = false)
        {
            return component.gameObject.GetAssetLifeTime(terminateOnDisable);
        }
        
		public static ILifeTime AddTo( this LifeTime lifeTime, ILifeTime parentLifeTime )
		{
			parentLifeTime.AddCleanUpAction( () => lifeTime.Release() );

			return lifeTime;
		}

        public static ILifeTime AddTo(this GameObject gameObject, IDisposable disposable)
        {
            return gameObject.GetAssetLifeTime().AddDispose(disposable);
        }
        
        public static ILifeTime AddCleanUp(this GameObject gameObject, Action cleanupAction)
        {
            return gameObject.GetAssetLifeTime().AddCleanUpAction(cleanupAction);
        }
        
        public static ILifeTime AddTo(this Component component, IDisposable disposable) => AddTo(component.gameObject, disposable);
        
        public static ILifeTime AddTo(this Component component, Action action) =>AddCleanUp(component.gameObject, action);
        
        
        public static ILifeTime AddCleanUp(this Component component, Action cleanupAction) => AddCleanUp(component.gameObject, cleanupAction);

    }
}