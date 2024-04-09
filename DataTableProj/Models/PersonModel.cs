// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Models
{
    using System;
    using System.Runtime.Serialization;
    using DataTableProj.BaseClasses;
    using Newtonsoft.Json;

    /// <summary>
    /// Model for Person.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class PersonModel : BaseNotifyPropertyChanged, ICloneable
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
        /// Value indicating whether person is now available for editing or not.
        /// </summary>
        private bool isEditing = false;

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

        /// <summary>
        /// Gets or sets id of person.
        /// </summary>
        [JsonProperty]
        public Guid Id { get; set; } = Guid.NewGuid();

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
                    this.NotifyPropertyChanged();
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
                    this.NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether person is now available for editing or not.
        /// </summary>
        [JsonProperty]
        public bool IsEditing
        {
            get => this.isEditing;
            set
            {
                this.isEditing = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <inheritdoc />
        public object Clone()
        {
            return new PersonModel
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                IsEditing = this.isEditing,
            };
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            if (this.isEditing == null)
            {
                this.IsEditing = false;
            }
        }

        [OnDeserialized]
        private void OnDeserializing(StreamingContext context)
        {
            if (this.isEditing == null)
            {
                this.IsEditing = false;
            }
        }
    }
}