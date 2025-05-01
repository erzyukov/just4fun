namespace Ecrys.UiManager
{
	using System;
	using System.Linq;
	using UnityEngine;
	using Zenject;


	public abstract class UiBaseInstaller<TScene, TScreen, TPopup> : MonoInstaller 
		where TScene : Enum where TScreen : Enum where TPopup : Enum
	{
		[SerializeField] protected TScene	Scene;

		protected abstract TScene BootstrapScene { get; }


	    public override void InstallBindings()
	    {
			Install_Core();
		}

		void Install_Core()
		{
			// UiModel
			Container
				.Bind< UiModel<TScreen, TPopup> >()
				.AsSingle()
				.When( _ => Scene.Equals( BootstrapScene ) );

			// UiNavigator
			Container
				.BindInterfacesTo< UiNavigator<TScreen, TPopup> >()
				.AsSingle()
				.When( _ => Scene.Equals( BootstrapScene ) );
		}


		protected void BindViewPresenter< TView, TPresenter >( TScene viewContext = default, TScene presenterContext = default )
		{
			BindView< TView >( viewContext );
			BindPresenter< TPresenter >( presenterContext );
		}


		protected void BindView< TView >( TScene viewContext )
		{
			if (!viewContext.Equals( Scene ))
				return;

			var interfaces			= typeof(TView)
				.GetInterfaces()
				.Where( t =>
					t != typeof(IUiViewBase)
				)
				.ToList();

			// View
			Container
				.Bind( interfaces )
				.To<TView>()
				.FromComponentInHierarchy()
				.AsSingle();
		}
		
		protected void BindPresenter< TPresenter >( TScene presenterContext = default )
		{
			if (!presenterContext.Equals( Scene ))
				return;

			Container
				.BindInterfacesTo<TPresenter>()
				.AsSingle();
		}
	}
}

