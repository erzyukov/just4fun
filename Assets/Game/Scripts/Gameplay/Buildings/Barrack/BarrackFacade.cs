namespace Game.Buildings
{
	using Game;
	using UnityEngine;
	using Zenject;


	public interface IBarrackFacade : IBuildingFacadeBase
	{
		ETeam Team { get; }
		Transform Transform { get; }
	}

	public class BarrackFacade : BuildingFacadeBase, IBarrackFacade
	{
		[Inject] private ETeam _team;

		public ETeam Team => _team;
		public Transform Transform => transform;


		public class Factory : PlaceholderFactory<BarrackFacadeFactory.Args, IBarrackFacade> { }
	}
}
