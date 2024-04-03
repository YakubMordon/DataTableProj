// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Models
{
    using System;
    using System.ComponentModel;
    using Newtonsoft.Json;

    /// <summary>
    /// Model for Person.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class PersonModel : INotifyPropertyChanged
    {
        /// <summary>
        /// First name of person.
        /// </summary>
        private string _firstName;

        /// <summary>
        /// Last name of person.
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        public PersonModel()
        {
            this._firstName = string.Empty;
            this._lastName = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        /// <param name="firstName">First name of person.</param>
        /// <param name="lastName">Last name of person.</param>
        public PersonModel(string firstName, string lastName)
        {
            this._firstName = firstName;
            this._lastName = lastName;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets first name of person.
        /// </summary>
        [JsonProperty]
        public string FirstName
        {
            get => this._firstName;
            set
            {
                if (this._firstName != value)
                {
                    this._firstName = value;
                    this.OnPropertyChanged(nameof(this.FirstName));
                }
            }
        }

        /// <summary>
        /// Gets or sets last name of person.
        /// </summary>
        [JsonProperty]
        public string LastName
        {
            get => this._lastName;
            set
            {
                if (this._lastName != value)
                {
                    this._lastName = value;
                    this.OnPropertyChanged(nameof(this.LastName));
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