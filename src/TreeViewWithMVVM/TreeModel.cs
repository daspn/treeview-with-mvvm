using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TreeViewWithMVVM
{
    public abstract class TreeModel<T1> : INotifyPropertyChanged
    {
        #region ctor

        public TreeModel()
        {
            this.IsSelected = false;
            _children = new ObservableCollection<TreeModel<T1>>();
        }

        #endregion ctor

        #region fields

        private TreeModel<T1> _parent;
        protected ObservableCollection<TreeModel<T1>> _children;
        private T1 _selectedValue;
        private string _displayText;
        private bool _isSelected;
        private bool _isExpanded;

        #endregion fields

        #region properties

        public TreeModel<T1> Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                NotifyPropertyChanged("Parent");
            }
        }

        public IEnumerable<TreeModel<T1>> Children
        {
            get { return _children; }
        }

        public T1 SelectedValue
        {
            get { return _selectedValue; }
            set
            {
                _selectedValue = value;
                NotifyPropertyChanged("SelectedValue");
            }
        }

        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                _displayText = value;
                NotifyPropertyChanged("DisplayText");
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                NotifyPropertyChanged("IsExpanded");
            }
        }

        #endregion properties

        #region methods

        public override string ToString()
        {
            return this.DisplayText;
        }

        public void AddChild(TreeModel<T1> child)
        {
            child.Parent = this;
            this._children.Add(child);
        }

        #endregion methods

        #region static methods

        public static TreeModel<T1> GetNodeById(T1 id, IEnumerable<TreeModel<T1>> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.SelectedValue.Equals(id))
                    return node;

                var foundChild = GetNodeById(id, node.Children);
                if (foundChild != null)
                    return foundChild;
            }
            return null;
        }

        public static TreeModel<T1> GetSelectedNode(IEnumerable<TreeModel<T1>> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.IsSelected)
                    return node;

                var selectedChild = GetSelectedNode(node.Children);
                if (selectedChild != null)
                    return selectedChild;
            }

            return null;
        }

        public static void ExpandParentNodes(TreeModel<T1> node)
        {
            if (node.Parent != null)
            {
                node.Parent.IsExpanded = true;
                ExpandParentNodes(node.Parent);
            }
        }

        public static void ToggleExpanded(IEnumerable<TreeModel<T1>> nodes, bool isExpanded)
        {
            foreach (var node in nodes)
            {
                node.IsExpanded = isExpanded;
                ToggleExpanded(node.Children, isExpanded);
            }
        }

        #endregion static methods

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged implementation
    }

    public class TreeModel : TreeModel<Guid>
    {
    }
}