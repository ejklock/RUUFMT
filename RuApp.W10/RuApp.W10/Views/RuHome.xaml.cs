using RuApp.W10.Backend.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RuApp.W10.Views
{
	public sealed partial class RuHome : Page
	{
		
		public RuHome()
		{

			this.InitializeComponent();
			this.NavigationCacheMode = NavigationCacheMode.Required;
		}


		private void BotaoHamburger_Click(object sender, RoutedEventArgs e)
		{
			split.IsPaneOpen = !split.IsPaneOpen;
		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Cardapio.IsSelected) { SplitFrameContent.Navigate(typeof(ViewCardapio)); }
			else { if (Info.IsSelected) { SplitFrameContent.Navigate(typeof(ViewInfo)); } }

		}

        public void setContext()
        {
            this.DataContext = CardapioDataSource.DefaultViewModel;
        }

		private async void appBarButton_Click(object sender, RoutedEventArgs e)
		{


			await CardapioDataSource.GetAsync();
            setContext();
		}

		protected override async void OnNavigatedTo(NavigationEventArgs e)
		{
			SplitFrameContent.Navigate(typeof(ViewCardapio));
			await CardapioDataSource.GetAsync();
            setContext();
		}



	}
}
