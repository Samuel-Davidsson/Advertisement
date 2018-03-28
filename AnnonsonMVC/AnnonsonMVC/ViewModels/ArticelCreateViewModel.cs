using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnnonsonMVC.ViewModels
{
    public class ArticelCreateViewModel
    {
        public ArticelViewModel Articel { get; set; }
        public CategoryViewModel Category { get; set; }
        public CompanyViewModel Company { get; set; }
    }
}
