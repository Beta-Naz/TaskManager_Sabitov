using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace TaskManager_Sabitov.Classes
{
    public class Notification : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if(PropertyChanged != null) 
            { 
                PropertyChanged(this, new PropertyChangedEventArgs(prop)); 
            }
        }
    }
}
