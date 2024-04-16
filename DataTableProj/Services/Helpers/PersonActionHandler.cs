// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Services.Helpers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DataTableProj.Models;
    using DataTableProj.Views.ContentDialogs;
    using Serilog;

    /// <summary>
    /// Handler for <see cref="PersonModel"/>-related actions.
    /// </summary>
    public class PersonActionHandler
    {
        /// <summary>
        /// Method for checking if person could be added.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <returns>True if person could be added. False if person could not be added.</returns>
        public async Task<bool> CheckPersonAsync(PersonModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName) || string.IsNullOrWhiteSpace(model.LastName))
            {
                Log.Information("First name or Last name were empty:\nFirstName: {FirstName}\n\nLastName: {LastName}", model.FirstName, model.LastName);

                var dialog = new InputErrorDialog();

                await dialog.ShowAsync();

                return false;
            }

            return true;
        }

        /// <summary>
        /// Method for adding model to <see cref="ObservableList{T}"/>.
        /// </summary>
        /// <param name="persons"><see cref="ObservableList{T}"/>, which contains <see cref="PersonModel"/>s.</param>
        /// <param name="model">Model.</param>
        public void Add(ObservableList<PersonModel> persons, PersonModel model)
        {
            Log.Information("Adding user...");

            var newPerson = model.Clone() as PersonModel;

            persons.Add(newPerson);

            model.FirstName = model.LastName = string.Empty;

            Log.Information("User added: {newPerson}", newPerson);
        }

        /// <summary>
        /// Method for removing model from <see cref="ObservableList{T}"/>.
        /// </summary>
        /// <param name="persons"><see cref="ObservableList{T}"/>, which contains <see cref="PersonModel"/>s.</param>
        /// <param name="model">Model.</param>
        /// <returns>Completed Task.</returns>
        public async Task RemoveAsync(ObservableList<PersonModel> persons, PersonModel model)
        {
            Log.Information("Removing person...");

            var dialog = new DeleteConfirmationDialog(() => persons.Remove(model));

            await dialog.ShowAsync();

            Log.Information("Person removed: {model}", model);
        }
    }
}