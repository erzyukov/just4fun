namespace Game.Core
{
	using UnityEngine;
	using R3;

	public class GameplayModel
	{
		public ReactiveProperty<Vector2> TouchDelta { get; } = new();

	}
}