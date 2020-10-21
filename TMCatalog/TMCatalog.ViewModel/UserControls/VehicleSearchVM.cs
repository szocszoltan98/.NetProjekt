using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMCatalog.Common.MVVM;
using TMCatalog.Logic;
using TMCatalogClient.Model;

namespace TMCatalog.ViewModel.UserControls
{
    public class VehicleSearchVM : ViewModelBase
    {
        private List<Manufacturer> manufacturers { get; set; }
        public VehicleSearchVM()
        {
            this.manufacturers = Data.Catalog.GetManufacturers();
        }
        public List<Manufacturer> Manufacturers {
            get
            {
                return manufacturers;
            }
            set
            {
                this.manufacturers = value;
                this.RaisePropertyChanged();
            }
        }
    }
}
