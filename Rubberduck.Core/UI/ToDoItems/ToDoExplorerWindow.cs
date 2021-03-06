﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using Rubberduck.Resources;

namespace Rubberduck.UI.ToDoItems
{
    [ExcludeFromCodeCoverage]
    public sealed partial class ToDoExplorerWindow : UserControl, IDockableUserControl
    {
        private const string ClassId = "8B071EDA-2C9C-4009-9A22-A1958BF98B28"; // todo get from Resources.Registration?
        string IDockableUserControl.ClassId { get { return ClassId; } }
        string IDockableUserControl.Caption { get { return RubberduckUI.TodoExplorer_Caption; } }

        private ToDoExplorerWindow()
        {
            InitializeComponent();
        }

        public ToDoExplorerWindow(ToDoExplorerViewModel viewModel) : this()
        {
            _viewModel = viewModel;
            TodoExplorerControl.DataContext = _viewModel;
        }

        private readonly ToDoExplorerViewModel _viewModel;
        public ToDoExplorerViewModel ViewModel
        {
            get { return _viewModel; }
        }
    }
}
