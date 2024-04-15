using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SuperBasketball.Models;

namespace SuperBasketball;

public partial class AddItemWindow : Window
{
    private DB _db = new DB();
    private Action updateLastViewPlus;
    private Action updateLastViewMinus;
    public AddItemWindow()
    {
        InitializeComponent();
        this.updateLastViewPlus = updateLastViewPlus;
        this.updateLastViewMinus = updateLastViewMinus;

        InitView();
    }
    
    private void InitView()
    {
        ComboBox positionComboBox = this.FindControl<ComboBox>("PositionComboBox");
        ComboBox teamComboBox = this.FindControl<ComboBox>("TeamComboBox");
        
        List<Position> positionsFromDB = _db.GetPositions();

        List<Position> positions = new List<Position>();
        positions.Add(new Position());
        positions.AddRange(positionsFromDB);
        positionComboBox.ItemsSource = positions;
        positionComboBox.SelectedIndex = 0;
        
        List<Team> teamsFromDB = _db.GetTeams();

        List<Team> teams = new List<Team>();
        teams.Add(new Team());
        teams.AddRange(teamsFromDB);
        teamComboBox.ItemsSource = teams;
        teamComboBox.SelectedIndex = 0;
    }

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        SaveIt();
        Close();
    }
    
    

    private void Cancel_OnClick(object? sender, RoutedEventArgs e)
    {
        Close();
    }
    private void SaveIt()
    {
        TextBox nameTextBox = this.FindControl<TextBox>("NameTextBox");
        NumericUpDown massNumericUpDown = this.FindControl<NumericUpDown>("MassNumericUpDown");
        NumericUpDown heightNumericUpDown = this.FindControl<NumericUpDown>("HeightNumericUpDown");
        DatePicker birthdayDatePicker = this.FindControl<DatePicker>("BirthdayDatePicker");
        DatePicker dateStartDatePicker = this.FindControl<DatePicker>("DateStartDatePicker");
        ComboBox positionComboBox = this.FindControl<ComboBox>("PositionComboBox");
        ComboBox teamComboBox = this.FindControl<ComboBox>("TeamComboBox");

        
        if (String.IsNullOrWhiteSpace(nameTextBox.Text) || massNumericUpDown.Value == null || heightNumericUpDown.Value == null || birthdayDatePicker.SelectedDate == null || dateStartDatePicker.SelectedDate == null)
            return;

        Player player = new Player()
        {
            Name = nameTextBox.Text,
            Mass = (double)massNumericUpDown.Value,
            Height = (double)heightNumericUpDown.Value,
            Birthday = birthdayDatePicker.SelectedDate.Value,
            DateStart = dateStartDatePicker.SelectedDate.Value,
            PositionId = (positionComboBox.SelectedItem as Position).Id,
            TeamId = (teamComboBox.SelectedItem as Team).Id
        };

        bool result = _db.AddPlayer(player);
        
        if (result)
            updateLastViewPlus.Invoke();
        else
            updateLastViewMinus.Invoke();
    }
}
