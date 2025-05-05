namespace Game.Gameplay
{
	using Game.Buildings;
	using System.Collections.Generic;
	using System.Linq;
	using UnityEngine;
	using Zenject;

	public interface IBattleTargets
	{
		Vector3 GetTargetFor( ETeam team );
	}

	public class BattleTargets : IInitializable
	{
		[Inject] List<ICastleFacade> _casles;


		private Dictionary<ETeam, ETeam> _twoOpponentTeams = new();

		public void Initialize()
		{
			_twoOpponentTeams = new() {
				{ ETeam.TeamA, ETeam.TeamB },
				{ ETeam.TeamB, ETeam.TeamA },
			};
		}

		public Vector3 GetTargetFor( ETeam team )
		{
			var target = _casles.Where( c => c.Team == _twoOpponentTeams[ team ] ).FirstOrDefault();

			return target.Transform.position;
		}
	}
}
