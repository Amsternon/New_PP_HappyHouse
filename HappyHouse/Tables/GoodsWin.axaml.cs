using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows;
using HappyHouse.CRUD;

namespace HappyHouse.Tables;

public partial class GoodsWin : Window
{
    public GoodsWin()
    {
        InitializeComponent();
        string fullTable = "SELECT tableware.ID, suppliers.SuppliersName, tableware.Name_tab, tableware.price, colorss.Named, materials.Namer, category_availability.Name_ava FROM tableware JOIN materials ON tableware.mater = materials.ID JOIN colorss ON tableware.color = colorss.ID JOIN category_availability ON tableware.Availability = category_availability.idCategory_Availability JOIN suppliers ON tableware.Supplier_ID = suppliers.SuppliersID";
        ShowTable(fullTable);
        FillStatus();
    }

    private List<tableware> goods;
    private List<category_availability> Availability;
    string connStr = "server=127.0.0.1;database=abd10;port=3306;User Id=root;password=12345";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        goods = new List<tableware>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Client = new tableware()
            {
                ID = reader.GetInt32("ID"),
                SuppliersName = reader.GetString("SuppliersName"),
                Name_tab = reader.GetString("Name_tab"),
                Namer = reader.GetString("Namer"),
                Named = reader.GetString("Named"),
                price = reader.GetInt32("price"),
                Name_ava = reader.GetString("Name_ava")
            };
            goods.Add(Client);
        }
        conn.Close();
        DataGrid.ItemsSource = goods;
    }

    private void SearchGoods(object? sender, TextChangedEventArgs e)
    {
        var gds = goods;
        gds = gds.Where(x => x.Name_tab.Contains(Search_Goods.Text)).ToList();
        DataGrid.ItemsSource = gds;
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Top back = new Top();
        Close();
        back.Show();
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        string fullTable = "SELECT tableware.ID, suppliers.SuppliersName, tableware.Name_tab, tableware.price, colorss.Named, materials.Namer, category_availability.Name_ava FROM tableware JOIN materials ON tableware.mater = materials.ID JOIN colorss ON tableware.color = colorss.ID JOIN category_availability ON tableware.Availability = category_availability.idCategory_Availability JOIN suppliers ON tableware.Supplier_ID = suppliers.SuppliersID";
        ShowTable(fullTable);
        Search_Goods.Text = string.Empty;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            tableware usr = DataGrid.SelectedItem as tableware;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM tableware WHERE ID = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            goods.Remove(usr);
            ShowTable("SELECT tableware.ID, suppliers.SuppliersName, tableware.Name_tab, tableware.price, colorss.Named, materials.Namer, category_availability.Name_ava FROM tableware JOIN materials ON tableware.mater = materials.ID JOIN colorss ON tableware.color = colorss.ID JOIN category_availability ON tableware.Availability = category_availability.idCategory_Availability JOIN suppliers ON tableware.Supplier_ID = suppliers.SuppliersID");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        tableware newUser = new tableware();
        CRUD.CRUD_Goods add = new CRUD.CRUD_Goods(newUser, goods);
        add.Show();
        this.Close();
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        tableware currenGoods = DataGrid.SelectedItem as tableware;
        if (currenGoods == null)
            return;
        CRUD.CRUD_Goods edit = new  CRUD.CRUD_Goods(currenGoods, goods);
        edit.Show();
        this.Close();
    }
    private void CmbAvailability(object? sender, SelectionChangedEventArgs e)
    {
        var genderComboBox = (ComboBox)sender;
        var currentGender = genderComboBox.SelectedItem as category_availability;
        var filteredUsers = goods
            .Where(x => x.Name_ava == currentGender.Name_ava)
            .ToList();
        DataGrid.ItemsSource = filteredUsers;
    }

    public void FillStatus()
    {
        Availability = new List<category_availability>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from category_availability", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentGender = new category_availability()
            {
                idCategory_Availability = reader.GetInt32("idCategory_Availability"),
                Name_ava = reader.GetString("Name_ava"),
            };
            Availability.Add(currentGender);
        }
        conn.Close();
        var genderComboBox = this.Find<ComboBox>("CmbGender");
        genderComboBox.ItemsSource = Availability;
    }
}