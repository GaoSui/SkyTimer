using System;
using System.Collections.Specialized;
using System.ComponentModel;
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

        protected override void OnDetaching()
        {
            dpd.RemoveValueChanged(AssociatedObject, itemsChanged);
        }

        private DependencyPropertyDescriptor dpd;

        private void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            registerEvent();

            dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(ListBox));
            dpd.AddValueChanged(AssociatedObject, itemsChanged);

            if (AssociatedObject.Items.Count == 0) return;
            AssociatedObject.ScrollIntoView(AssociatedObject.Items[AssociatedObject.Items.Count - 1]);
        }

        private void itemsChanged(object sender, EventArgs e)
        {
            registerEvent();
        }

        private void registerEvent()
        {
            var colle = AssociatedObject.ItemsSource as INotifyCollectionChanged;
            colle.CollectionChanged -= BringIntoViewBehavior_CollectionChanged;
            colle.CollectionChanged += BringIntoViewBehavior_CollectionChanged;
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
