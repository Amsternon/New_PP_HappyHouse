using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HappyHouse.CRUD;
using MySql.Data.MySqlClient;

namespace HappyHouse.Tables;

public partial class Sklad : Window
{
    public Sklad()
    {
        InitializeComponent();
        ShowTable(fullTable);
    }
    string fullTable = "SELECT warehouse.idWarehouse, warehouse.ProductName, warehouse.Quantity, warehouse.Price_per_unit, suppliers.SuppliersName FROM warehouse JOIN suppliers ON warehouse.idSupplier = suppliers.SuppliersID";
   
    private List<warehouse> skl;
    string connStr = "server=127.0.0.1;database=abd10;port=3306;User Id=root;password=12345";
    private MySqlConnection conn;
    public void ShowTable(string sql)
    {
        skl = new List<warehouse>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Warehouse = new warehouse()
            {
                idWarehouse = reader.GetInt32("idWarehouse"),
                ProductName  = reader.GetString("ProductName"),
                Quantity = reader.GetString("Quantity"),
                Price_per_unit = reader.GetString("Price_per_unit"),
                SuppliersName = reader.GetString("SuppliersName"),
            };
            skl.Add(Warehouse);
        }
        conn.Close();
        DataGrid.ItemsSource = skl;
    }
    private void SearchLogin(object? sender, TextChangedEventArgs e)
    {
        var login = skl;
        login = login.Where(x => x.ProductName.Contains(Search_Login.Text)).ToList();
        DataGrid.ItemsSource = login;
    }
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Top back = new Top();
        Close();
        back.Show();
    }
    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable(fullTable);
        Search_Login.Text = string.Empty;
    }
    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            warehouse skll = DataGrid.SelectedItem as warehouse;
            if (skll == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM warehouse WHERE idWarehouse = " + skll.idWarehouse;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            skl.Remove(skll);
            ShowTable("SELECT warehouse.idWarehouse, warehouse.ProductName, warehouse.Quantity, warehouse.Price_per_unit, suppliers.SuppliersName FROM warehouse JOIN suppliers ON warehouse.idSupplier = suppliers.SuppliersID");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void AddData(object? sender, RoutedEventArgs e)
    {
        warehouse newSklad = new warehouse();
        CRUD_sklad add = new CRUD_sklad(newSklad, skl);
        add.Show();
        this.Close();
    }
    private void Edit(object? sender, RoutedEventArgs e)
    {
        warehouse currentSklad = DataGrid.SelectedItem as warehouse;
        if (currentSklad == null)
            return;
        CRUD_sklad edit = new  CRUD_sklad(currentSklad, skl);
        edit.Show();
        this.Close();
    }
}