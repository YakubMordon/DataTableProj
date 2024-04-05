// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Models
{
    using System;
    using System.ComponentModel;
    using DataTableProj.Extensions;
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
        private ObservableList<PersonModel> persons;

        /// <summary>
        /// Person for adding.
        /// </summary>
        private PersonModel person;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageModel"/> class.
        /// </summary>
        public MainPageModel()
        {
            this.persons = new ObservableList<PersonModel>();
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
            get => this.persons;
        }

        /// <summary>
        /// Gets or sets person for adding.
        /// </summary>
        [JsonProperty]
        public PersonModel Person
        {
            get => this.person;
            set
            {
                if (this.person != value)
                {
                    this.person = value;
                    this.NotifyPropertyChanged(this.PropertyChanged);
                }
            }
        }
    }
}