// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using DataTableProj.Models;
    using DataTableProj.Services.Helpers;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// ViewModel for <see cref="MainPageView"/>.
    /// </summary>
    public class MainPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        public MainPageViewModel()
        {
            this.AddPersonCommand = new RelayCommand(async execute => await this.AddUserAsync());

            this.DeletePersonCommand = new RelayCommand(execute => this.RemovePerson(execute));

            this.Model = new MainPageModel();
        }

        /// <summary>
        /// Gets or sets model of Main Page.
        /// </summary>
        public MainPageModel Model { get; set; }

        /// <summary>
        /// Gets command for adding user.
        /// </summary>
        public RelayCommand AddPersonCommand { get; }

        /// <summary>
        /// Gets command for deleting user.
        /// </summary>
        public RelayCommand DeletePersonCommand { get; }

        /// <summary>
        /// Method for adding person to list.
        /// </summary>
        private async Task AddUserAsync()
        {
            if (this.Model.Person.FirstName is null || this.Model.Person.LastName is null)
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Some inputs are empty",
                    CloseButtonText = "Close",
                };

                await dialog.ShowAsync();

                return;
            }

            this.Model.Persons.Add(this.Model.Person);

            this.Model.Person.FirstName = string.Empty;
            this.Model.Person.LastName = string.Empty;
        }

        /// <summary>
        /// Method for removing person from list.
        /// </summary>
        /// <param name="sender">Object, which was sent.</param>
        private void RemovePerson(object sender)
        {
            if (sender is PersonModel model)
            {
                this.Model.Persons.Remove(model);
            }
        }
    }
}