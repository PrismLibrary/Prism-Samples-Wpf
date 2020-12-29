﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using UsingCompositeCommands.Core;

namespace UsingCompositeCommands.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Unity Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private IApplicationCommands _applicationCommands;
        public IApplicationCommands ApplicationCommands
        {
            get { return _applicationCommands; }
            set { SetProperty(ref _applicationCommands, value); }
        }

        public MainWindowViewModel(IApplicationCommands applicationCommands)
        {
            ApplicationCommands = applicationCommands;

            var updateCommand = new DelegateCommand(Update);
            _applicationCommands.SaveCommand.RegisterCommand(updateCommand);
        }

        private void Update()
        {
            Title = $"Updated By {nameof(MainWindowViewModel)}: {DateTime.Now}";
        }

    }
}
