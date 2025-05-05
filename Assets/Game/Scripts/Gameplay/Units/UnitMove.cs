namespace Game.Units
{
	using Cysharp.Threading.Tasks;
	using Ecrys.Configs;
	using Game.Camera;
	using Game.Gameplay;
	using UnityEngine;
	using UnityEngine.AI;
	using Zenject;

	public class UnitMove : IInitializable
	{
		[Inject] private ETeam				_team;
		[Inject] private NavMeshAgent		_agent;
		[Inject] private PrefabsConfig		_config;
		//[Inject] private IBattleTargets		_targets;
		[Inject] private IGameplayCamera		_targets;

		public async void Initialize()
		{
			await UniTask.WaitForFixedUpdate();

			_agent.enabled = true;

			//var target = _targets.GetTargetFor( _team );

			//_agent.SetDestination( target );
		}
	}
}
