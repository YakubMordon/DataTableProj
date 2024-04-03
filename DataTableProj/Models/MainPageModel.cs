// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Models
{
    using System;
    using DataTableProj.Services.Helpers;
    using DataTableProj.ViewModels;
    using Newtonsoft.Json;

    /// <summary>
    /// Model for <see cref="MainPageViewModel"/>.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class MainPageModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageModel"/> class.
        /// </summary>
        public MainPageModel()
        {
            this.Persons = new ObservableList<PersonModel>();
            this.Person = new PersonModel();
        }

        /// <summary>
        /// Gets or sets list of <see cref="PersonModel"/>.
        /// </summary>
        [JsonProperty]
        public ObservableList<PersonModel> Persons { get; set; }

        /// <summary>
        /// Gets or sets person.
        /// </summary>
        [JsonProperty]
        public PersonModel Person { get; set; }
    }
}