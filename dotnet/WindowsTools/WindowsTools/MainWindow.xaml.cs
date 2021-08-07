using Ninject;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using WindowsTools.Services;
using Application = System.Windows.Application;

namespace WindowsTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IKernel _kernel;
        private readonly ThemeTool _themeTool;

        public MainWindow()
        {
            InitializeComponent();

            #region Injection
            _kernel = new StandardKernel(new ServiceModule());
            _themeTool = _kernel.Get<ThemeTool>(); 
            #endregion

            Initialize();
        }

        private void Initialize()
        {
            Visibility = Visibility.Hidden;
            var notifyIcon = new NotifyIcon()
            {
                Visible = true,
                Icon = new Icon(@"Assets\windows.ico"),
                ContextMenuStrip = new ContextMenuStrip(),
            };
            notifyIcon.ContextMenuStrip.Items.Add("Toggle Apps Light Theme", null, (_, __) => _themeTool.ToggleAppsLightTheme());
            notifyIcon.ContextMenuStrip.Items.Add("Toggle System Light Theme", null, (_, __) => _themeTool.ToggleSystemLightTheme());
            notifyIcon.ContextMenuStrip.Items.Add("Quit", null, (_, __) => Application.Current.Shutdown());
        }
    }
}
