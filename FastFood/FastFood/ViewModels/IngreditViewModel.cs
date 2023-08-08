using FastFood.Models;
using FastFood.Utils;
using FastFood.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using Xamarin.Forms;

namespace FastFood.ViewModels
{
    public class IngreditViewModel : BaseViewModel
    {
        ObservableCollection<Ingredits> obIngredits;

        private string menuName;

        List<int> ingreditList;

        public string MenuName
        {
            get => menuName;
            set => SetProperty(ref menuName, value);
        }

        public IngreditViewModel(string menu, ListView lstIngredits)
        {
            lstIngredits.ItemsSource = obIngredits;
            MenuName = menu;
            getIngredit(MenuName);
            AddIngredits();
            lstIngredits.ItemsSource = obIngredits;
            AppSessionManager.Instance.StartSession();
        }

        private void AddIngredits()
        {
            try
            {
                obIngredits = new ObservableCollection<Ingredits>();
                for (int i = 0; i < ingreditList.Count; i++)
                {
                    string connstring = @"data source=InventorySystem.mssql.somee.com;initial catalog=InventorySystem;user id=linglu626;password=linglu626;Connect Timeout=600";
                    string strQuery = string.Format("SELECT * FROM IngreditList WHERE iID = '{0}'", ingreditList[i]);
                    SqlConnection con = new SqlConnection(connstring);
                    con.Open();
                    SqlCommand command = new SqlCommand(strQuery, con);
                    SqlDataReader reader = command.ExecuteReader();
                    string temp = "";
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            temp = reader.GetString(1);
                        }
                        obIngredits.Add(new Ingredits
                        {
                            ingreditName = temp,
                        });
                    }
                    reader.Close();
                    if (i == ingreditList.Count - 1)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {
                DependencyService.Get<Toast>().Show("Can't connect database");
            }
        }

        private void getIngredit(string menu)
        {
            string connstring = @"data source=InventorySystem.mssql.somee.com;initial catalog=InventorySystem;user id=linglu626;password=linglu626;Connect Timeout=600";
            string strQuery = string.Format("SELECT * FROM MenuList WHERE mName = '{0}'", menu);
            SqlConnection con = new SqlConnection(connstring);
            con.Open();
            SqlCommand command = new SqlCommand(strQuery, con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    string temp = reader.GetString(2);
                    ingreditList = temp.Split(',').Select(int.Parse).ToList();
                }
            }
            reader.Close();
            con.Close();
        }

    }
}
