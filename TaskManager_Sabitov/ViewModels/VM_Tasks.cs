using System.Collections.ObjectModel;
using TaskManager_Sabitov.Classes;
using TaskManager_Sabitov.Context;
using TaskManager_Sabitov.Models;

namespace TaskManager_Sabitov.ViewModels
{
    public class VM_Tasks : Notification
    {
        public TasksContext TaskContext = new TasksContext();
        public ObservableCollection<Tasks> Tasks {  get; set; }
        public VM_Tasks()
        {
            Tasks = new ObservableCollection<Tasks>(TaskContext.Tasks.OrderBy(x => x.Done));
        }
        public RealyCommand OnAddTask
        {
            get
            {
                return new RealyCommand(obg =>
                {
                    Models.Tasks newTask = new Tasks()
                    {
                        DateExecute = DateTime.Now
                    };
                    Tasks.Add(newTask);
                    TaskContext.Tasks.Add(newTask);
                    TaskContext.SaveChanges();
                });
            }
        }
    }
}
