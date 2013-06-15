using System;
using System.Collections.ObjectModel;

namespace TreeViewWithMVVM
{
    /// <summary>
    /// Fake data service for illustration purposes only
    /// </summary>
    internal class DataService
    {
        /// <summary>
        /// Provides some hierarquical data
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<TreeModel> GetData()
        {
            var data = new ObservableCollection<TreeModel>();

            for (int i = 1; i <= 3; i++)
            {
                var node = new TreeModel() { DisplayText = string.Format("Item {0}", i), SelectedValue = Guid.NewGuid() };

                for (int j = 1; j <= 3; j++)
                {
                    var child = new TreeModel() { DisplayText = string.Format("Item {0}.{1}", i, j), SelectedValue = Guid.NewGuid() };
                    node.AddChild(child);

                    for (int k = 1; k <= 3; k++)
                    {
                        child.AddChild(new TreeModel() { DisplayText = string.Format("Item {0}.{1}.{2}", i, j, k), SelectedValue = Guid.NewGuid() });
                    }
                }

                data.Add(node);
            }

            return data;
        }
    }
}