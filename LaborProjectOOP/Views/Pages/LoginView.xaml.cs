using System.Windows;
using System.Windows.Controls;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для LoginView.xaml
	/// </summary>
	public partial class LoginView : UserControl
	{
		public LoginView()
		{
			InitializeComponent();
		}
		public void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			if (this.DataContext != null)
			{ ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
		}
	}
}
