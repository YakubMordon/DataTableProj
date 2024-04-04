// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Models
{
    using System;
    using System.ComponentModel;
    using DataTableProj.Services.Helpers;
    using DataTableProj.ViewModels;
    using Newtonsoft.Json;

    /// <summary>
    /// Model for <see cref="MainPageViewModel"/>.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MainPageModel : INotifyPropertyChanged
    {
        /// <summary>
        /// List of <see cref="PersonModel"/>.
        /// </summary>
        private ObservableList<PersonModel> _persons;

        /// <summary>
        /// Person for adding.
        /// </summary>
        private PersonModel _person;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageModel"/> class.
        /// </summary>
        public MainPageModel()
        {
            this._persons = new ObservableList<PersonModel>();
            this.Person = new PersonModel();
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets list of <see cref="PersonModel"/>.
        /// </summary>
        [JsonProperty]
        public ObservableList<PersonModel> Persons
        {
            get => this._persons;
        }

        /// <summary>
        /// Gets or sets person for adding.
        /// </summary>
        [JsonProperty]
        public PersonModel Person
        {
            get => this._person;
            set
            {
                if (this._person != value)
                {
                    this._person = value;
                    this.OnPropertyChanged(nameof(this.Person));
                }
            }
        }

        /// <summary>
        /// Method for handling property change.
        /// </summary>
        /// <param name="propertyName">Name of property, which was changed.</param>
        protected virtual void OnPropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}