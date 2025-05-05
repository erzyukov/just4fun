namespace Game.Buildings
{
	using Zenject;


	public interface IBarrackFacade : IBuildingFacadeBase { }

	public class BarrackFacade : BuildingFacadeBase, IBarrackFacade
	{

		public class Factory : PlaceholderFactory<BarrackFacadeFactory.Args, IBarrackFacade> { }
	}
}
