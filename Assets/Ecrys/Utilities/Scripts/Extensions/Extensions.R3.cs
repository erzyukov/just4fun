namespace Ecrys.Utilities
{
	using ObservableCollections;
	using R3;
	using System;
	using UnityEngine.InputSystem;


	public static partial class R3_Extensions
	{
		public static Observable<TV> ObserveKey<TK, TV>( this ObservableDictionary<TK, TV> dict, TK key )
		{
			TV defaultValue		= default;

			return Observable
				.Merge(
					dict.ObserveDictionaryAdd()		.Where( x => x.Key.Equals( key ) )	.Select( x => x.Value ),
					dict.ObserveDictionaryReplace()	.Where( x => x.Key.Equals( key ) )	.Select( x => x.NewValue ),
					dict.ObserveDictionaryRemove()	.Where( x => x.Key.Equals( key ) )	.Select( _ => defaultValue ),
					dict.ObserveReset()													.Select( _ => defaultValue )
				)
				.Prepend( dict.GetOrDefault( key ) )
				.DistinctUntilChanged();
		}
		

		public static Observable<Unit> ObserveAll<TK, TV>( this ObservableDictionary<TK, TV> dict )
		=>
			Observable
				.Merge(
					dict.ObserveDictionaryAdd()		.AsUnitObservable(),
					dict.ObserveDictionaryReplace()	.AsUnitObservable(),
					dict.ObserveDictionaryRemove()	.AsUnitObservable(),
					dict.ObserveReset()				.AsUnitObservable()
				);


		public static Observable<Unit> ObserveAll<T>( this ObservableList<T> dict )
		=>
			Observable
				.Merge(
					dict.ObserveAdd()		.AsUnitObservable(),
					dict.ObserveReplace()	.AsUnitObservable(),
					dict.ObserveRemove()	.AsUnitObservable(),
					dict.ObserveReset()		.AsUnitObservable()
				);
		

		public static void AddDelta<TK>( this ObservableDictionary<TK, int> dict, TK key, int delta )
		=>
			dict[ key ]		= dict.GetOrDefault( key ) + delta;


		public static void AddDelta<TK>( this ObservableDictionary<TK, long> dict, TK key, long delta )
		=>
			dict[ key ]		= dict.GetOrDefault( key ) + delta;


		public static TV GetOrDefault<TK, TV>( this ObservableDictionary<TK, TV> dict, TK key )
		=>
			dict.TryGetValue( key, out var value ) ? value : default;


		// https://github.com/neuecc/UniRx/issues/481
		public static Observable<InputAction.CallbackContext> PerformedAsObservable( this InputAction action )
		=>
			Observable.FromEvent<InputAction.CallbackContext>(
				h => action.performed		+= h,
				h => action.performed		-= h
			);

		public static Observable<InputAction.CallbackContext> StartedAsObservable(this InputAction action)
		=>
			Observable.FromEvent<InputAction.CallbackContext>(
				h => action.started += h,
				h => action.started -= h
			);

		public static Observable<InputAction.CallbackContext> CanceledAsObservable(this InputAction action)
		=>
			Observable.FromEvent<InputAction.CallbackContext>(
				h => action.canceled += h,
				h => action.canceled -= h
			);

	}
}

