namespace Game.Units
{
	using UnityEngine;
	using Zenject;

	public interface IUnitFacade
	{
		void SetPosition( Vector3 value );
	}

	public class UnitFacade : MonoBehaviour, IUnitFacade
	{
		public void SetPosition( Vector3 value )
		{
			transform.position = value;
		}

		public class Factory : PlaceholderFactory<UnitFacadeFactory.Args, IUnitFacade> { }
	}
}
