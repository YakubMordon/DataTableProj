// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Services.Serializers
{
    using System;
    using System.Threading.Tasks;
    using DataTableProj.DTOs;
    using Newtonsoft.Json;
    using Serilog;
    using Windows.Storage;

    /// <summary>
    /// Service for serializing / deserializing JSON view model.
    /// </summary>
    public class JsonSerializerService
    {
        /// <summary>
        /// Settings for JSON serializer.
        /// </summary>
        private readonly JsonSerializerSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializerService"/> class.
        /// </summary>
        public JsonSerializerService()
        {
            this.settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
        }

        /// <summary>
        /// Method for deserialization of <see cref="AppStateDto"/>.
        /// </summary>
        /// <param name="file">File to read saved app data.</param>
        /// <returns>Deserialized <see cref="AppStateDto"/>.</returns>
        /// <exception cref="Exception">Exception thrown, when deserializing isn't successful.</exception>
        public async Task<AppStateDto> Deserialize(StorageFile file)
        {
            Log.Information("Deserializing content of file: {file}", file);

            var json = await FileIO.ReadTextAsync(file);

            var model = JsonConvert.DeserializeObject<AppStateDto>(json, this.settings);

            Log.Information("Deserialized model: {model}", model);

            return model;
        }

        /// <summary>
        /// Method for serialization of <see cref="AppStateDto"/>.
        /// </summary>
        /// <param name="model">App State for saving.</param>
        /// <param name="file">File, to save app data.</param>
        /// <returns>Completed Task.</returns>
        public async Task Serialize(AppStateDto model, StorageFile file)
        {
            Log.Information("Model for serialization: {model}", model);
            Log.Information("Serializing file: {file}", file);

            var json = JsonConvert.SerializeObject(model, this.settings);

            Log.Information("Serialized model: {json}", json);

            await FileIO.WriteTextAsync(file, json);
        }
    }
}