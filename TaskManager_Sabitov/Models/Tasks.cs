using System.Text.RegularExpressions;
using System.Windows;
using TaskManager_Sabitov.Classes;
using Schema = System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManager_Sabitov.Models
{
    public class Tasks : Notification
    {
        public int Id { get; set; }

        private string _name;
        [Required]
        [MaxLength(50)]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                {
                    MessageBox.Show("Наименование не должно быть пустым, и не более 50 символов",
                        "Некорректный ввод значений", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private int _priorityId;
        [Required]
        public int PriorityId
        {
            get => _priorityId;
            set
            {
                _priorityId = value;
                OnPropertyChanged("PriorityId");
                OnPropertyChanged("PriorityName");
            }
        }

        private Priority _priority;
        public virtual Priority Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                if (value != null)
                    PriorityId = value.Id;
                OnPropertyChanged("Priority");
                OnPropertyChanged("PriorityName");
            }
        }

        [Schema.NotMapped]
        public string PriorityName => Priority?.Name ?? "Не указан";

        private DateTime _dateExecute;
        [Required]
        public DateTime DateExecute
        {
            get => _dateExecute;
            set
            {
                if (value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Дата выполнения не может быть меньше текущей",
                       "Некорректный ввод значений", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    _dateExecute = value;
                    OnPropertyChanged("DateExecute");
                }
            }
        }

        private string _comment;
        [MaxLength(1000)]
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != null && value.Length > 1000)
                {
                    MessageBox.Show("Комментарий не более 1000 символов.",
                        "Некорректный ввод значений", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    _comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private bool _done;
        public bool Done
        {
            get => _done;
            set
            {
                _done = value;
                OnPropertyChanged("Done");
                OnPropertyChanged("IsDoneText");
            }
        }

        [Schema.NotMapped]
        private bool _isEnable = false;

        [Schema.NotMapped]
        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                _isEnable = value;
                OnPropertyChanged("IsEnable");
                OnPropertyChanged("IsEnableText");
            }
        }

        [Schema.NotMapped]
        public string IsEnableText
        {
            get
            {
                if (IsEnable) return "Сохранить";
                else return "Изменить";
            }
        }

        [Schema.NotMapped]
        public string IsDoneText
        {
            get
            {
                if (Done) return "Не выполнено";
                else return "Выполнено";
            }
        }

        [Schema.NotMapped]
        public RealyCommand OnEdit
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    IsEnable = !IsEnable;
                    if (!IsEnable)
                    {
                        var vm = ((ViewModels.VM_Pages)MainWindow.Instance.DataContext).VMTasks;
                        vm.TaskContext.SaveChanges();
                        vm.UpdateItems();
                    }
                });
            }
        }

        [Schema.NotMapped]
        public RealyCommand OnDelete
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    if (MessageBox.Show("Вы уверены что хотите удалить задачу", "Предупреждение",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var vm = ((ViewModels.VM_Pages)MainWindow.Instance.DataContext).VMTasks;
                        vm.TaskContext.Tasks.Remove(this);
                        vm.TaskContext.SaveChanges();
                        vm.UpdateItems();
                    }
                });
            }
        }

        [Schema.NotMapped]
        public RealyCommand OnDone
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    Done = !Done;
                    var vm = ((ViewModels.VM_Pages)MainWindow.Instance.DataContext).VMTasks;
                    vm.TaskContext.SaveChanges();
                    vm.UpdateItems();
                });
            }
        }
    }
}