using System;
using System.Collections.Generic;
using UniRx;

namespace DeveloperKit.Runtime.GameFramework
{
    public abstract class GameFramework<T> :IFramework where T :  GameFramework<T>,new()
    {
        private static T _instance;
        private static bool _initialize = false;

        private System.Collections.Generic.Dictionary<Type, IDataModel>
            _dateModels = new Dictionary<Type, IDataModel>();

        public MessageBroker eventHandler = new MessageBroker();

        
        public static T Interface
        {
            get
            {
                if (!_initialize)
                {
                    CreateFramework();
                    _initialize = true;
                }
                return _instance;
            }
        }

        private static void CreateFramework()
        {
            _instance = new T();
            _instance.Init();
        }

        public abstract void Init();
        public abstract IFramework GetFramework();

        public void SendCommand<T1>(T1 command) where  T1 : ICommand
        {
            command.SetFramework(this);
            command.Execute();
        }

        public void SendEvent<T1>(T1 evt)
        {
            throw new System.NotImplementedException();
        }

        public IObservable<T1> ReceiveEvent<T1>()
        {
            return eventHandler.Receive<T1>();
        }

        public void BindDataModel<T1>(T1 model) where T1 : IDataModel
        {
            var type = typeof(T1);
            if (_dateModels.ContainsKey(type))
            {
                _dateModels[type] = model;
            }
            else
            {
                _dateModels.Add(type, model);
            }
        }

        public T1 GetDataModel<T1>() where T1 : IDataModel
        {
            var type = typeof(T1);
            _dateModels.TryGetValue(type, out IDataModel model);
            return (T1)model;
        }
    }
}