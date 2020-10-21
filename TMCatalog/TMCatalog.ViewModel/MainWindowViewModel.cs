// ------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="DVSE GmbH">
//   Copyright (c) by DVSE Gesellschaft für Datenverarbeitung, Service und Entwicklung mbH. All rights reserved.
// </copyright>
// <author>Szilveszter Gorgicze</author>
// ------------------------------------------------------------------------------------------------------------------

namespace TMCatalog.ViewModel
{
    using System.Collections.Generic;
    using TMCatalog.Common.MVVM;
  using TMCatalog.Logic;
    using TMCatalog.ViewModel.UserControls;
    using TMCatalogClient.Model;

    public class MainWindowViewModel : ViewModelBase
   {

        private int test = 0;


    public MainWindowViewModel()
    {
            Test = 2;
            this.VehicleSearchVM = new VehicleSearchVM();
      this.CloseCommand = new RelayCommand<string>(this.CloseCommandExecute, this.CloseCommandCanExecute);
    }
        public VehicleSearchVM VehicleSearchVM { get;}

        public RelayCommand<string> CloseCommand { get;}//ezzel csak ctor ban inicializalhato

    public void CloseCommandExecute(string obj)
    {
            this.Test = 3;
            List<Manufacturer> data = Data.Catalog.GetManufacturers();
      //ViewService.CloseDialog(this);
    }

        public bool CloseCommandCanExecute()
        {
            return true;
        }

    public int Test
    {
            get
            {
                return this.test;
            }
            set
            {
                this.test = value;
                this.RaisePropertyChanged();
            }
    }
        
    }
}
