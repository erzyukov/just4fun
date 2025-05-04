namespace Game.Level
{
	using Ecrys.Configs;
	using Game.Buildings;
	using R3;
	using System;
	using System.Collections.Generic;
	using Zenject;


	public class LevelFiller : IInitializable, IDisposable
	{
		[Inject] private List<IBuildingSpawnPlaceholder>	_placeholders;
		[Inject] private BarrackFacade.Factory				_barrackFactory;
		[Inject] private PrefabsConfig						_config;

		private CompositeDisposable _disposables = new();

		public void Initialize()
		{
			_placeholders.ForEach( InitBuilding );
		}

		public void Dispose() => _disposables.Dispose();

		private void InitBuilding( IBuildingSpawnPlaceholder placeholder )
		{
			switch ( placeholder.Building )
			{
				case EBuilding.Barrack:
					var building = _barrackFactory.Create( new() {
						Prefab		= _config.Buildings[ placeholder.Building ],
						Team		= placeholder.Team,
					} );
					building.Transform.position = placeholder.Position;
					break;

				default:
					break;
			}

			placeholder.Remove();
		}
	}
}
