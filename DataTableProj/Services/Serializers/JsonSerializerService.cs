// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Services.Serializers
{
    using System;
    using System.Threading.Tasks;
    using DataTableProj.Models;
    using Newtonsoft.Json;
    using Windows.Storage;

    /// <summary>
    /// Service for serializing / deserializing JSON model.
    /// </summary>
    public class JsonSerializerService : ISerializerService
    {
        /// <summary>
        /// Settings for JSON serializer.
        /// </summary>
        private JsonSerializerSettings _settings;

        /// <summary>
        /// Indicates if object is disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializerService"/> class.
        /// </summary>
        public JsonSerializerService()
        {
            this._settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

            this._disposed = false;
        }

        /// <inheritdoc/>
        public async Task<MainPageModel> Deserialize(StorageFile file)
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException("Exception thrown. Object was disposed.");
            }

            var json = await FileIO.ReadTextAsync(file);

            var model = JsonConvert.DeserializeObject<MainPageModel>(json, this._settings) ?? new MainPageModel();

            return model;
        }

        /// <inheritdoc/>
        public async Task Serialize(MainPageModel model, StorageFile file)
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException("Exception thrown. Object was disposed.");
            }

            var json = JsonConvert.SerializeObject(model, this._settings);

            await FileIO.WriteTextAsync(file, json);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="JsonSerializerService"/> and optionally releases the managed resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="JsonSerializerService"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; False to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                this._settings = null;
            }

            this._disposed = true;
        }
    }
}