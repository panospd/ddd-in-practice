﻿namespace DddInPractice.UI.Common
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            var viewModel = new SnackMachineViewModel(new Logic.SnackMachine());
            _dialogService.ShowDialog(viewModel);
        }
    }
}
