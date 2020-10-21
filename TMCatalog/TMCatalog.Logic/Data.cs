using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCatalog.Logic
{
    public static class Data//el kell erni barhonnan
    {
        static Data()
        {
            Catalog = new CatalogController();
        }

        public static CatalogController Catalog { get; }

    }
}
