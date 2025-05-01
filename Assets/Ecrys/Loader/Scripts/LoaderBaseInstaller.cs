namespace Ecrys.Loader
{
	using System;
	using System.Collections.Generic;
	using Zenject;


	public abstract class LoaderBaseInstaller : MonoInstaller
	{
		protected abstract List<Type> taskTypes { get; }

		public override void InstallBindings()
		{
			Container
				.Bind< LoaderModel >()
				.AsSingle();

			taskTypes.ForEach( t =>
			{
				Container
					.BindInterfacesTo( t )
					.AsSingle();
			} );
		}
	}
}
