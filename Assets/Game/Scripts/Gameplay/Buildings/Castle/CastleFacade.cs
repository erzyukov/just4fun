namespace Game.Buildings
{
	using Zenject;


	public interface ICastleFacade : IBuildingFacadeBase { }

	public class CastleFacade : BuildingFacadeBase, ICastleFacade
	{
		public class Factory : PlaceholderFactory<CastleFacadeFactory.Args, ICastleFacade> { }
	}
}
