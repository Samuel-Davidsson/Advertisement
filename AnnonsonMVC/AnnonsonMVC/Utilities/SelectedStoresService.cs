using AnnonsonMVC.ViewModels;
using Domain.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace AnnonsonMVC.Utilities
{
    public class SelectedStoresService
    {
        public List<int> GetSelectedStoresList(ArticelViewModel model, IEnumerable<Store> stores)
        {
            var selectedStores = stores.Select(x => new SelectListItem { Value = x.StoreId.ToString(), Text = x.Name }).ToList();
            model.Stores = selectedStores;
            List<SelectListItem> selectedStoreList = model.Stores.Where(x => model.StoreIds.Contains(int.Parse(x.Value))).ToList();

            foreach (var selecteditem in selectedStoreList)
            {
                selecteditem.Selected = true;
            }

            var selectedStoreListIds = selectedStoreList.Select(x => x.Value).Select(x => int.Parse(x)).ToList();

            return selectedStoreListIds;
        }
    }
}
