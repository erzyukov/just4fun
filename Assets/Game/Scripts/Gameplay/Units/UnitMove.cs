namespace Game.Units
{
	using UnityEngine;
	using UnityEngine.AI;
	using Zenject;

	public class UnitMove : IInitializable
	{
		[Inject] private NavMeshAgent _agent;

		public void Initialize()
		{
			var startPos = _agent.transform.position;
			_agent.SetDestination( startPos + Vector3.back * 15 );
		}
	}
}
