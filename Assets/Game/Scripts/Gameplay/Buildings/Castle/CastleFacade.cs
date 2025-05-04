namespace Game.Buildings
{
	using UnityEngine;
	using Zenject;


	public interface ICastleFacade
	{
		ETeam Team { get; }
		Transform Transform { get; }
	}

	public class CastleFacade : MonoBehaviour, ICastleFacade
	{
		[SerializeField] private ETeam _team;

		public ETeam Team => _team;

		public Transform Transform => transform;
	}
}
