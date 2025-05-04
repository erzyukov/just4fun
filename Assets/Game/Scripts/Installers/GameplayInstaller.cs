namespace Game.Installers
{
	using Ecrys.Configs;
	using Game.Buildings;
	using Game.Camera;
	using Game.Gameplay;
	using Game.Level;
	using Game.Units;
	using Zenject;


	public class GameplayInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			// GameplayCamera
			Container
				.BindInterfacesTo<GameplayCamera>()
				.FromComponentInHierarchy()
				.AsSingle();

			Install_Factories();

			// CastleFacade(s)
			//Container
				//Bind<ICastleFacade>()
				//.FromComponentsInHierarchy()
				//.AsSingle();

			// BattleTargets
			//Container
				//.BindInterfacesTo<BattleTargets>()
				//.AsSingle();

			// BuildingSpawnPlaceholder(s)
			Container
				.Bind<IBuildingSpawnPlaceholder>()
				.FromComponentsInHierarchy()
				.AsSingle();

			// LevelFiller
			Container
				.BindInterfacesTo<LevelFiller>()
				.AsSingle();
		}

		private void Install_Factories()
		{
			// UnitFacade.Factory
			Container
				.BindFactory<BarrackFacadeFactory.Args, IBarrackFacade, BarrackFacade.Factory>()
				.FromFactory<BarrackFacadeFactory>()
				.CopyIntoDirectSubContainers();
		}
	}
}