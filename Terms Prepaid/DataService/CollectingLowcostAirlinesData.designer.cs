﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataService
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="total_services")]
	public partial class CollectingLowcostAirlinesDataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCollectingLowcostAirline(CollectingLowcostAirline instance);
    partial void UpdateCollectingLowcostAirline(CollectingLowcostAirline instance);
    partial void DeleteCollectingLowcostAirline(CollectingLowcostAirline instance);
    #endregion
		
		public CollectingLowcostAirlinesDataDataContext() : 
				base(global::DataService.Properties.Settings.Default.total_servicesConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CollectingLowcostAirlinesDataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CollectingLowcostAirlinesDataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CollectingLowcostAirlinesDataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CollectingLowcostAirlinesDataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CollectingLowcostAirline> CollectingLowcostAirlines
		{
			get
			{
				return this.GetTable<CollectingLowcostAirline>();
			}
		}
		
		public System.Data.Linq.Table<CollectingLowcostAirlinesNote> CollectingLowcostAirlinesNotes
		{
			get
			{
				return this.GetTable<CollectingLowcostAirlinesNote>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CollectingLowcostAirlines")]
	public partial class CollectingLowcostAirline : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Airline;
		
		private string _Check_in;
		
		private string _SeatSelection;
		
		private string _FreeCarryOnBaggage;
		
		private string _Baggage;
		
		private string _Food;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnAirlineChanging(string value);
    partial void OnAirlineChanged();
    partial void OnCheck_inChanging(string value);
    partial void OnCheck_inChanged();
    partial void OnSeatSelectionChanging(string value);
    partial void OnSeatSelectionChanged();
    partial void OnFreeCarryOnBaggageChanging(string value);
    partial void OnFreeCarryOnBaggageChanged();
    partial void OnBaggageChanging(string value);
    partial void OnBaggageChanged();
    partial void OnFoodChanging(string value);
    partial void OnFoodChanged();
    #endregion
		
		public CollectingLowcostAirline()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Airline", DbType="VarChar(30)")]
		public string Airline
		{
			get
			{
				return this._Airline;
			}
			set
			{
				if ((this._Airline != value))
				{
					this.OnAirlineChanging(value);
					this.SendPropertyChanging();
					this._Airline = value;
					this.SendPropertyChanged("Airline");
					this.OnAirlineChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Check_in", DbType="VarChar(500)")]
		public string Check_in
		{
			get
			{
				return this._Check_in;
			}
			set
			{
				if ((this._Check_in != value))
				{
					this.OnCheck_inChanging(value);
					this.SendPropertyChanging();
					this._Check_in = value;
					this.SendPropertyChanged("Check_in");
					this.OnCheck_inChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SeatSelection", DbType="VarChar(500)")]
		public string SeatSelection
		{
			get
			{
				return this._SeatSelection;
			}
			set
			{
				if ((this._SeatSelection != value))
				{
					this.OnSeatSelectionChanging(value);
					this.SendPropertyChanging();
					this._SeatSelection = value;
					this.SendPropertyChanged("SeatSelection");
					this.OnSeatSelectionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FreeCarryOnBaggage", DbType="VarChar(500)")]
		public string FreeCarryOnBaggage
		{
			get
			{
				return this._FreeCarryOnBaggage;
			}
			set
			{
				if ((this._FreeCarryOnBaggage != value))
				{
					this.OnFreeCarryOnBaggageChanging(value);
					this.SendPropertyChanging();
					this._FreeCarryOnBaggage = value;
					this.SendPropertyChanged("FreeCarryOnBaggage");
					this.OnFreeCarryOnBaggageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Baggage", DbType="VarChar(500)")]
		public string Baggage
		{
			get
			{
				return this._Baggage;
			}
			set
			{
				if ((this._Baggage != value))
				{
					this.OnBaggageChanging(value);
					this.SendPropertyChanging();
					this._Baggage = value;
					this.SendPropertyChanged("Baggage");
					this.OnBaggageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Food", DbType="VarChar(500)")]
		public string Food
		{
			get
			{
				return this._Food;
			}
			set
			{
				if ((this._Food != value))
				{
					this.OnFoodChanging(value);
					this.SendPropertyChanging();
					this._Food = value;
					this.SendPropertyChanged("Food");
					this.OnFoodChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CollectingLowcostAirlinesNotes")]
	public partial class CollectingLowcostAirlinesNote
	{
		
		private string _Note;
		
		private string _Text;
		
		public CollectingLowcostAirlinesNote()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Note", DbType="VarChar(10)")]
		public string Note
		{
			get
			{
				return this._Note;
			}
			set
			{
				if ((this._Note != value))
				{
					this._Note = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text", DbType="VarChar(500)")]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				if ((this._Text != value))
				{
					this._Text = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
