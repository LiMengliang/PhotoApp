using PlatformFramework.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using PlatformFramework.Shell.LeftPanel;
using System.Windows.Controls;

namespace PlatformFramework.Core.Composition
{
    public class CompositionHost
    {
        private CompositionContainer _compositionContainer;

        private static CompositionHost _compositionManager;
        public static CompositionHost Instance()
        {
            if (_compositionManager == null)
            {
                _compositionManager = new CompositionHost();
            }
            return _compositionManager;
        }

        public void InitiailizeComposition(ShellApplication application)
        {
            var aggregateCatalog = new AggregateCatalog();
            // Directory catalog.
            aggregateCatalog.Catalogs.Add(new DirectoryCatalog(new DirectoryInfo(typeof(ShellApplication).Assembly.Location).Parent.FullName));
            _compositionContainer = new CompositionContainer(aggregateCatalog);
            AttributedModelServices.ComposeParts(_compositionContainer, application);
        }

        public IEnumerable<Lazy<T>> GetExports<T>()
        {
            return _compositionContainer.GetExports<T>();
        }

        public IEnumerable<Lazy<T, TMetadata>> GetExports<T, TMetadata>()
        {
            return _compositionContainer.GetExports<T, TMetadata>();
        }
    }
}
