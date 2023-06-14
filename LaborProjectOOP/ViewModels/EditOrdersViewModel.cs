using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.LibrarianServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System;
using LaborProjectOOP.Services.OrderHistoryServices;
using System.Linq;
using System.Threading.Tasks;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class EditOrdersViewModel : ViewModelBase
	{
		public EditOrdersViewModel(IOrderService orderService)
		{
			_orderService = orderService;
            ordrsFromDb = _orderService.GetAll();
            _orders = new ObservableCollection<OrderEntity>(ordrsFromDb);	
			_sortingTypes = new ObservableCollection<OrdersSorting>(Enum.GetValues(typeof(OrdersSorting)).Cast<OrdersSorting>().ToArray());

			DeleteSelectedOrderCommand = new DelegateCommand(DeleteSelectedOrder, IsSelected);
			MakeNotActiveCommand = new DelegateCommand(MakeNotActiveAdminSelected, IsSelected);
			RefreshOrdersCommand = new DelegateCommand(RefreshLibrarians);
		}
       
      
		private async void MakeNotActiveAdminSelected()
		{
			_selectedOrder.IsActual = false;
            await _orderService.Update(_selectedOrder);
			MessageBox.Show("Succesfully make not active");
		}

		private  void RefreshLibrarians()
		{
            Orders.Clear();
            ordrsFromDb = _orderService.GetAll();
            foreach (OrderEntity order in ordrsFromDb)
                _orders.Add(order);
        }

		private bool IsSelected()
		{
			return SelectedOrder != null;
		}

		private async void DeleteSelectedOrder()
		{
			await _orderService.Delete(SelectedOrder.Id);
            MessageBox.Show("Succesfully deleted. Update list.");
        }

		private ObservableCollection<OrdersSorting> _sortingTypes;
		public ICollection<OrdersSorting> SortingTypes => _sortingTypes;
		private OrdersSorting _selectedSortType;
		public OrdersSorting SelectedSortType
		{
			get => _selectedSortType;
			set
			{
				if (_selectedSortType != value)
				{
					_selectedSortType = value;
					OnPropertyChanged(nameof(SelectedSortType));
					Orders.Clear();
					switch (_selectedSortType)
					{
						case OrdersSorting.None:
							foreach (OrderEntity order in ordrsFromDb)
								Orders.Add(order);
							break;
						case OrdersSorting.ActualOnly:
							foreach (OrderEntity order in ordrsFromDb.Where(order => order.IsActual))
							{
								Orders.Add(order);
							}
							break;
						default: break;
					}
				}
			}
		}
		private ObservableCollection<OrderEntity> _orders;
		public ICollection<OrderEntity> Orders => _orders;
		private OrderEntity _selectedOrder;
		public OrderEntity SelectedOrder
		{
			get => _selectedOrder;
			set
			{
				_selectedOrder = value;
				OnPropertyChanged(nameof(SelectedOrder));
			}
		}
		private readonly IOrderService _orderService;
		private static ICollection<OrderEntity> ordrsFromDb;
		public ICommand DeleteSelectedOrderCommand { get; }
		public ICommand MakeNotActiveCommand { get; }
		public ICommand RefreshOrdersCommand { get; }
	}
}
