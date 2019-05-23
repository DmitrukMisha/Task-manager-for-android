using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Task_Manager.Models;

namespace Task_Manager
{
    [Activity(Label = "TaskPage")]
    public class TaskPage : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Task);
           string TaskName = Intent.GetStringExtra("TaskName");
            TextView name = FindViewById<TextView>(Resource.Id.textView1);
            TextView Description = FindViewById<TextView>(Resource.Id.textView2);
            TextView Date = FindViewById<TextView>(Resource.Id.textView3);
            TextView Priority = FindViewById<TextView>(Resource.Id.textView4);

            Task task = GetTask(TaskName);
            name.Text = "Name: "+task.Name;
            Description.Text = "Description: "+task.Description;
            Date.Text = "Date: "+task.Date;
            Priority.Text = "Priority: "+GetPriority(task.Priority);

            Button LoginButton = FindViewById<Button>(Resource.Id.button1);
            LoginButton.Click += (sender, e) =>
            {
                string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite");
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    db.Tasks.Remove(task);
                    db.SaveChanges();
                    this.Finish();
                }
            };

        }

        Task GetTask(string TaskName)
        {
            List<Task> Tasks;
            Task task = new Task();
            string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite");
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
              Tasks = db.Tasks.ToList();
            }
            foreach(Task taask in Tasks)
            {
                if (String.Equals(taask.Name, TaskName)){
                    return taask;
                }
            }
            return task;
        }
        public string GetPriority(int priority)
        {
            string s = "";
            switch (priority)
            {
                case 1:
                    s = "High";
                    break;
                case 2:
                    s = "Medium";
                    break;
                case 3:
                    s = "Low";
                    break;
            }
            return s;
        }
    }
}