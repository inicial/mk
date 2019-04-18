using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Behaviors
{
    public sealed class FlowDocumentScrollViewerBehavior : Behavior<FlowDocumentScrollViewer>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += OnLoaded;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= OnLoaded;
            base.OnDetaching();
        }
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ScrollToTop();
        }
        public void ScrollToBottom()
        {
            var sv = ScrollHelper.FindScrollViewer(AssociatedObject);
            if(sv != null) sv.ScrollToBottom();
        }
        public void ScrollToTop()
        {
            var sv = ScrollHelper.FindScrollViewer(AssociatedObject);
            if (sv != null) sv.ScrollToTop();
        }
    }
}