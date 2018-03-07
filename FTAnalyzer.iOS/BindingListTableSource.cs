using System;
using System.Linq;
using System.Reflection;
using UIKit;
using FTAnalyzer.Utilities;

namespace FTAnalyzer.iOS
{
    public class BindingListTableSource<T> : UITableViewSource
    {
        const string CellIdentifier = "TableView";

        readonly SortableBindingList<T> _bindingList;
        readonly PropertyInfo[] _properties;
        readonly string[] _fieldNames;

        public BindingListTableSource(SortableBindingList<T> bindingList)
        {
            _bindingList = bindingList;
            _properties = typeof(T).GetProperties();
            _fieldNames = typeof(T).GetProperties().Select(f => f.Name).ToArray();
        }

        public override nint GetRowCount(UITableView tableView)
        {
            return _bindingList.Count;
        }

        public override NSView GetViewForItem(UITableView tableView, UITableColumn tableColumn, nint row)
        {
            var view = tableView.MakeView(CellIdentifier, this) as UITextField;
            if (view == null)
            {
                view = new UITextField
                {
                    Identifier = CellIdentifier,
                    BackgroundColor = UIColor.Clear,
                    Bordered = false,
                    Selectable = false,
                    Editable = false
                };
            }

            // Setup view based on the column selected
            var item = _bindingList[(int)row];
            var index = Array.IndexOf(_fieldNames, tableColumn.Title);
            var propertyValue = _properties[index].GetValue(item);
            view.StringValue = propertyValue.ToString();

            return view;
        }
    }
}
