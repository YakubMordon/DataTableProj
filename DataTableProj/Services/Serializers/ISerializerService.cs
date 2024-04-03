// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Services.Serializers
{
    using System;
    using DataTableProj.Models;

    /// <summary>
    /// Interface-Service for serialization/deserialization of model.
    /// </summary>
    public interface ISerializerService : IDisposable
    {
        /// <summary>
        /// Method for serialization of <see cref="MainPageModel"/>.
        /// </summary>
        /// <param name="model"><see cref="MainPageModel"/>.</param>
        /// <exception cref="Exception">Exception thrown, when serializing isn't successful.</exception>
        /// <returns>Serialized json string.</returns>
        string Serialize(MainPageModel model);

        /// <summary>
        /// Method for deserialization of <see cref="MainPageModel"/>.
        /// </summary>
        /// <param name="json">Json for deserializing.</param>
        /// <returns>Deserialized <see cref="MainPageModel"/>.</returns>
        /// <exception cref="Exception">Exception thrown, when deserializing isn't successful.</exception>
        MainPageModel Deserialize(string json);
    }
}