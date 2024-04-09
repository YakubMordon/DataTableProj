// Copyright (c) Digital Cloud Technologies. All rights reserved.

using System.Linq;

namespace DataTableProj.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using DataTableProj.BaseClasses;
    using DataTableProj.Models;
    using DataTableProj.Services.Helpers;
    using DataTableProj.Views.ContentDialogs;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// ViewModel for <see cref="MainPageView"/>.
    /// </summary>
    public class MainPageViewModel : BaseNotifyPropertyChanged
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
            this.RemovePersonCommand = new RelayCommand(async execute => await this.RemovePersonAsync(execute));
            this.EditPersonCommand = new RelayCommand(this.EditPerson);
            this.SaveChangesCommand = new RelayCommand(this.SaveChanges);
            this.DiscardChangesCommand = new RelayCommand(this.DiscardChanges);

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
                    this.NotifyPropertyChanged();
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
        /// Gets command for editing user.
        /// </summary>
        public RelayCommand EditPersonCommand { get; }

        /// <summary>
        /// Gets command for saving changes in editing.
        /// </summary>
        public RelayCommand SaveChangesCommand { get; }

        /// <summary>
        /// Gets command for discarding changes in editing.
        /// </summary>
        public RelayCommand DiscardChangesCommand { get; }

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
        private async Task RemovePersonAsync(object sender)
        {
            if (sender is PersonModel person)
            {
                Console.WriteLine(person);

                var dialog = new DeleteConfirmationDialog(() => this.Model.Persons.Remove(person));
                await dialog.ShowAsync();
            }
        }

        /// <summary>
        /// Method for editing person in list.
        /// </summary>
        /// <param name="sender">Object, which was sent.</param>
        private void EditPerson(object sender)
        {
            if (sender is PersonModel person)
            {
                person.IsEditing = true;

                var clonedPerson = person.Clone() as PersonModel;

                this.model.EditablePersons.Add(clonedPerson);
            }
        }

        /// <summary>
        /// Method for saving changes in edit.
        /// </summary>
        /// <param name="sender">Object, which was sent.</param>
        private void SaveChanges(object sender)
        {
            if (sender is PersonModel person)
            {
                person.IsEditing = false;

                var index = this.model.EditablePersons.FindIndex(editablePerson => editablePerson.Id == person.Id);

                this.model.EditablePersons.RemoveAt(index);
            }
        }

        /// <summary>
        /// Method for discarding changes in edit.
        /// </summary>
        /// <param name="sender">Object, which was sent.</param>
        private void DiscardChanges(object sender)
        {
            if (sender is PersonModel discardedPerson)
            {
                discardedPerson.IsEditing = false;

                var originalPerson = this.model.EditablePersons.First(person => person.Id == discardedPerson.Id);

                discardedPerson.FirstName = originalPerson.FirstName;
                discardedPerson.LastName = originalPerson.LastName;
            }
        }
    }
}