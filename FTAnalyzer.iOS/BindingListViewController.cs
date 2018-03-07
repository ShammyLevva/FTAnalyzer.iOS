using Foundation;
using FTAnalyzer.Utilities;
using UIKit;

namespace FTAnalyzer.iOS
{
    public class BindingListViewController<T> : UIViewController
    {
        NSTableView _tableView;

        public BindingListViewController(string title)
        {
            SetupView(title);
            Title = title;
        }

        void SetupView(string title)
        {
            _tableView = new UITableView
            {
                Identifier = title,
                RowSizeStyle = NSTableViewRowSizeStyle.Default,
                Enabled = true,
                UsesAlternatingRowBackgroundColors = true,
                ColumnAutoresizingStyle = UITableViewColumnAutoresizingStyle.None,
                Bounds = new CoreGraphics.CGRect(0, 0, 500, 500),
                AutoresizingMask = UIViewResizingMask.HeightSizable | UIViewResizingMask.WidthSizable
            };

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var tableColumn = new UITableColumn
                {
                    Identifier = property.Name,
                    Width = 100,
                    Editable = false,
                    Hidden = false,
                    Title = property.Name
                };
                _tableView.AddColumn(tableColumn);
            }
            var scrollView = new UIScrollView
            {
                DocumentView = _tableView
            };
            View = scrollView;
        }

        public void RefreshDocumentView(SortableBindingList<T> list)
        {
            if (!NSThread.IsMain)
            {
                InvokeOnMainThread(() => RefreshDocumentView(list));
                return;
            }

            var source = new BindingListTableSource<T>(list);
            _tableView.Source = source;
            _tableView.ReloadData();
        }

    }
}
