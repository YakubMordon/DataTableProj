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

            this.PrimaryButtonClick += this.OnButtonClick;

            this.SecondaryButtonClick += (sender, args) =>
            {
                sender.IsPrimaryButtonEnabled = false;
                this.OnButtonClick(sender, args);
            };
        }

        /// <summary>
        /// Handles the click event of the primary button (Yes) to confirm the deletion.
        /// </summary>
        /// <param name="sender">The <see cref="ContentDialog"/> object that raised the event.</param>
        /// <param name="args">The event data for the button click event.</param>
        private void OnButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (sender.IsPrimaryButtonEnabled)
            {
                this.deleteAction();
            }

            this.Hide();
        }
    }
}
