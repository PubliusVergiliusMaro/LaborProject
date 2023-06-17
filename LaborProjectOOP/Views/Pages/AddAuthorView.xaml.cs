using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.WishListServices;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для AddAuthorView.xaml
	/// </summary>
	public partial class AddAuthorView : UserControl
	{
		public AddAuthorView()
		{
			InitializeComponent();		
		}
	}
}
