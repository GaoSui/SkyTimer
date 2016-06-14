using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace SkyTimer.Helper
{
    public class BringIntoViewBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            (AssociatedObject.ItemsSource as INotifyCollectionChanged).CollectionChanged += BringIntoViewBehavior_CollectionChanged;
            if (AssociatedObject.Items.Count == 0) return;
            AssociatedObject.ScrollIntoView(AssociatedObject.Items[AssociatedObject.Items.Count - 1]);
        }

        private void BringIntoViewBehavior_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                AssociatedObject.ScrollIntoView(e.NewItems[0]);
            }
        }
    }
}
