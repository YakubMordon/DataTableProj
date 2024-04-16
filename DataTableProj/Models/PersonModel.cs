// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Models
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using DataTableProj.BaseClasses;
    using Newtonsoft.Json;
    using Serilog;

    /// <summary>
    /// Model for Person.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class PersonModel : BaseNotifyPropertyChanged, IEditableObject, ICloneable
    {
        /// <summary>
        /// Current person data.
        /// </summary>
        private PersonData personData;

        /// <summary>
        /// Backup Data for user, for discarding changes, when editing.
        /// </summary>
        [JsonProperty]
        private PersonData backupData;

        /// <summary>
        /// Value indicating whether person is now available for editing or not.
        /// </summary>
        private bool isEditing = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        public PersonModel()
        {
            this.personData = new PersonData
            {
                FirstName = string.Empty,
                LastName = string.Empty,
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModel"/> class.
        /// </summary>
        /// <param name="firstName">First name of person.</param>
        /// <param name="lastName">Last name of person.</param>
        public PersonModel(string firstName, string lastName)
        {
            this.personData = new PersonData
            {
                FirstName = firstName,
                LastName = lastName,
            };
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
            get => this.personData.FirstName;
            set
            {
                if (this.personData.FirstName != value)
                {
                    this.personData.FirstName = value;
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
            get => this.personData.LastName;
            set
            {
                if (this.personData.LastName != value)
                {
                    this.personData.LastName = value;
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
        public void BeginEdit()
        {
            Log.Debug("Starting BeginEdit");

            if (!this.IsEditing)
            {
                Log.Debug("BeginEdit Data - {personData}", this.personData);

                this.backupData = this.personData;

                this.IsEditing = !this.IsEditing;
            }

            Log.Debug("End BeginEdit");
        }

        /// <inheritdoc />
        public void CancelEdit()
        {
            Log.Debug("Starting CancelEdit");

            if (this.IsEditing)
            {
                Log.Debug("CancelEdit Data - {backupData}", this.backupData);

                this.FirstName = this.backupData.FirstName;
                this.LastName = this.backupData.LastName;

                this.IsEditing = !this.IsEditing;

                this.backupData = default;
            }

            Log.Debug("End CancelEdit");
        }

        /// <inheritdoc />
        public void EndEdit()
        {
            Log.Debug("Starting EndEdit");

            if (this.IsEditing)
            {
                Log.Debug("CancelEdit Data - {personData}", this.personData);

                this.backupData = default;

                this.IsEditing = !this.IsEditing;
            }

            Log.Debug("End EndEdit");
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

        [OnDeserialized]
        private void OnDeserializing(StreamingContext context)
        {
            if (this.isEditing == null)
            {
                this.IsEditing = false;
            }
        }

        /// <summary>
        /// Structure for saving person data.
        /// </summary>
        internal struct PersonData
        {
            /// <summary>
            /// First name of person.
            /// </summary>
            internal string FirstName;

            /// <summary>
            /// Last name of person.
            /// </summary>
            internal string LastName;
        }
    }
}