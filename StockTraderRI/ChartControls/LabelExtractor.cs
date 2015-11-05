

using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System;
using System.Windows.Controls;
using System.Collections.Specialized;

namespace StockTraderRI.ChartControls
{
    public class LabelExtractor : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new LabelExtractor();
        }

        private static void OnItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            LabelExtractor v = sender as LabelExtractor;
            ItemCollection oldItems = args.OldValue as ItemCollection;
            ItemCollection newItems = args.NewValue as ItemCollection;
            if (oldItems != null)
                ((INotifyCollectionChanged)oldItems).CollectionChanged -= new NotifyCollectionChangedEventHandler(v.OnLabelsCollectionChanged);

            if (v != null && v.Items != null)
            {
                ((INotifyCollectionChanged)v.Items).CollectionChanged += new NotifyCollectionChangedEventHandler(v.OnLabelsCollectionChanged);
                if (v.LabelPath != null)
                    v.GenerateLabelList();
            }
        }

        private static void OnLabelPathChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            LabelExtractor v = sender as LabelExtractor;
            if (v != null && v.LabelPath != null && v.Items != null)
            {
                v.GenerateLabelList();
            }
        }
        
        private void OnLabelsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action.Equals(NotifyCollectionChangedAction.Reset))
            {
                GenerateLabelList();
            }
            else if (e.Action.Equals(NotifyCollectionChangedAction.Remove))
            {
                for (int i = 0; i < e.OldItems.Count; i++)
                {
                    Labels.RemoveAt(e.OldStartingIndex);
                }
            }
            else if (e.Action.Equals(NotifyCollectionChangedAction.Move))
            {
                Labels.Move(e.OldStartingIndex, e.NewStartingIndex);
            }
            else
            {
                for (int i = 0; i < e.NewItems.Count; i++)
                {
                    CreateInternalBinding(Items[e.NewStartingIndex + i]);
                    if (e.Action.Equals(NotifyCollectionChangedAction.Add))
                        Labels.Insert(e.NewStartingIndex + i, LabelHolder);
                    else
                        Labels[e.NewStartingIndex + i] = LabelHolder;
                }
            }
        }
        
        private void GenerateLabelList()
        {
            SetValue(LabelsKey, new ObservableCollection<string>());

            ObservableCollection<String> tempLabels = Labels;
            foreach (Object o in Items)
            {
                CreateInternalBinding(o);
                tempLabels.Add((String)LabelHolder);
            }
        }

        private void CreateInternalBinding(Object source)
        {
            Binding b = new Binding();
            b.Source = source;
            if (IsXmlNodeHelper(source))
                b.XPath = LabelPath.Path;
            else
                b.Path = LabelPath;
            BindingOperations.SetBinding(this, LabelExtractor.LabelHolderProperty, b);
        }

        private static bool IsXmlNodeHelper(object item)
        {
            return item is System.Xml.XmlNode;
        }

        public ItemCollection Items
        {
            get { return (ItemCollection)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ItemCollection), typeof(LabelExtractor), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnItemsChanged)));


        public PropertyPath LabelPath
        {
            get { return (PropertyPath)GetValue(LabelPathProperty); }
            set { SetValue(LabelPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelPathProperty =
            DependencyProperty.Register("LabelPath", typeof(PropertyPath), typeof(LabelExtractor), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnLabelPathChanged)));


        public ObservableCollection<String> Labels
        {
            get { return (ObservableCollection<String>)GetValue(LabelsProperty); }
        }

        // Using a DependencyProperty as the backing store for Labels.  This enables animation, styling, binding, etc...
        private static readonly DependencyPropertyKey LabelsKey =
            DependencyProperty.RegisterReadOnly("Labels", typeof(ObservableCollection<String>), typeof(LabelExtractor), new UIPropertyMetadata(null));

        public static readonly DependencyProperty LabelsProperty = LabelsKey.DependencyProperty;

        private String LabelHolder
        {
            get { return (String)GetValue(LabelHolderProperty); }
            set { SetValue(LabelHolderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentLabel.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty LabelHolderProperty =
            DependencyProperty.Register("CurrentLabel", typeof(String), typeof(LabelExtractor), new UIPropertyMetadata(null));



    }
}