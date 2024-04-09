﻿// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Services.Serializers
{
    using System;
    using System.Threading.Tasks;
    using DataTableProj.Models;
    using Newtonsoft.Json;
    using Serilog;
    using Windows.Storage;

    /// <summary>
    /// Service for serializing / deserializing JSON model.
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
        /// Method for deserialization of <see cref="MainPageModel"/>.
        /// </summary>
        /// <param name="file">File to read saved app data.</param>
        /// <returns>Deserialized <see cref="MainPageModel"/>.</returns>
        /// <exception cref="Exception">Exception thrown, when deserializing isn't successful.</exception>
        public async Task<MainPageModel> Deserialize(StorageFile file)
        {
            Log.Information("Deserializing content of file: {file}", file);

            var json = await FileIO.ReadTextAsync(file);

            var model = JsonConvert.DeserializeObject<MainPageModel>(json, this.settings) ?? new MainPageModel();

            Log.Information("Deserialized model: {model}", model);

            return model;
        }

        /// <summary>
        /// Method for serialization of <see cref="MainPageModel"/>.
        /// </summary>
        /// <param name="model"><see cref="MainPageModel"/>.</param>
        /// <param name="file">File, to save app data.</param>
        /// <returns>Completed Task.</returns>
        public async Task Serialize(MainPageModel model, StorageFile file)
        {
            Log.Information("Model for serialization: {model}", model);
            Log.Information("Serializing file: {file}", file);

            var json = JsonConvert.SerializeObject(model, this.settings);

            Log.Information("Serialized model: {json}", json);

            await FileIO.WriteTextAsync(file, json);
        }
    }
}