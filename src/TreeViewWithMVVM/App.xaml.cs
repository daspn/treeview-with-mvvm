using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Cinch;

namespace TreeViewWithMVVM
{
    public partial class App : Application
    {
        public App()
        {
            CinchBootStrapper.Initialise(new List<Assembly> { typeof(App).Assembly });
        }
    }
}