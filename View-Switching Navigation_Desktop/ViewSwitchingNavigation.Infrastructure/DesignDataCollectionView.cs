

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewSwitchingNavigation.Infrastructure
{
    /// <summary>
    /// This class is used to specify design data for view models that expose an ICollectionView. It is not intended
    /// to be used by production code.
    /// </summary>
    public class DesignDataCollectionView : Collection<object>, ICollectionView
    {
        public bool CanFilter
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanGroup
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanSort
        {
            get { throw new NotImplementedException(); }
        }

        public System.Globalization.CultureInfo Culture
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

#pragma warning disable 67    
        // Implements ICollectionView and is here only to support design-time data only.
        // It's no surprise no one actually uses these events
        public event EventHandler CurrentChanged;

        public event CurrentChangingEventHandler CurrentChanging;
#pragma warning restore 67

        public object CurrentItem
        {
            get { throw new NotImplementedException(); }
        }

        public int CurrentPosition
        {
            get { throw new NotImplementedException(); }
        }

        public IDisposable DeferRefresh()
        {
            throw new NotImplementedException();
        }

        public Predicate<object> Filter
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ObservableCollection<GroupDescription> GroupDescriptions
        {
            get { throw new NotImplementedException(); }
        }

        public ReadOnlyObservableCollection<object> Groups
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsCurrentAfterLast
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsCurrentBeforeFirst
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsEmpty
        {
            get { throw new NotImplementedException(); }
        }

        public bool MoveCurrentTo(object item)
        {
            throw new NotImplementedException();
        }

        public bool MoveCurrentToFirst()
        {
            throw new NotImplementedException();
        }

        public bool MoveCurrentToLast()
        {
            throw new NotImplementedException();
        }

        public bool MoveCurrentToNext()
        {
            throw new NotImplementedException();
        }

        public bool MoveCurrentToPosition(int position)
        {
            throw new NotImplementedException();
        }

        public bool MoveCurrentToPrevious()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public SortDescriptionCollection SortDescriptions
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.IEnumerable SourceCollection
        {
            get { throw new NotImplementedException(); }
        }

#pragma warning disable 67
        // Implements ICollectionView and is here only to support design-time data only.
        // It's no surprise no one actually uses this event.
        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;
#pragma warning restore 67
    }
}
