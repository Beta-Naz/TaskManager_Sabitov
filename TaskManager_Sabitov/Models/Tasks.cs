using System.Text.RegularExpressions;
using System.Windows;
using TaskManager_Sabitov.Classes;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager_Sabitov.Models
{
    public class Tasks : Notification
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                Match match = Regex.Match(value, "^.{1,50}$");
                if (!match.Success)
                {
                    MessageBox.Show("Наименование не должно быть пустым, и не более 50 символов",
                        "Не корректный ввод значений", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _priority;
        public string Priority
        {
            get => _priority;
            set
            {
                Match match = Regex.Match(value, "^.{1,30}$");
                if (!match.Success)
                {
                    MessageBox.Show("Приоритет не должно быть пустым, и не более 40 символов",
                        "Не корректный ввод значений", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    _name = value;
                    OnPropertyChanged("Priority");
                }
            }
        }
        private DateTime _dateExecute;
        public DateTime DateExecute
        {
            get => _dateExecute;
            set
            {
                if(value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Дата выполнения не может быть меньше текущей",
                       "Не корректный ввод значений", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    _dateExecute = value;
                    OnPropertyChanged("DateExecute");
                }
            }
        }
        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                Match match = Regex.Match(value, "^.{1,1000}$");
                if (!match.Success)
                {
                    MessageBox.Show("Комментарий не должен быть пустым, и не более 1000 символов.",
                        "Не корректный ввод значений", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        private bool _isEnable;
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
                        ((ViewModels.VM_Pages)MainWindow.Instance.DataContext).VMTasks.TaskContext.SaveChanges();
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
                        ((ViewModels.VM_Pages)MainWindow.Instance.DataContext).VMTasks.Tasks.Remove(this);
                        ((ViewModels.VM_Pages)MainWindow.Instance.DataContext).VMTasks.TaskContext.Remove(this);
                        ((ViewModels.VM_Pages)MainWindow.Instance.DataContext).VMTasks.TaskContext.SaveChanges();
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
                });
            }
        }
    }
}
