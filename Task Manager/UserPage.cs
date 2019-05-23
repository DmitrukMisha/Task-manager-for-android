using System;
using System.Collections.Generic;
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
    [Activity(Label = "UserPage")]
    public class UserPage : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Task);
            string UserName = Intent.GetStringExtra("UserName");
            TextView login = FindViewById<TextView>(Resource.Id.textView1);
            TextView Name = FindViewById<TextView>(Resource.Id.textView2);
            TextView Role = FindViewById<TextView>(Resource.Id.textView3);
            TextView Priority = FindViewById<TextView>(Resource.Id.textView4);

            User user = GetUser(UserName);
            login.Text = "Login: " + user.Login;
            Name.Text = "Name: " + user.Name;
            Role.Text = "Role: " + user.Role;
            Priority.Text = "Priority: " + GetPriority(user.Priority);

            Button delete = FindViewById<Button>(Resource.Id.button1);
            delete.Text = "Delete user";
            delete.Click += (sender, e) =>
            {
                string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite");
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    this.Finish();
                }
            };

        }

        User GetUser(string UserName)
        {
            List<User> Users;
            User user = new User();
            string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite");
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                Users = db.Users.ToList();
            }
            foreach (User userr in Users)
            {
                if (String.Equals(userr.Login, UserName))
                {
                    return userr;
                }
            }
            return user;
        }

        public string GetPriority(int priority)
        {
            string s="";
            switch (priority)
            {
                case 1: s = "High";
                    break;
                case 2: s = "Medium";
                    break;
                case 3: s = "Low";
                    break;
            }
            return s;
        }
    }
}