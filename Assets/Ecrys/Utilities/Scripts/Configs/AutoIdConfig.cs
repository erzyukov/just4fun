namespace Ecrys.Configs
{
	using Sirenix.OdinInspector;
	using System;
#if UNITY_EDITOR
	using UnityEditor;
#endif
	using UnityEngine;


	public abstract class AutoIdConfig : SerializedScriptableObject
	{
		public uint Id		=> _id;

		[PropertyOrder(-1000)]
		[BoxGroup]
		[DisplayAsString]
		[SerializeField] private uint   _id;


		protected virtual void OnValidate()		=> AssignId();

		public void AssignId()
		{
#if UNITY_EDITOR

			string path         = AssetDatabase.GetAssetPath( this );

			if (string.IsNullOrWhiteSpace( path ))
				return;

			Guid guid           = new Guid( AssetDatabase.AssetPathToGUID( path ) );
			_id					= (uint) guid.GetHashCode();

#endif
		}

		public void SetConfigId( uint id ) => _id = id;
	}
}

