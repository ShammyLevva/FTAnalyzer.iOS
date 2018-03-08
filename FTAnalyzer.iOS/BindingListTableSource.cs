using System;
using System.Linq;
using System.Reflection;
using UIKit;
using FTAnalyzer.Utilities;
using Foundation;
using Xamarin.Forms.DataGrid;

namespace FTAnalyzer.iOS
{
    public class BindingListTableSource<T> : DataGrid
    {
        const string CellIdentifier = "TableCell";

        readonly SortableBindingList<T> _bindingList;
        readonly PropertyInfo[] _properties;
        readonly string[] _fieldNames;
        BindingListViewController<T> _screen;


        public BindingListTableSource(SortableBindingList<T> bindingList, BindingListViewController<T> screen)
        {
            _screen = screen;
            _bindingList = bindingList;
            _properties = typeof(T).GetProperties();
            _fieldNames = typeof(T).GetProperties().Select(f => f.Name).ToArray();
        }

		public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _bindingList.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            var item = _bindingList[indexPath.Row];

            if (cell == null)
            { 
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); 
            }

            var index = Array.IndexOf(_fieldNames, tableColumn.Title);
            var propertyValue = _properties[index].GetValue(item);
            cell.TextLabel.Text = propertyValue.ToString();

            return cell;
        }
    }
}
