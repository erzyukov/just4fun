namespace Game.Units
{
	using Cysharp.Threading.Tasks;
	using UnityEngine;
	using UnityEngine.AI;
	using Zenject;

	public class UnitMove : IInitializable
	{
		[Inject] private NavMeshAgent _agent;

		public async void Initialize()
		{
			await UniTask.WaitForFixedUpdate();

			_agent.enabled = true;

			var startPos = _agent.transform.position;
			_agent.SetDestination( startPos + Vector3.back * 15 );
		}
	}
}
