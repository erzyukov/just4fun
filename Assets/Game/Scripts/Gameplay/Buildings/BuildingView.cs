namespace Game.Buildings
{
	using UnityEngine;


	public interface IBuildingView
	{
		Transform SpawnPoint { get; }
	}

	public class BuildingView : MonoBehaviour, IBuildingView
	{
		[SerializeField] private Transform _spawnPoint;

		public Transform SpawnPoint => _spawnPoint;
	}
}
