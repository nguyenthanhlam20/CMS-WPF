﻿using WPFApp.UI.Public.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Constants;

namespace WPFApp.UI.Public.Commands
{
   public class MainWindowCommand
    {
      
        private MainWindowViewModel _viewModel;
        public MainWindowCommand(MainWindowViewModel viewModel) {
            _viewModel = viewModel;
        }

       
       
    }
}
