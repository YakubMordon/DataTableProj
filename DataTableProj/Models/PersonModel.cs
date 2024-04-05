// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Models
{
    using System;
    using System.ComponentModel;
    using DataTableProj.Extensions;
    using Newtonsoft.Json;

    /// <summary>
    /// Model for Person.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class PersonModel : INotifyPropertyChanged, ICloneable
    {
        /// <summary>
        /// First name of person.
        /// </summary>
        private string firstName;

        /// <summary>
        /// Last name of person.
        /// </summary>
        private string lastName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        public PersonModel()
        {
            this.firstName = string.Empty;
            this.lastName = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        /// <param name="firstName">First name of person.</param>
        /// <param name="lastName">Last name of person.</param>
        public PersonModel(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets first name of person.
        /// </summary>
        [JsonProperty]
        public string FirstName
        {
            get => this.firstName;
            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    this.NotifyPropertyChanged(this.PropertyChanged);
                }
            }
        }

        /// <summary>
        /// Gets or sets last name of person.
        /// </summary>
        [JsonProperty]
        public string LastName
        {
            get => this.lastName;
            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                    this.NotifyPropertyChanged(this.PropertyChanged);
                }
            }
        }

        /// <inheritdoc />
        public object Clone()
        {
            return new PersonModel(this.firstName, this.lastName);
        }
    }
}