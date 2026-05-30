using System.Collections.ObjectModel;
using System.Linq;
using TaskManager_Sabitov.Classes;
using TaskManager_Sabitov.Context;
using TaskManager_Sabitov.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManager_Sabitov.ViewModels
{
    public class VM_Tasks : Notification
    {
        public TasksContext TaskContext = new TasksContext();
        public ObservableCollection<Tasks> Tasks { get; set; }
        public ObservableCollection<Priority> Priorities { get; set; }

        public VM_Tasks()
        {
            LoadPriorities();
            UpdateItems();
        }

        private void LoadPriorities()
        {
            Priorities = new ObservableCollection<Priority>(
                TaskContext.Priorities.OrderBy(p => p.Level));
            OnPropertyChanged("Priorities");
        }

        private string _searchString = "";
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged("SearchString");
                UpdateItems();
            }
        }

        private bool _isPrioritySorting = false;
        public bool IsPrioritySorting
        {
            get => _isPrioritySorting;
            set
            {
                _isPrioritySorting = value;
                TextBtnPriority = _isPrioritySorting ? "По умолчанию" : "По приоритету";
                UpdateItems();
            }
        }

        private string _textBtnPriority = "По приоритету";
        public string TextBtnPriority
        {
            get => _textBtnPriority;
            set
            {
                _textBtnPriority = value;
                OnPropertyChanged("TextBtnPriority");
            }
        }

        public void UpdateItems()
        {
            var query = TaskContext.Tasks
                .Include(t => t.Priority)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchString))
            {
                query = query.Where(t => t.Name.Contains(SearchString));
            }

            if (IsPrioritySorting)
            {
                query = query.OrderBy(t => t.Done)
                             .ThenByDescending(t => t.Priority.Level)
                             .ThenBy(t => t.DateExecute);
            }
            else
            {
                query = query.OrderBy(t => t.Done)
                             .ThenBy(t => t.DateExecute);
            }

            var tasksList = query.ToList();
            Tasks = new ObservableCollection<Tasks>(tasksList);
            OnPropertyChanged("Tasks");
        }

        public RealyCommand OnSetPriority
        {
            get
            {
                return new RealyCommand(obg =>
                {
                    IsPrioritySorting = !IsPrioritySorting;
                });
            }
        }

        public RealyCommand OnAddTask
        {
            get
            {
                return new RealyCommand(obg =>
                {
                    var defaultPriority = TaskContext.Priorities.FirstOrDefault(p => p.Name == "Средний");

                    Models.Tasks newTask = new Tasks()
                    {
                        DateExecute = DateTime.Now,
                        Name = "Новая задача",
                        PriorityId = defaultPriority?.Id ?? 2,
                        Comment = "Мой комментарий",
                        Done = false,
                        IsEnable = true
                    };

                    Tasks.Add(newTask);
                    TaskContext.Tasks.Add(newTask);
                    TaskContext.SaveChanges();
                    UpdateItems();
                });
            }
        }
    }
}