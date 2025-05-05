namespace Game.Installers
{
	using Ecrys.Configs;
	using Game.Buildings;
	using Game.Camera;
	using Game.Core;
	using Game.Gameplay;
	using Game.Inputs;
	using Game.Level;
	using Game.Units;
	using UnityEngine;
	using Zenject;


	public class GameplayInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Debug.Log("[GameplayInstaller] InstallBindings ");

			// GameplayModel
			Container
				.Bind<GameplayModel>()
				.AsSingle();

			// GameplayCamera
			Container
				.BindInterfacesTo<GameplayCamera>()
				.FromComponentInHierarchy()
				.AsSingle();

			// DragCameraController
			Container
				.BindInterfacesTo<DragCameraController>()
				.AsSingle();

			// TouchHandle
			Container
				.BindInterfacesTo<TouchHandle>()
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
			// BarrackFacade.Factory
			Container
				.BindFactory<BarrackFacadeFactory.Args, IBarrackFacade, BarrackFacade.Factory>()
				.FromFactory<BarrackFacadeFactory>()
				.CopyIntoDirectSubContainers();

			// CastleFacade.Factory
			Container
				.BindFactory<CastleFacadeFactory.Args, ICastleFacade, CastleFacade.Factory>()
				.FromFactory<CastleFacadeFactory>()
				.CopyIntoDirectSubContainers();
		}
	}
}