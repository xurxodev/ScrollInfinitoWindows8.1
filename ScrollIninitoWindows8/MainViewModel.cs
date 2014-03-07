using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrollIninitoWindows8
{
    public class MainViewModel
    {
        private uint itemsCount;

        public PaginatedCollection<Item> Items { get; set; }

        public MainViewModel()
        {
            Items = new PaginatedCollection<Item>(LoadData);
        }

        private async Task<IEnumerable<Item>> LoadData(uint count)
        {
            List<Item> products = new List<Item>();

            if (itemsCount < 150)
            {

                for (int i = 0; i < count; i++)
                {
                    products.Add(new Item() { Title = "Titulo del item" + (itemsCount + (i + 1)) });
                }
            }
            itemsCount += count;

            return products;
        }
    }
}
