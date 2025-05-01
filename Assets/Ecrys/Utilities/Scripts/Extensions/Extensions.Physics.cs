namespace Ecrys.Utilities
{
	using UnityEngine;

	public static class Physics_Extensions
	{

		// https://stackoverflow.com/questions/34250868/unity-addexplosionforce-to-2d
		public static void AddExplosionForce( this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force )
		{
			var explosionDir		= rb.position - explosionPosition;
			var explosionDistance	= (explosionRadius != 0) ? explosionDir.magnitude / explosionRadius : 0;

			// Normalize without computing magnitude again
			if (upwardsModifier == 0)
			{
				explosionDir	= (explosionDistance != 0) ? explosionDir / explosionDistance : explosionDir.normalized;
			}
			else
			{
				// From Rigidbody.AddExplosionForce doc:
				// If you pass a non-zero value for the upwardsModifier parameter, the direction
				// will be modified by subtracting that value from the Y component of the centre point.
				explosionDir.y	+= upwardsModifier;
				explosionDir.Normalize();
			}

			rb.AddForce( Mathf.Lerp( 0, explosionForce, (1 - explosionDistance) ) * explosionDir, mode );
		}
	}
}