using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace ScrollIninitoWindows8
{
    public class PaginatedCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private Func<uint, Task<IEnumerable<T>>> load;
        public bool HasMoreItems { get; protected set; }

        public PaginatedCollection(Func<uint, Task<IEnumerable<T>>> load)
        {
            HasMoreItems = true;
            this.load = load;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return Task.Run<LoadMoreItemsResult>(async () =>
            {
                var data = await load(count);

                foreach (var item in data)
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                    () =>
                    {
                        Add(item);
                    });
                }

                HasMoreItems = data.Any();

                return new LoadMoreItemsResult()
                {
                    Count = (uint)data.Count(),
                };
            }).AsAsyncOperation<LoadMoreItemsResult>();
        }
    }
}
