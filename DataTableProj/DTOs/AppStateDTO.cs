namespace DataTableProj.DTOs
{
    using DataTableProj.Models;
    using DataTableProj.Services.Helpers;

    /// <summary>
    /// DTO class for App State deserialization.
    /// </summary>
    public class AppStateDto
    {
        /// <summary>
        /// Gets or sets list of <see cref="PersonModel"/>.
        /// </summary>
        public ObservableList<PersonModel> Persons { get; set; }

        /// <summary>
        /// Gets or sets person for adding.
        /// </summary>
        public PersonModel Person { get; set; }
    }
}