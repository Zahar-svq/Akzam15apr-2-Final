using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SuperBasketball.Models;

namespace SuperBasketball;

public partial class TableWindow : Window
{
    private DB _db = new DB();
    public TableWindow()
    {
        InitializeComponent();
        InitView();
        UpdateDateOnView();
    }
private void UpdateDateOnView()
    {
        TextBox searchTextBox = this.FindControl<TextBox>("SearchTextBox");
        ComboBox filterComboBox = this.FindControl<ComboBox>("FilterComboBox");

        string search;
        if (String.IsNullOrWhiteSpace(searchTextBox.Text))
        {
            search = "";
        }
        else
        {
            search = searchTextBox.Text;
        }

        int filter_id = (filterComboBox.SelectedItem as Position).Id;

        List<Player> players = GetDataFromDB(search, filter_id);

        DataGrid mainDataGrid = this.FindControl<DataGrid>("MainDataGrid");
        mainDataGrid.ItemsSource = players;
    }

    private void InitView()
    {
        ComboBox filterComboBox = this.FindControl<ComboBox>("FilterComboBox");
        
        List<Position> positionsFromDB = _db.GetPositions();

        List<Position> filterPositions = new List<Position>();
        filterPositions.Add(new Position(){Id = 0, Name = "Все позиции"});
        filterPositions.AddRange(positionsFromDB);

        filterComboBox.ItemsSource = filterPositions;
    }

    private void DeleteItem_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Config.CurrentEmployee.Role != "Администратор")
            return;

        DataGrid mainDataGrid = this.FindControl<DataGrid>("MainDataGrid");
        var selectedItem = mainDataGrid.SelectedItem;

        if (selectedItem == null)
            return;

        bool result = _db.DeletePlayer((selectedItem as Player).Id);
        
        if (result)
            SetInfo("Delete successful");
        else
            SetInfo("ERROR Delete");
            
        
        UpdateDateOnView();
    }

    private void SetInfo(string message)
    {
        TextBlock infoTextBox = this.FindControl<TextBlock>("InfoTextBox");
        infoTextBox.Text = message;
    }

    private void AddItem_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Config.CurrentEmployee.Role != "Администратор" || Config.CurrentEmployee.Role != "Менеджер")
            return;

        AddItemWindow window = new AddItemWindow(  );
        window.Show(this);
    }

    private void AddUpdatePlus()
    {
        UpdateDateOnView();
        SetInfo("Add item");
    }
    
    private void AddUpdateMinus()
    {
        UpdateDateOnView();
        SetInfo("ERROR Add item");
    }


    private List<Player> GetDataFromDB(string search, int filter_id)
    {
        return _db.GetPlayers(search, filter_id);
    }

    private void SearchTextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        UpdateDateOnView();
    }

    private void FilterComboBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        UpdateDateOnView();
    }
}