namespace Ecrys.Utilities
{
	using UnityEngine;
	using PrimeTween;

    public class Rotator : MonoBehaviour
    {
		[SerializeField] Vector3	_direction;
		[SerializeField] float		_rotatePerMinutes;

		Tween _tween;

		void Start()
		{
			float secondsInMinute	= 60;
			float duration			= secondsInMinute / _rotatePerMinutes;
			float fullAngle			= 360;

			_tween = Tween.EulerAngles( 
				transform, 
				Vector3.zero,
				_direction * fullAngle, 
				duration, 
				Ease.Linear, 
				-1, 
				CycleMode.Incremental );
		}

		void OnDestroy()	=> _tween.Stop();
	}
}
