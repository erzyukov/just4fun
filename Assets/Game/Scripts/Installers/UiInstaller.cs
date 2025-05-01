namespace Ecrys.Installers
{
	using Zenject;
	using Ecrys.UiManager;


	public class UiInstaller : UiBaseInstaller< EScene, EScreen, EPopup >
	{
		protected override EScene BootstrapScene => EScene.Bootstrap;

		public override void InstallBindings()
		{
			ForSceneExt.BindScene		= Scene;

			base.InstallBindings();
		}
	}

	public static class ForSceneExt
	{
		public static EScene	BindScene;

		public static ConditionCopyNonLazyBinder ForScene( this ConditionCopyNonLazyBinder binder, EScene scene )
		{
			var copy		= BindScene;

			binder.When( _ => copy == scene );

			return binder;
		}
	}
}
