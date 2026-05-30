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
    }
}
