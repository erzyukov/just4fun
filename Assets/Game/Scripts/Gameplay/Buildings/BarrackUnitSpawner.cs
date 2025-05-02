namespace Game.Buildings
{
	using R3;
	using System;
	using UnityEngine;
	using Zenject;

	public class BarrackUnitSpawner : IInitializable, IDisposable
	{
		[Inject] private IBuildingView _view;

		private const float StartSpawnDelay = 1;
		private const float SpawnDelay = 5;

		private CompositeDisposable _disposables = new();

		public void Initialize()
		{
			Observable.Timer( TimeSpan.FromSeconds( StartSpawnDelay ), TimeSpan.FromSeconds( SpawnDelay ) )
				.Subscribe( _ => SpawnUnit() )
				.AddTo( _disposables );
		}

		public void Dispose() => _disposables.Dispose();

		private void SpawnUnit()
		{
			Debug.LogWarning( $" >> Unit spawned at {_view.SpawnPoint.position}" );
		}
	}
}
