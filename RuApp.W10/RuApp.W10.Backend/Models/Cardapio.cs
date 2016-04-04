using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuApp.W10.Backend.Models
{
	public class Cardapio: INotifyPropertyChanged

	{

		private string _Id;
		private string _periodo;
		private string _data;

		public string Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id = value;
				RaisePropertyChanged("Id");
			}

		}
		public string periodo
		{
			get
			{
				return _periodo;
			}
			set
			{
				_periodo = value;
				RaisePropertyChanged("periodo");
			}
		}
		public string data
		{
			get
			{
				return _data;
			}
			set
			{
				_data = value;
				RaisePropertyChanged("data");
			}
		}
		public ObservableCollection<CardapioDataItem> items { get; private set; }

		public Cardapio(string id, string periodo, string data)
		{
			this.Id = id;
			this.periodo = periodo;
			this.data = data;
			this.items = new ObservableCollection<CardapioDataItem>();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		public override string ToString()
		{
			return this.periodo;
		}
		public string GetData()
		{

			return this.data;
		}
	}

	public class CardapioDataItem:INotifyPropertyChanged
	{
		private string _Id;
		private string _tipo;
		private string _prato;

		public string Id
		{
			get
			{
				return _Id;
			}

			set
			{
				_Id = value;
				RaisePropertyChanged("Id");
			}
		}
		public string tipo
		{
			get
			{
				return _tipo;
			}
			set
			{
				_tipo = value;
				RaisePropertyChanged("tipo");
			}

		}
		public string prato
		{
			get
			{
				return _prato;
			}
			set
			{
				_prato = value;
				RaisePropertyChanged("prato");
			}

		}

		public CardapioDataItem(string id, string tipo, string prato)
		{
			this.Id = id;
			this.tipo = tipo;
			this.prato = prato;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		public override string ToString()
		{
			return this.tipo;
		}
	}
}
