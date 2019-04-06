namespace AocWikiTranslationHelper
{
    using BLL;
    using Contracts;
    using Unity;
    using ViewModels;

    public partial class App
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Constructors

        public App()
        {
            Startup += App_Startup;
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Private Methods

        private IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();

            // Views
            container.RegisterType<IMainWindow, MainWindow>();

            // ViewModels
            container.RegisterType<IMainWindowViewModel, MainWindowViewModel>();

            // Models

            // BLL
            container.RegisterType<IAutoLocalizer, AutoLocalizer>();
            container.RegisterType<ITextParser, TextParser>();

            // DAL


            return container;
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Event Handler

        private void App_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            using (var container = CreateContainer())
            {
                var mainWindow = container.Resolve<IMainWindow>();
                mainWindow.ShowDialog();
            }
        }

        #endregion
    }
}
