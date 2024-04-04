// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj
{
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Main Page of UWP application.
    /// </summary>
    public sealed partial class MainPageView : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageView"/> class.
        /// </summary>
        public MainPageView()
        {
            this.InitializeComponent();

            this.DataContext = this.Resources["ViewModel"];
        }
    }
}
