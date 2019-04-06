namespace AocWikiTranslationHelper
{
    using Contracts;
    using ViewModels;

    public partial class MainWindow : IMainWindow
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
