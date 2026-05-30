using TaskManager_Sabitov.Classes;

namespace TaskManager_Sabitov.ViewModels
{
    public class VM_Pages : Notification 
    {
        public VM_Tasks VMTasks = new VM_Tasks();

        public VM_Pages()
        {
            MainWindow.Instance.OpenPage(new View.Main(VMTasks));
        }

        public RealyCommand OnClose
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    MainWindow.Instance.Close();
                });
            }
        }
    }
}
