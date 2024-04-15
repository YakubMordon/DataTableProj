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

        /// <summary>
        /// Method for toggling model to Edit Mode.
        /// </summary>
        /// <param name="editablePersons"><see cref="ObservableList{T}"/>, which contains editable <see cref="PersonModel"/>s.</param>
        /// <param name="model">Model.</param>
        public void Edit(ObservableList<PersonModel> editablePersons, PersonModel model)
        {
            Log.Information("Enabling Edit Mode for row...");

            var clonedPerson = model.Clone() as PersonModel;

            editablePersons.Add(clonedPerson);

            model.IsEditing = true;

            Log.Information("Edit mode activated for person: {model}", model);
        }

        /// <summary>
        /// Method for saving changes from Edit Mode.
        /// </summary>
        /// <param name="editablePersons"><see cref="ObservableList{T}"/>, which contains editable <see cref="PersonModel"/>s.</param>
        /// <param name="model">Model.</param>
        public void Save(ObservableList<PersonModel> editablePersons, PersonModel model)
        {
            Log.Information("Saving changes...");

            var index = editablePersons.FindIndex(editablePerson => editablePerson.Id == model.Id);

            editablePersons.RemoveAt(index);

            model.IsEditing = false;

            Log.Information("Saved person: {model}", model);
        }

        /// <summary>
        /// Method for discarding changes in Edit Mode.
        /// </summary>
        /// <param name="editablePersons"><see cref="ObservableList{T}"/>, which contains editable <see cref="PersonModel"/>s.</param>
        /// <param name="model">Model.</param>
        public void Discard(ObservableList<PersonModel> editablePersons, PersonModel model)
        {
            Log.Information("Discarding changes...");
            Log.Information("Person for discarding: {model}", model);

            var originalPerson = editablePersons.First(editablePerson => editablePerson.Id == model.Id);

            model.FirstName = originalPerson.FirstName;
            model.LastName = originalPerson.LastName;
            model.IsEditing = false;

            Log.Information("Person renewed: {model}", model);
        }
    }
}