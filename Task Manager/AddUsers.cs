using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Task_Manager.Models;

namespace Task_Manager
{
    class AddUsers: Android.Support.V4.App.Fragment
    {

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite");
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AddUser, container, false);
            string Login = Arguments.GetString("Login");

            List<User> Users = GetUsers();
            List<string> UsersNames = GetUsersLogins(Users);
            UsersNames.Remove(Login);
            ListView listView = view.FindViewById<ListView>(Resource.Id.listView1);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleListItem1, UsersNames);
            listView.Adapter = adapter;

            listView.ItemClick += (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) =>
            {
                string selectedFromList = listView.GetItemAtPosition(e.Position).ToString();
                var intent = new Intent(Context, typeof(UserPage));
                intent.PutExtra("UserName", selectedFromList);
                StartActivity(intent);
            };

            Button LoginButton = view.FindViewById<Button>(Resource.Id.AddUserButton);

            LoginButton.Click += (sender, e) =>
            {
                Intent intent = new Intent(Context, typeof(AddUserPage));
                        StartActivity(intent);
            };
            return view;
        }

        public List<User> GetUsers()
        {
            List<User> Users;
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
               Users = db.Users.ToList();
            }
            return Users;
        }

        public List<string> GetUsersLogins(List<User> Users)
        {
            List<string> UsersLogins = new List<string>();
            foreach(User user in Users)
            {
                UsersLogins.Add(user.Login);
            }
            return UsersLogins;
        }
    }
}