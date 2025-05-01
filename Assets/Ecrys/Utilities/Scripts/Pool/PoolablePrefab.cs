﻿namespace Ecrys.Utilities
{
	using R3;
	using UnityEngine;
	using UnityEngine.Pool;


	public abstract class PoolablePrefab : MonoBehaviour, IPoolablePrefab
	{
		IObjectPool<IPoolablePrefab>	_pool;

		bool		_isSpawned;

		private void OnDestroy()
		{
			_isSpawned = false;
			IsReleased = true;
		}

#region IPooledPrefab

		public Transform Transform		=> transform;

		public bool IsReleased { get; private set; }

		public ReactiveCommand<Unit> Releasing { get; } = new();
		public ReactiveCommand<Unit> Reinitializing { get; } = new();

		public virtual void Initialize<T>( IObjectPool<T> pool ) where T : class
		{
			_pool	= (IObjectPool<IPoolablePrefab>) pool;
		}

		// TODO: [refact] rename
		public virtual void Reinitialize()
		{
			IsReleased = false;
			_isSpawned = true;
			Reinitializing.Execute( default );
		}

		public virtual void Release( bool isSilently = false )
		{
			IsReleased = true;

			if (_isSpawned)
			{
				if (!isSilently)
					Releasing.Execute( default );

				_pool?.Release( this );
			}
			
			_isSpawned = false;
		}

#endregion

	}
}
