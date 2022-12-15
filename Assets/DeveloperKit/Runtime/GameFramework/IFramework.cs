using System;

namespace DeveloperKit.Runtime.GameFramework
{
    public interface IFramework
    {
        void Init();
        void SendCommand<T>(T command) where T : ICommand;
        void BindDataModel<T>(T model) where T : IDataModel;
        T GetDataModel<T>() where T : IDataModel;
    }

    public interface ICanSetFramework
    {
        void SetFramework(IFramework framework);
    }
    
    public interface ICanGetFramework
    {
        IFramework GetFramework();
    }
    

    public interface ICommand: ICanGetFramework,ICanSetFramework
    {
        void Execute();
    }

    public interface IController : ICanGetFramework
    {
    }

    public interface IDataModel
    {
        
    }
    
    public static class InterfaceExtension
    {
        public static T GetDataModel<T>(this IController controller) where  T : IDataModel
        {
            return controller.GetFramework().GetDataModel<T>();
        }
        
       
        public static T GetDataModel<T>(this ICommand command) where  T : IDataModel
        {
            return command.GetFramework().GetDataModel<T>();
        }
        
        public static void SendCommand<T>(this IController controller,T command) where  T : ICommand
        {
            controller.GetFramework().SendCommand(command);
        }
    }
}