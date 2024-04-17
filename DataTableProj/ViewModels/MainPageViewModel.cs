// Copyright (c) Digital Cloud Technologies. All rights reserved.

using DataTableProj.DTOs;

namespace DataTableProj.ViewModels
{
    using System.Threading.Tasks;
    using DataTableProj.BaseClasses;
    using DataTableProj.Models;
    using DataTableProj.Services.Helpers;

    /// <summary>
    /// ViewModel for <see cref="MainPageView"/>.
    /// </summary>
    public class MainPageViewModel : BaseNotifyPropertyChanged
    {
        /// <summary>
        /// Service for handling actions related with <see cref="PersonModel"/>.
        /// </summary>
        private readonly PersonActionHandler personActionHandler;

        /// <summary>
        /// List of <see cref="PersonModel"/>.
        /// </summary>
        private ObservableList<PersonModel> persons;

        /// <summary>
        /// Person for adding.
        /// </summary>
        private PersonModel person;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        public MainPageViewModel()
        {
            this.personActionHandler = new PersonActionHandler();

            this.InitializeCommands();

            this.InitializeData();
        }

        /// <summary>
        /// Gets list of <see cref="PersonModel"/>.
        /// </summary>
        public ObservableList<PersonModel> Persons
        {
            get => this.persons;
            private set
            {
                if (this.persons != value)
                {
                    this.persons = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets person for adding.
        /// </summary>
        public PersonModel Person
        {
            get => this.person;
            set
            {
                if (this.person != value)
                {
                    this.person = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets command for adding user.
        /// </summary>
        public RelayCommand AddPersonCommand { get; private set; }

        /// <summary>
        /// Gets command for deleting user.
        /// </summary>
        public RelayCommand RemovePersonCommand { get; private set; }

        /// <summary>
        /// Gets command for editing user.
        /// </summary>
        public RelayCommand EditPersonCommand { get; private set; }

        /// <summary>
        /// Gets command for saving changes in editing.
        /// </summary>
        public RelayCommand SaveChangesCommand { get; private set; }

        /// <summary>
        /// Gets command for discarding changes in editing.
        /// </summary>
        public RelayCommand DiscardChangesCommand { get; private set; }

        /// <summary>
        /// Method for copying data from other model.
        /// </summary>
        /// <param name="model">Deserialized model.</param>
        public void CopyFrom(AppStateDto model)
        {
            this.Person = model.Person;

            this.Persons = model.Persons;
        }

        /// <summary>
        /// Method for adding person to list.
        /// </summary>
        private async Task AddUserAsync()
        {
            var isCorrect = await this.personActionHandler.CheckPersonAsync(this.Person);

            if (isCorrect)
            {
                this.personActionHandler.Add(this.Persons, this.Person);
            }
        }

        /// <summary>
        /// Method for removing person from list.
        /// </summary>
        /// <param name="sender">Object, which was sent.</param>
        private async Task RemovePersonAsync(object sender)
        {
            var model = (PersonModel)sender;

            await this.personActionHandler.RemoveAsync(this.Persons, model);
        }

        /// <summary>
        /// Method for editing person in list.
        /// </summary>
        /// <param name="sender">Object, which was sent.</param>
        private void EditPerson(object sender)
        {
            var model = (PersonModel)sender;

            model.BeginEdit();
        }

        /// <summary>
        /// Method for saving changes in edit.
        /// </summary>
        /// <param name="sender">Object, which was sent.</param>
        private void SaveChanges(object sender)
        {
            var model = (PersonModel)sender;

            model.EndEdit();
        }

        /// <summary>
        /// Method for discarding changes in edit.
        /// </summary>
        /// <param name="sender">Object, which was sent.</param>
        private void DiscardChanges(object sender)
        {
            var model = (PersonModel)sender;

            model.CancelEdit();
        }

        /// <summary>
        /// Method for initializing commands.
        /// </summary>
        private void InitializeCommands()
        {
            this.AddPersonCommand = new RelayCommand(async execute => await this.AddUserAsync());

            this.RemovePersonCommand = new RelayCommand(async execute => await this.RemovePersonAsync(execute));

            this.EditPersonCommand = new RelayCommand(this.EditPerson);

            this.SaveChangesCommand = new RelayCommand(this.SaveChanges);

            this.DiscardChangesCommand = new RelayCommand(this.DiscardChanges);
        }

        /// <summary>
        /// Method for initializing data objects.
        /// </summary>
        private void InitializeData()
        {
            this.persons = new ObservableList<PersonModel>();

            this.Person = new PersonModel();
        }
    }
}