namespace Ecrys.Utilities
{
	using UnityEngine;


	public class RotationResetter : MonoBehaviour
	{
		void LateUpdate()
		{
		    transform.rotation		= Quaternion.identity;
		}
	}
}