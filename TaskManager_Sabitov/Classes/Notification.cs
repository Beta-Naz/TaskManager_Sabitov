using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManager_Sabitov.Classes
{
    public class Notification
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
