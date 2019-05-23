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
   public class Home: Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.sqlite");

        int Priority;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Home, container, false);
      
            Priority=Arguments.GetInt("Priority");
            
            List<Task> Tasks = GetTasks();
            List<string> tasksNames = GetTasksNames(Tasks);
            ListView listView = view.FindViewById<ListView>(Resource.Id.listView1);
             ArrayAdapter<string> adapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SimpleListItem1, tasksNames);
             listView.Adapter = adapter;

            listView.ItemClick += (object sender, Android.Widget.AdapterView.ItemClickEventArgs e) =>
            {
                string selectedFromList = listView.GetItemAtPosition(e.Position).ToString();
                var intent = new Intent(Context, typeof(TaskPage));
                intent.PutExtra("TaskName", selectedFromList);
                StartActivity(intent);
            };
            return view;
        }

        public List<Task> GetTasks()
        {
            List<Task> Tasks;
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                Tasks = db.Tasks.ToList();
            }
            return Tasks;
        }
        public List<string> GetTasksNames(List<Task> Tasks)
        {
            List<string> tasksNames = new List<string>();
            foreach (Task task in Tasks)
            {
                if (task.Priority >= Priority)
                {
                    tasksNames.Add(task.Name);
                }
            }
            return tasksNames;
        }

    }
}