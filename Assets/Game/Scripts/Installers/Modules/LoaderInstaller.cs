namespace Ecrys.Installers
{
	using Ecrys.Core;
	using Ecrys.Loader;
	using Ecrys.UI;
	using System;
	using System.Collections.Generic;


	public class LoaderInstaller : LoaderBaseInstaller
	{
		protected override List<Type> taskTypes => new()
		{
			//typeof( LoadProfileTask ),
			typeof( LoadScenesTask ),
		};

		public override void InstallBindings()
		{
			base.InstallBindings();

			//Loader
			Container
				.BindInterfacesTo< Loader >()
				.AsSingle();

			//UiVeil
			Container
				.BindInterfacesTo< UiVeil >()
				.FromComponentInHierarchy()
				.AsSingle();
		}
	}
}