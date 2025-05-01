namespace Ecrys.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Runtime.Serialization.Formatters.Binary;
	using UnityEngine;
	using Random = UnityEngine.Random;


	// https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Math/Mathf.cs


	public static partial class Utils
	{
#region Math

		// Calculates the ::ref::Lerp parameter between of two values.
		public static double InverseLerp( double a, double b, double value )
		=>
			a == b ? 0 :
			Clamp01( (value - a) / (b - a) );

		public static int Dot( Vector2Int a, Vector2Int b )		=> a.x * b.x + a.y * b.y;

		public static double Clamp01( double value )
		{
			return
				value < 0.0 ? 0.0 :
				value > 1.0 ? 1.0 :
				value
			;
		}

		public static double Clamp( double value, double min, double max )
		{
			// https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Math/Mathf.cs

			return
				value < min ? min :
				value > max ? max :
				value
			;
		}

#endregion
#region Random

		public static Vector2Int RandomRange( Vector2Int minInclusive, Vector2Int maxExclusive )
		=>
			new Vector2Int(
				Random.Range( minInclusive.x, maxExclusive.x ),
				Random.Range( minInclusive.y, maxExclusive.y )
			);

		public static int RandomRange( Vector2Int range )			=> Random.Range( range.x, range.y + 1 );
		public static float RandomRange( Vector2 range )			=> Random.Range( range.x, range.y );
		public static float RandomPlusMinus1()						=> Random.value * 2 - 1;
		public static Vector2 RandomPlusMinus1v2()					=> new Vector2( RandomPlusMinus1(), RandomPlusMinus1() );
		public static Vector3 RandomPlusMinus1v3()					=> new Vector3( RandomPlusMinus1(), RandomPlusMinus1(), RandomPlusMinus1() );
		public static Vector2 RandomValue_v2()						=> new Vector2( Random.value, Random.value );
		public static Vector3 RandomValue_v3()						=> new Vector3( Random.value, Random.value, Random.value );
		public static bool RandomBool( float w0, float w1 )			=> Random.Range( 0, w0 + w1 ) > w0;
		public static bool RandomBool()								=> Random.Range( 0, 2 ) != 0;

		public static List< T > Shuffle_FisherYates< T >( this List< T > list )
		{
			var n		= list.Count;

			for (var i = 0; i < n - 1; i ++)
			{
				var j			= Random.Range( i, n );

				var tmp			= list[ i ];
				list[ i ]		= list[ j ];
				list[ j ]		= tmp;
			}

			return list;
		}

#endregion
#region Collections

		public static void Remove<T>( List<T> list, int index )
		{
			var lastIndex		= list.Count - 1;
			list[ index ]		= list[ lastIndex ];

			list.RemoveAt( lastIndex );
		}

		
		public static T[] GetEnumItems<T>()			=> (T[])Enum.GetValues( typeof(T) );

#endregion


		public static Vector2 CamHalfSize()			=> CamSize() / 2;
		public static Vector2 CamSize()				=> Camera.main.orthographicSize * new Vector2( Camera.main.aspect, 1 ) * 2;


		public static T Nullify<T>( ref T value ) where T : class
		{
			var copy		= value;
			value		= null;
			return copy;
		}


		public static T DeepClone<T>( this T obj )
		{
			// https://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-of-an-object-in-net
			using (var ms = new MemoryStream())
			{
				var formatter		= new BinaryFormatter();
				formatter.Serialize( ms, obj );
				ms.Position		= 0;
				return (T) formatter.Deserialize(ms);
			}
		}


		public static void DestroyChildren( Transform transform )
		{
			for (var i = transform.childCount - 1; i >= 0; i --)
				GameObject.Destroy( transform.GetChild( i ).gameObject );
		}


		public static void DisableAndDestroy( GameObject go )
		{
			go.SetActive( false );
			GameObject.Destroy( go );
		}
		
		public static void Repeat( int times, Action action )
		{
			for (var i = 0; i < times; i++)			
				action();
		}

		public static T GetRandomFromDistribution<T>( Dictionary<T, float> values )
		{
			var sum			= values.Sum( e => e.Value );
			float mpl		= 1.0f / sum;
			var keys		= values.Keys.ToList();
			float chance	= 0;
			float random	= UnityEngine.Random.value;
            for (int i = 0; i < values.Count; i++)
            {
				var key = keys[ i ];

				chance += values[ key ] * mpl;

				if (random <= chance)
					return key;
            }

            return keys[ 0 ];
		}

		public static List<T> GetRandomFromDistribution<T>( Dictionary<T, float> values, int count )
		{
			if (count >= values.Count)
				return values.Keys.ToList();

			var result		= new List<T>();
			var curValues	= values.ToDictionary( e => e.Key, e => e.Value );

            for (int i = 0; i < count; i++)
            {
				var r = GetRandomFromDistribution( curValues );
				result.Add( r );

				curValues = values.Where( e => !e.Key.Equals( r ) ).ToDictionary( e => e.Key, e => e.Value );
            }

            return result;
		}
	}
}

