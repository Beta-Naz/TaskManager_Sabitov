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

        private RealyCommand _onClose;
        public RealyCommand OnClose
        {
            get => _onClose ??= new RealyCommand(obj => MainWindow.Instance.Close());
        }
    }
}
