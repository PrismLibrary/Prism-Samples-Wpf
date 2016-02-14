// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;


using Commanding.Modules.Order.Properties;
using Commanding.Modules.Order.Services;
using Prism.Commands;
using Prism.Events;

namespace Commanding.Modules.Order.PresentationModels
{
    /// <summary>
    /// Presentation model that represents an Order entity.
    /// </summary>
    public partial class OrderViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly IDictionary<string, string> errors = new Dictionary<string, string>();
        private readonly Services.Order _order;

        public OrderViewModel(Services.Order order)
        {
            _order = order;

            //TODO: 01 - Each Order defines a Save command.
            this.SaveOrderCommand = new DelegateCommand<object>( this.Save, this.CanSave );

            // Track all property changes so we can validate.
            this.PropertyChanged += this.OnPropertyChanged;

            this.Validate();
        }

        #region Order Properties - OrderName, DeliveryDate, Quantity, Price, Shipping, Total

        public string OrderName
        {
            get { return _order.Name; }
            set { _order.Name = value; NotifyPropertyChanged( "OrderName" ); }
        }

        public DateTime DeliveryDate
        {
            get { return _order.DeliveryDate; }
            set
            {
                if ( _order.DeliveryDate != value )
                {
                    _order.DeliveryDate = value;
                    this.NotifyPropertyChanged( "DeliveryDate" );
                }
            }
        }

        public int? Quantity
        {
            get { return _order.Quantity; }
            set
            {
                if ( _order.Quantity != value )
                {
                    _order.Quantity = value;
                    this.NotifyPropertyChanged( "Quantity" );
                }
            }
        }

        public decimal? Price
        {
            get { return _order.Price; }
            set
            {
                if ( _order.Price != value )
                {
                    _order.Price = value;
                    this.NotifyPropertyChanged( "Price" );
                }
            }
        }

        public decimal? Shipping
        {
            get { return _order.Shipping; }
            set
            {
                if ( _order.Shipping != value )
                {
                    _order.Shipping = value;
                    this.NotifyPropertyChanged( "Shipping" );
                }
            }
        }

        public decimal Total
        {
            get
            {
                if (this.Price != null && this.Quantity != null)
                {
                    return (this.Price.Value * this.Quantity.Value) + (this.Shipping ?? 0);
                }
                return 0;
            }
        }

        #endregion

        private void OnPropertyChanged( object sender, PropertyChangedEventArgs e )
        {
            // Total is a calculated property based on price, quantity and shipping cost.
            // If any of these properties change, then notify the view.
            string propertyName = e.PropertyName;
            if ( propertyName == "Price" || propertyName == "Quantity" || propertyName == "Shipping" )
            {
                this.NotifyPropertyChanged( "Total" );
            }

            // Validate and update the enabled status of the SaveOrder
            // command whenever any property changes.
            this.Validate();
            this.SaveOrderCommand.RaiseCanExecuteChanged();
        }

        #region SaveOrder Command

        public event EventHandler<DataEventArgs<OrderViewModel>> Saved;

        public DelegateCommand<object> SaveOrderCommand { get; private set; }

        private bool CanSave( object arg )
        {
            //TODO: 02 - The Order Save command is enabled only when all order data is valid.
            // Can only save when there are no errors and
            // when the order quantity is greater than zero.
            return this.errors.Count == 0 && this.Quantity > 0;
        }

        private void Save( object obj )
        {
            // Save the order here.
            Console.WriteLine( String.Format( CultureInfo.InvariantCulture, "{0} saved.", this.OrderName ) );

            // Notify that the order was saved.
            this.OnSaved(new DataEventArgs<OrderViewModel>(this));
        }

        private void OnSaved(DataEventArgs<OrderViewModel> e)
        {
            EventHandler<DataEventArgs<OrderViewModel>> savedHandler = this.Saved;
            if ( savedHandler != null )
            {
                savedHandler( this, e );
            }
        }

        #endregion

        #region IDataErrorInfo Interface

        public string this[ string columnName ]
        {
            get
            {
                if ( this.errors.ContainsKey( columnName ) )
                {
                    return this.errors[columnName];
                }
                return null;
            }
            set
            {
                this.errors[columnName] = value;
            }
        }

        public string Error
        {
            get
            {
                // Not implemented because we are not consuming it in this quick start. 
                // Instead, we are displaying error messages at the item level. 
                throw new NotImplementedException();
            }
        }

        #endregion

        private void Validate()
        {
            if ( this.Price == null || this.Price <= 0 )
            {
                this["Price"] = Resources.InvalidPriceRange;
            }
            else
            {
                this.ClearError("Price");
            }

            if ( this.Quantity == null || this.Quantity <= 0 )
            {
                this["Quantity"] = Resources.InvalidQuantityRange;
            }
            else
            {
                this.ClearError("Quantity");
            }
        }

        private void ClearError( string columnName )
        {
            if ( this.errors.ContainsKey( columnName ) )
            {
                this.errors.Remove( columnName );
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged( string propertyName )
        {
            if ( this.PropertyChanged != null )
            {
                this.PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }

        #endregion
    }
}
