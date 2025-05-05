namespace Game.Installers
{
	using Game.Buildings;
	using Game.Units;
	using UnityEngine;
	using Zenject;


	public class BarrackInstaller : MonoInstaller
	{
		//[Inject] private ETeam _team;

		public override void InstallBindings()
		{
			//Debug.LogWarning(_team);

			// BarrackFacade
			Container
				.BindInterfacesAndSelfTo<BarrackFacade>()
				.FromComponentOnRoot()
				.AsSingle();

			// BuildingView
			Container
				.BindInterfacesTo<BuildingView>()
				.FromComponentInHierarchy()
				.AsSingle();

			// BarrackUnitSpawner
			Container
				.BindInterfacesTo<BarrackUnitSpawner>()
				.AsSingle();

			Install_Factories();
		}

		private void Install_Factories()
		{
			// UnitFacade.Factory
			Container
				.BindFactory<UnitFacadeFactory.Args, IUnitFacade, UnitFacade.Factory>()
				.FromFactory<UnitFacadeFactory>()
				.CopyIntoDirectSubContainers();
		}
	}
}