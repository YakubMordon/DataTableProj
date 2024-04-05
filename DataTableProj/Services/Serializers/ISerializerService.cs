// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Services.Serializers
{
    using System;
    using System.Threading.Tasks;
    using DataTableProj.Models;
    using Windows.Storage;

    /// <summary>
    /// Interface-Service for serialization/deserialization of model.
    /// </summary>
    public interface ISerializerService : IDisposable
    {
        /// <summary>
        /// Method for serialization of <see cref="MainPageModel"/>.
        /// </summary>
        /// <param name="model"><see cref="MainPageModel"/>.</param>
        /// <param name="file">File, to save app data.</param>
        /// <exception cref="Exception">Exception thrown, when serializing isn't successful.</exception>
        /// <returns>Completed Task.</returns>
        Task Serialize(MainPageModel model, StorageFile file);

        /// <summary>
        /// Method for deserialization of <see cref="MainPageModel"/>.
        /// </summary>
        /// <param name="file">File to read saved app data.</param>
        /// <returns>Deserialized <see cref="MainPageModel"/>.</returns>
        /// <exception cref="Exception">Exception thrown, when deserializing isn't successful.</exception>
        Task<MainPageModel> Deserialize(StorageFile file);
    }
}