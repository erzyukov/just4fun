﻿namespace Ecrys.Utilities
{
	using R3;
	using UnityEngine;
	using UnityEngine.Pool;

	public interface IPoolablePrefab
	{
		ReactiveCommand<Unit> Releasing { get; }
		ReactiveCommand<Unit> Reinitializing { get; }
		Transform Transform { get; }
		bool IsReleased { get; }
		void Initialize<T>( IObjectPool<T> pool ) where T : class;
		void Reinitialize();
		void Release( bool isSilently = false );
	}
}
