﻿namespace Ecrys.Utilities
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Pool;


	public class PrefabPool< T > where T : IPoolablePrefab
	{
		protected ObjectPool<IPoolablePrefab>	Pool;
		
		protected T		Prefab;

		Transform		_parent;
		int				_defaultCapacity;
		bool			_isDisposed;

		public PrefabPool( T prefab, Transform parent, int defaultCapacity, int maxSize )
		{
			Prefab				= prefab;
			_parent				= parent;
			_defaultCapacity	= defaultCapacity;
			_isDisposed			= false;

			Pool	= new ObjectPool<IPoolablePrefab>( CreateElement, OnTakeFromPool, OnReturnToPool, OnDestroy, true, defaultCapacity, maxSize );
		}

#region PrefabPool

		public Transform Parent => _parent;

		public T GetElement()
		{
			return (T) Pool.Get();
		}

		public void WarmUp()
		{
			if (Pool.CountAll >= _defaultCapacity)
				return;

			List<IPoolablePrefab> elements = new();

            for (int i = 0; i < _defaultCapacity; i++)
			{
				elements.Add( Pool.Get() );
				elements[ elements.Count - 1 ].Reinitialize();
			}

            for (int i = 0; i < _defaultCapacity; i++)
				elements[i].Release( true );

			elements.Clear();
		}

		public void Release()
		{
			_isDisposed = true;
			Pool.Dispose();
			GameObject.Destroy( _parent.gameObject );
		}
#endregion

#region ObjectPool

		protected virtual IPoolablePrefab CreateElement()
		{
			IPoolablePrefab element	= GameObject.Instantiate( Prefab.Transform, _parent ).GetComponent<IPoolablePrefab>();

			element.Initialize( Pool );

			return element;
		}

		void OnTakeFromPool( IPoolablePrefab element )
		{
			element.Transform.gameObject.SetActive( true );
		}

		void OnReturnToPool( IPoolablePrefab element )
		{
			if (_isDisposed)
			{
				GameObject.Destroy( element.Transform.gameObject );
			}
			else if (element is not null)
			{
				element.Transform.gameObject.SetActive( false );
				element.Transform.SetParent( _parent );
			}
		}

		void OnDestroy( IPoolablePrefab element )
		{
			if (!element.IsReleased)
				GameObject.Destroy( element.Transform.gameObject );
		}

#endregion

	}
}
