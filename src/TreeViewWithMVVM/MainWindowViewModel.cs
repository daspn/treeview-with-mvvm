using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Cinch;
using MEFedMVVM.ViewModelLocator;

namespace TreeViewWithMVVM
{
    [ExportViewModel("MainWindowViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IViewAwareStatus _viewAwareStatusService;

        public SimpleCommand<object, EventToCommandArgs> ExpandAllCommand { get; private set; }

        public SimpleCommand<object, EventToCommandArgs> CollapseAllCommand { get; private set; }

        public SimpleCommand<object, EventToCommandArgs> GetSelectedCommand { get; private set; }

        [ImportingConstructor]
        public MainWindowViewModel(IViewAwareStatus viewAwareStatusService)
        {
            ExpandAllCommand = new SimpleCommand<object, EventToCommandArgs>((commandParameter) =>
            {
                TreeModel.ToggleExpanded(this.ItemsSource, true);
            });

            CollapseAllCommand = new SimpleCommand<object, EventToCommandArgs>((commandParameter) =>
            {
                TreeModel.ToggleExpanded(this.ItemsSource, false);
            });

            GetSelectedCommand = new SimpleCommand<object, EventToCommandArgs>((commandParameter) =>
            {
                var selectedNode = TreeModel.GetSelectedNode(this.ItemsSource);
                this.SelectedDisplayText = selectedNode != null ? selectedNode.DisplayText : "none selected";
            });

            this._viewAwareStatusService = viewAwareStatusService;
            this._viewAwareStatusService.ViewLoaded += ViewAwareStatusService_ViewLoaded;
        }

        private void ViewAwareStatusService_ViewLoaded()
        {
            this.ItemsSource = DataService.GetData();
        }

        private ObservableCollection<TreeModel> _itemsSource = null;

        private static PropertyChangedEventArgs _itemsSourceChangeArgs = ObservableHelper.CreateArgs<MainWindowViewModel>(x => x.ItemsSource);

        public ObservableCollection<TreeModel> ItemsSource
        {
            get { return this._itemsSource; }
            private set
            {
                this._itemsSource = value;
                NotifyPropertyChanged(_itemsSourceChangeArgs);
            }
        }

        private string _selectedDisplayText;

        private static PropertyChangedEventArgs _selectedDisplayTextChangeArgs = ObservableHelper.CreateArgs<MainWindowViewModel>(x => x.SelectedDisplayText);

        public string SelectedDisplayText
        {
            get { return this._selectedDisplayText; }
            private set
            {
                this._selectedDisplayText = value;
                NotifyPropertyChanged(_selectedDisplayTextChangeArgs);
            }
        }
    }
}