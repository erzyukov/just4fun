﻿namespace Ecrys.Utilities
{
	using Cysharp.Threading.Tasks;
	using System;
	using System.Collections.Generic;
	using UnityEngine;


	public struct CreatePrefabPoolInput<T> where T : Enum
	{
		public T Type;
		public Transform PoolParent;
		public LifeTime LifeTime;
	}

	public interface IPrefabPoolFactory<TPrefab, TType> 
		where TPrefab : IPoolablePrefab where TType : Enum
	{
		UniTask<PrefabPool<TPrefab>> Create( CreatePrefabPoolInput<TType> input );
		void Destroy( TType type );
	}

	public abstract class PrefabPoolFactory<TPrefab, TType> : IPrefabPoolFactory<TPrefab, TType> 
		where TPrefab : IPoolablePrefab where TType : Enum
	{
		Dictionary<TType, PrefabPool<TPrefab>>		_pools			= new();

		async public UniTask<PrefabPool<TPrefab>> Create( CreatePrefabPoolInput<TType> input )
		{
			if (_pools.ContainsKey( input.Type ))
				return _pools[ input.Type ];

			var pool = await CreateNewPool( input );

			pool.WarmUp();

			_pools.Add( input.Type, pool );

			input.LifeTime.AddCleanUpAction( () => ReleasePool( input.Type ) );

			return pool;
		}

		protected abstract UniTask<PrefabPool<TPrefab>> CreateNewPool( CreatePrefabPoolInput<TType> input );

		public void Destroy( TType type )
		{
			ReleasePool( type );
		}

		void ReleasePool( TType type )
		{
			if (!_pools.ContainsKey( type ))
				return;

			_pools[ type ].Release();
			_pools.Remove( type );
		}
	}

}
