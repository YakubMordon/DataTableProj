// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Services.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary>
    /// Represents a generic observable list that provides notifications when items are added, removed, or when the list is cleared.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class ObservableList<T> : List<T>, INotifyCollectionChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableList{T}"/> class.
        /// </summary>
        public ObservableList()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableList{T}"/> class.
        /// </summary>
        /// <param name="list">List, from which will be content copied.</param>
        public ObservableList(IEnumerable<T> list)
        {
            this.AddRange(list);
        }

        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public new T this[int index]
        {
            get => base[index];
            set
            {
                base[index] = value;
                this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, index));
            }
        }

        /// <summary>
        /// Adds an object to the end of the list and raises the <see cref="CollectionChanged"/> event.
        /// </summary>
        /// <param name="item">The object to add to the list.</param>
        public new void Add(T item)
        {
            base.Add(item);
            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the list and raises the <see cref="CollectionChanged"/> event.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the end of the list.</param>
        /// <exception cref="ArgumentNullException">Thrown when the collection is null.</exception>
        public new void AddRange(IEnumerable<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var startingIndex = this.Count;

            base.AddRange(collection);

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection, startingIndex));
        }

        /// <summary>
        /// Removes all elements from the list and raises the <see cref="CollectionChanged"/> event.
        /// </summary>
        public new void Clear()
        {
            base.Clear();

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset, this));
        }

        /// <summary>
        /// Inserts an element into the list at the specified index and raises the <see cref="CollectionChanged"/> event.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        public new void Insert(int index, T item)
        {
            base.Insert(index, item);

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the list and raises the <see cref="CollectionChanged"/> event.
        /// </summary>
        /// <param name="item">The object to remove from the list.</param>
        /// <returns>true if item is successfully removed; otherwise, false.</returns>
        public new bool Remove(T item)
        {
            var index = this.IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            base.Remove(item);

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));

            return true;
        }

        /// <summary>
        /// Removes a range of elements from the list and raises the <see cref="CollectionChanged"/> event.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is less than 0 or index + count is greater than the number of elements in the list.</exception>
        public new void RemoveRange(int index, int count)
        {
            if (index < 0 || index + count > this.Count)
            {
                throw new ArgumentOutOfRangeException($"Starting index {index} and count of elements {count} is out of range");
            }

            var removedItems = this.GetRange(index, count);

            base.RemoveRange(index, count);

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedItems, index));
        }

        /// <summary>
        /// Removes the element at the specified index and raises the <see cref="CollectionChanged"/> event.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public new void RemoveAt(int index)
        {
            var item = base[index];

            base.RemoveAt(index);

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }
    }
}