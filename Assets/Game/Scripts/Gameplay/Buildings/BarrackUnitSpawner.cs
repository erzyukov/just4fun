namespace Game.Buildings
{
	using Ecrys.Configs;
	using Game.Units;
	using R3;
	using System;
	using Zenject;


	public class BarrackUnitSpawner : IInitializable, IDisposable
	{
		[Inject] private IBuildingView				_view;
		[Inject] private UnitFacade.Factory			_unitFacadeFactory;
		[Inject] private PrefabsConfig				_config;


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
			var type	= EUnit.Warrior;

			var unit	= _unitFacadeFactory.Create( new() {
				Prefab	= _config.Units[ type ],
				Type	= type,
			} );

			unit.SetPosition( _view.SpawnPoint.position );
		}
	}
}
