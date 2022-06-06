﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elden_Ring_Debug_Tool_ViewModels.ViewModels;

namespace Elden_Ring_Debug_Tool_ViewModels.Commands
{
    public class DisableEventCommand : CommandBase
    {
        private MiscViewViewModel _miscViewViewModel { get; }

        public DisableEventCommand(MiscViewViewModel miscViewViewModel)
        {
            _miscViewViewModel = miscViewViewModel;
            _miscViewViewModel.PropertyChanged += _debugViewViewModel_PropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _miscViewViewModel.Hook.Setup && _miscViewViewModel.Hook.Loaded && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if (!(parameter is int eventID))
                throw new ArgumentNullException(nameof(parameter), "parameter was null. parameter must be an int");

            _miscViewViewModel.Hook.SetEventFlag(eventID, false);
        }

        private void _debugViewViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(MiscViewViewModel.Setup) 
                or nameof(MiscViewViewModel.Loaded))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
