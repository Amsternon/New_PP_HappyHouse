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
using HappyHouse.Tables;
using MySql.Data.MySqlClient;

namespace HappyHouse.CRUD;

public partial class CRUD_sklad : Window
{
    private List<warehouse> Skl;
    private warehouse CurrentSklads;
    public CRUD_sklad(warehouse currentSklad, List<warehouse> skl)
    {
        InitializeComponent();
        CurrentSklads = currentSklad;
        this.DataContext = currentSklad;
        Skl = skl;
    }
    private MySqlConnection conn;
    string connStr = "server=127.0.0.1;database=abd10;port=3306;User Id=root;password=12345";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var skll = Skl.FirstOrDefault(x => x.idWarehouse == CurrentSklads.idWarehouse);
        if (skll == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO warehouse VALUES (" + Convert.ToInt32(idWarehouse.Text) + ", '" + ProductName.Text + "', '" + Quantity.Text + "', '" + price.Text + "', '" + Convert.ToInt32(SupplierName.Text) + "');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE warehouse SET ProductName = '" + ProductName.Text + "', Quantity = '" +  Quantity.Text + "', price = '" + price.Text + "', SupplierName = '" + Convert.ToInt32(SupplierName.Text) + "' WHERE id = " + Convert.ToInt32(idWarehouse.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }
    private void GoBack(object? sender, RoutedEventArgs e)
    {
        Sklad back = new Sklad();
        this.Close();
        back.Show();
    }
}