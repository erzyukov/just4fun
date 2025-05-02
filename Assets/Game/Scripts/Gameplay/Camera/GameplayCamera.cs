namespace Game.Camera
{
	using Unity.Cinemachine;
	using UnityEngine;

	public interface IGameplayCamera
	{
		Transform Transform { get; }
		void SetPosition( Vector3 position );
	}

	public class GameplayCamera : MonoBehaviour, IGameplayCamera
	{

		[SerializeField] private CinemachineCamera _camera;

#region IGameplayCamera

		public Transform Transform => transform;

		public void SetPosition( Vector3 position )
		{
			_camera.transform.position = position;
		}

#endregion

	}
}
