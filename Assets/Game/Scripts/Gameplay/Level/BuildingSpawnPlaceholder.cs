namespace Game.Level
{
	using UnityEngine;


	public interface IBuildingSpawnPlaceholder
	{
		EBuilding Building { get; }
		ETeam Team { get; }
		Vector3 Position { get; }
		void Remove();
	}

	public class BuildingSpawnPlaceholder : MonoBehaviour, IBuildingSpawnPlaceholder
	{
		[SerializeField] private EBuilding		_building;
		[SerializeField] private ETeam			_team;

		public EBuilding Building		=> _building;

		public ETeam Team				=> _team;

		public Vector3 Position			=> transform.position;

		public void Remove()			=> GameObject.Destroy( gameObject );
	}

}
