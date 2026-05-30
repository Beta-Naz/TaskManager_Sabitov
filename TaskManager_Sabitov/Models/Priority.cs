using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using TaskManager_Sabitov.Classes;

namespace TaskManager_Sabitov.Models
{
    public class Priority : Notification
    {
        public int Id { get; set; }

        private string _name;
        [Required]
        [MaxLength(30)]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private int _level;
        [Required]
        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged("Level");
            }
        }
        public virtual ObservableCollection<Tasks> Tasks { get; set; }

        public Priority()
        {
            Tasks = new ObservableCollection<Tasks>();
        }
    }
}