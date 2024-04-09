// Copyright (c) Digital Cloud Technologies. All rights reserved.

namespace DataTableProj.Views.ContentDialogs
{
    using System;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Code-Behind for DeleteConfirmationDialog.xaml.
    /// </summary>
    public sealed partial class DeleteConfirmationDialog : ContentDialog
    {
        /// <summary>
        /// Action for deleting user.
        /// </summary>
        private readonly Action deleteAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteConfirmationDialog"/> class.
        /// </summary>
        /// <param name="deleteAction">The action to perform when the deletion is confirmed.</param>
        public DeleteConfirmationDialog(Action deleteAction)
        {
            this.InitializeComponent();
            this.deleteAction = deleteAction ?? throw new ArgumentNullException(nameof(deleteAction));
        }

        /// <summary>
        /// Handles the click event of the primary button (Yes) to confirm the deletion.
        /// </summary>
        /// <param name="sender">The <see cref="ContentDialog"/> object that raised the event.</param>
        /// <param name="args">The event data for the button click event.</param>
        private void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.deleteAction();
            this.Hide();
        }

        /// <summary>
        /// Handles the click event of the secondary button (No) to cancel the deletion.
        /// </summary>
        /// <param name="sender">The <see cref="ContentDialog"/> object that raised the event.</param>
        /// <param name="args">The event data for the button click event.</param>
        private void OnSecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }
    }
}
