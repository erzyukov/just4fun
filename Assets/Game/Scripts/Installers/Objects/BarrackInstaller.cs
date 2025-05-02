namespace Game.Installers
{
	using Game.Buildings;
	using Game.Units;
	using Zenject;

	public class BarrackInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
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