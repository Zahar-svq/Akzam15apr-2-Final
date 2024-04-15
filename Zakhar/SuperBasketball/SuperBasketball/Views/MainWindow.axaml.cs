using System;
using Avalonia.Controls;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SuperBasketball.Models;

namespace SuperBasketball.Views;

public partial class MainWindow : Window
{
    private DB _db = new DB();
    public MainWindow()
    {
        InitializeComponent();
        
    }
    private void SignIn()
    {
        TextBox loginTextBox = this.FindControl<TextBox>("LoginTextBox");
        TextBox passwordTextBox = this.FindControl<TextBox>("PasswordTextBox");

        if (String.IsNullOrWhiteSpace(loginTextBox.Text) || String.IsNullOrWhiteSpace(passwordTextBox.Text))
            return;
        
        Employee employee = _db.CheckEmployee(loginTextBox.Text, passwordTextBox.Text);

        if (employee.Id.ToString() != "")
        {
            Config.CurrentEmployee = employee;
            OpenTableWindow();
        }
    }


    private void SignIn_OnClick(object? sender, RoutedEventArgs e)
    {
        SignIn();
    }
    
    private void OpenTableWindow()
    {
        TableWindow window = new TableWindow();
        window.Show();
        this.Close();
    }
}