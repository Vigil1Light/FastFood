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
    public class MainViewModel : BaseViewModel
    {
        ObservableCollection<Menus> obMenus;

        readonly string[] menuImageList = { "tacos.jpg", "hamburger.jpg", "chinese_food.jpg", "thailand_food.jpg", "subway.jpg", "mcdonald's.jpg", "mexican_food.jpg", "italian_food.jpg", "french_food.jpg", "japanese_food.jpg"};


        private Users users { get; set; }
        private string headertext;

        List<int> menuList;

        public Command BackCommand { get; }

        public Users Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged();
            }
        }

        public string HeaderText
        {
            get => headertext;
            set => SetProperty(ref headertext, value);
        }

        public MainViewModel(Users user, ListView lstMenus)
        {
            BackCommand = new Command(onBackClicked);
            Users = new Users();
            Users.username = user.username;
            Users.password = user.password;
            HeaderText = "Welcome " + Users.username;
            getMenu(Users.username);
            AddMenus();
            lstMenus.ItemsSource = obMenus;
            AppSessionManager.Instance.StartSession();
        }

        private void onBackClicked(object obj)
        {
            App.Current.MainPage = new LoginPage();
        }

        private void AddMenus()
        {
            try
            {
                obMenus = new ObservableCollection<Menus>();
                for (int i = 0; i < menuList.Count; i++)
                {
                    string connstring = @"data source=InventorySystem.mssql.somee.com;initial catalog=InventorySystem;user id=linglu626;password=linglu626;Connect Timeout=600";
                    string strQuery = string.Format("SELECT * FROM MenuList WHERE mID = '{0}'", menuList[i]);
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
                        obMenus.Add(new Menus
                        {
                            menuImage = menuImageList[i],
                            menuType = temp,
                            menuLists = "Ramen, Sukiyaki, Curry rice, Basashi, Gyūdon, Sushi, Yakitori, Sashimi..."
                        });
                    }
                    reader.Close();
                    if(i == menuList.Count - 1)
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

        private void getMenu(string user)
        {
            string connstring = @"data source=InventorySystem.mssql.somee.com;initial catalog=InventorySystem;user id=linglu626;password=linglu626;Connect Timeout=600";
            string strQuery = string.Format("SELECT * FROM FoodUsers WHERE username = '{0}'", user);
            SqlConnection con = new SqlConnection(connstring);
            con.Open();
            SqlCommand command = new SqlCommand(strQuery, con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    string temp = reader.GetString(2);
                    menuList = temp.Split(',').Select(int.Parse).ToList();
                }
            }
            reader.Close();
            con.Close();
        }
    }
}
