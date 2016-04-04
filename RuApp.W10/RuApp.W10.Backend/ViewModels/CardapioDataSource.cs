using Newtonsoft.Json;
using RuApp.W10.Backend.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace RuApp.W10.Backend.Services
{
    public class CardapioDataSource
	{
		private static readonly CardapioDataSource _cardapioDataSource = new CardapioDataSource();
		public static CardapioDataSource cardapioDataSource
		{
			get
			{
				return _cardapioDataSource;
			}
		}
		private ObservableCollection<Cardapio> _grupos = new ObservableCollection<Cardapio>();
		public ObservableCollection<Cardapio> Grupos
		{
			set
			{
				_grupos = value;
			}
			get {return this._grupos; }
		}

		private static  ObservableDictionary _defaultViewModel = new ObservableDictionary();

		public static ObservableDictionary DefaultViewModel
		{
			get { return _defaultViewModel; }
		}
	
		public static async Task GetAsync()
		{
			await _cardapioDataSource.GetCardapioAsync();
            _defaultViewModel["Groups"] = _cardapioDataSource.Grupos;
        }
		private async Task GetCardapioAsync()
		{
			try
			{
				using (var client = new System.Net.Http.HttpClient())
				{

					client.DefaultRequestHeaders.Host = "www.ruufmt.tk";
					client.DefaultRequestHeaders.Add("Accept-Language", "de,en;q=0.7,en-us;q=0.3");
					var resposta = await client.GetAsync("http://ruufmt.tk/api/cardapio/").Result.Content.ReadAsStringAsync();
					var lido = JsonConvert.DeserializeObject<ObservableCollection<Cardapio>>(resposta);
					Grupos = new ObservableCollection<Cardapio>(lido);
					_defaultViewModel["Groups"] = _cardapioDataSource.Grupos;
					await StoreCardapio(resposta);
					GC.SuppressFinalize(resposta);
					GC.SuppressFinalize(lido);

				}
			}
			catch
			{

				await GetCardapioAsyncOffline();
			}
		}
		private async Task GetCardapioAsyncOffline()
		{
			try
			{
				StorageFolder localFolder = ApplicationData.Current.LocalFolder;
				StorageFile file = await localFolder.GetFileAsync("CardapioDataSource.txt");
				string jsonText = await FileIO.ReadTextAsync(file);
				var lido = JsonConvert.DeserializeObject<ObservableCollection<Cardapio>>(jsonText);
                Grupos = new ObservableCollection<Cardapio>(lido);
			}
			catch
			{
				Uri dataUri = new Uri("ms-appx:///RuApp.W10.Backend/Models/SampleData.txt");
				StorageFile file2 = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
				string jsonText2 = await FileIO.ReadTextAsync(file2);
				var lido = JsonConvert.DeserializeObject<ObservableCollection<Cardapio>>(jsonText2);
                Grupos = new ObservableCollection<Cardapio>(lido);
			}
		}
		public async Task StoreCardapio(string cardapio)
		{
			StorageFolder localFolder = ApplicationData.Current.LocalFolder;
			StorageFile file = await localFolder.CreateFileAsync("CardapioDataSource.txt", CreationCollisionOption.ReplaceExisting);
			try
			{
				if (file != null)
				{
					await FileIO.WriteTextAsync(file, cardapio);
				}
			}
			catch (FileNotFoundException)
			{
			}
		}
	}
}
