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
    public class MainPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Model of Main Page.
        /// </summary>
        private MainPageModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        public MainPageViewModel()
        {
            this.AddPersonCommand = new RelayCommand(async execute => await this.AddUserAsync());
            this.RemovePersonCommand = new RelayCommand(execute => this.RemovePerson(execute));

            this.Model = new MainPageModel();
        }

        /// <summary>
        /// Gets or sets model of Main Page.
        /// </summary>
        public MainPageModel Model
        {
            get => this.model;
            set
            {
                if (this.model != value)
                {
                    this.model = value;
                    this.OnPropertyChanged(nameof(this.Model));
                }
            }
        }

        /// <summary>
        /// Gets command for adding user.
        /// </summary>
        public RelayCommand AddPersonCommand { get; }

        /// <summary>
        /// Gets command for deleting user.
        /// </summary>
        public RelayCommand RemovePersonCommand { get; }

        /// <summary>
        /// Method for adding person to list.
        /// </summary>
        private async Task AddUserAsync()
        {
            if (string.IsNullOrWhiteSpace(this.Model.Person.FirstName) || string.IsNullOrWhiteSpace(this.Model.Person.LastName))
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

            var newPerson = this.Model.Person.Clone() as PersonModel;

            this.Model.Persons.Add(newPerson);

            this.Model.Person.FirstName = string.Empty;
            this.Model.Person.LastName = string.Empty;
        }

        /// <summary>
        /// Method for removing person from list.
        /// </summary>
        /// <param name="sender">Object, which was sent.</param>
        private void RemovePerson(object sender)
        {
            if (sender is PersonModel person)
            {
                this.Model.Persons.Remove(person);
            }
        }
    }
}