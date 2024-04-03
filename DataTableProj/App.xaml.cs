﻿// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj
{
    using System;
    using DataTableProj.Services.Serializers;
    using DataTableProj.ViewModels;
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
        /// Initializes a new instance of the <see cref="App"/> class. This is the first line of executed
        /// code for the application, so it is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
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
            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame is null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += this.OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    this.SaveAppState();
                }

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
                }

                // Ensure the current window is active
                Window.Current.Activate();
            }
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
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            this.LoadAppState();

            deferral.Complete();
        }

        /// <summary>
        /// Method for saving app state.
        /// </summary>
        private void SaveAppState()
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            var frame = Window.Current.Content as Frame;

            if (frame is null)
            {
                return;
            }

            var view = frame.Content as MainPageView;

            if (view is null)
            {
                return;
            }

            var viewModel = view.DataContext as MainPageViewModel;

            if (viewModel is null)
            {
                return;
            }

            var model = viewModel.Model;

            if (model != null)
            {
                var serializer = new JsonSerializerService();
                var json = serializer.Serialize(model);
                localSettings.Values["AppState"] = json;
            }
        }

        /// <summary>
        /// Methid for loading app state.
        /// </summary>
        private void LoadAppState()
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey("AppState"))
            {
                var json = localSettings.Values["AppState"].ToString();

                var serializer = new JsonSerializerService();

                var model = serializer.Deserialize(json);

                var frame = Window.Current.Content as Frame;

                if (frame is null)
                {
                    return;
                }

                var view = frame.Content as MainPageView;

                if (view is null)
                {
                    return;
                }

                view.DataContext = model;
            }
        }
    }
}