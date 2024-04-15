// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using DataTableProj.Services.Serializers;
    using DataTableProj.ViewModels;
    using Serilog;
    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Save file name, of last state in app.
        /// </summary>
        private const string SaveFileName = "saveFile.json";

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class. This is the first line of executed
        /// code for the application, so it is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            var logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "log.txt");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath)
                .CreateLogger();

            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user. Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Log.Debug("Launching application...");

            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame is null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += this.OnNavigationFailed;

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated is false)
            {
                if (rootFrame.Content is null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPageView), e.Arguments);
                    this.LoadAppState();
                }

                // Ensure the current window is active
                Window.Current.Activate();
            }

            Log.Information("Application was successfully launched.");
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails.
        /// </summary>
        /// <param name="sender">The Frame which failed navigation.</param>
        /// <param name="e">Details about the navigation failure.</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspending request.</param>
        /// <param name="e">Details about the suspending request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            Log.Information("Suspending application...");

            var deferral = e.SuspendingOperation.GetDeferral();

            await this.SaveAppState();

            deferral.Complete();

            Log.Information("Application was successfully suspended.");
        }

        /// <summary>
        /// Method for saving app state.
        /// </summary>
        private async Task SaveAppState()
        {
            Log.Information("Saving application state...");

            try
            {
                var frame = (Frame)Window.Current.Content;

                var view = (MainPageView)frame.Content;

                var viewModel = (MainPageViewModel)view.DataContext;

                var localFolder = ApplicationData.Current.LocalFolder;

                var saveFile = await localFolder.CreateFileAsync(SaveFileName, CreationCollisionOption.ReplaceExisting);

                var serializer = new JsonSerializerService();

                await serializer.Serialize(viewModel, saveFile);

                Log.Information("Application state was successfully saved.");
            }
            catch (NullReferenceException ex)
            {
                Log.Error("Error occured, when tried to access object: {Message}", ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Error("Error occured, when loading file: {Message}", ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error occured: {Message}", ex.Message);
            }
        }

        /// <summary>
        /// Method for loading app state.
        /// </summary>
        private async void LoadAppState()
        {
            Log.Information("Loading application state...");

            try
            {
                var frame = (Frame)Window.Current.Content;

                var view = (MainPageView)frame.Content;

                var localFolder = ApplicationData.Current.LocalFolder;

                var saveFile = await localFolder.GetFileAsync(SaveFileName);

                var serializer = new JsonSerializerService();

                var deserializedModel = await serializer.Deserialize(saveFile);

                ((MainPageViewModel)view.DataContext).CopyFrom(deserializedModel);
            }
            catch (FileNotFoundException ex)
            {
                Log.Error("Error occured, when loading file: {Message}", ex.Message);
            }
            catch (NullReferenceException ex)
            {
                Log.Error("Error occured, when tried to access object: {Message}", ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error occured: {Message}", ex.Message);
            }
        }
    }
}
