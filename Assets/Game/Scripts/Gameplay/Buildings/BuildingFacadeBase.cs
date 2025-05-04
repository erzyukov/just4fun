namespace Game.Buildings
{
	using UnityEngine;
	using Zenject;

	public interface IBuildingFacadeBase
	{
		ETeam Team { get; }
		Transform Transform { get; }
	}

	public class BuildingFacadeBase : MonoBehaviour, IBuildingFacadeBase
	{
		[Inject] private ETeam			_team;

		public ETeam Team				=> _team;
		public Transform Transform		=> transform;
	}
}
