using System;

namespace DeveloperKit.Runtime.GameFramework
{
    public interface IFramework
    {
        void Init();
        void SendCommand<T>(T command) where T : ICommand;
        void SendEvent<T>(T evt);
        public IObservable<T> ReceiveEvent<T>();

        void BindDataModel<T>(T model) where T : IDataModel;
        T GetDataModel<T>() where T : IDataModel;
    }

    public interface ICanSetFramework
    {
        public void SetFramework(IFramework framework);
    }
    
    public interface ICanGetFramework
    {
        public IFramework GetFramework();
    }
    

    public interface ICommand: ICanGetFramework,ICanSetFramework
    {
        public void Execute();
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
        
        public static void SendEvent<T>(this IController command,T evt)
        {
            command.GetFramework().SendEvent(evt);
        }

        public static IObservable<T> ReceiveEvent<T>(this IController controller)
        {
            return controller.GetFramework().ReceiveEvent<T>();
        }
        
        public static T GetDataModel<T>(this ICommand command) where  T : IDataModel
        {
            return command.GetFramework().GetDataModel<T>();
        }
        
        public static void SendEvent<T>(this ICommand command,T evt) where  T : IDataModel
        {
            command.GetFramework().SendEvent(evt);
        }
        
        public static void SendCommand<T>(this IController controller,T command) where  T : ICommand
        {
            controller.GetFramework().SendCommand(command);
        }
    }
}