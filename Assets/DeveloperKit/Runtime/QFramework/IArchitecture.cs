using System;

namespace QFramework
{
    /// <summary>
    /// mvc架构接口
    /// </summary>
    public interface IArchitecture
    {
        /// <summary>
        /// 注册系统
        /// </summary>
        /// <param name="system"></param>
        /// <typeparam name="T"></typeparam>
        void RegisterSystem<T>(T system) where T : ISystem;

        /// <summary>
        /// 注册数据模型
        /// </summary>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        void RegisterModel<T>(T model) where T : IModel;

        /// <summary>
        /// 注册功能模块
        /// </summary>
        /// <param name="utility"></param>
        /// <typeparam name="T"></typeparam>
        void RegisterUtility<T>(T utility) where T : IUtility;

        /// <summary>
        /// 获取系统
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetSystem<T>() where T : class, ISystem;

        /// <summary>
        /// 获取数据模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetModel<T>() where T : class, IModel;

        /// <summary>
        /// 获取功能模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetUtility<T>() where T : class, IUtility;

        /// <summary>
        /// 向系统内发送命令，修改数据模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void SendCommand<T>() where T : ICommand, new();
        
        /// <summary>
        /// 向架构内发送命令，修改数据模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void SendCommand<T>(T command) where T : ICommand;

        /// <summary>
        /// 向架构内发送查询
        /// </summary>
        /// <param name="query"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        TResult SendQuery<TResult>(IQuery<TResult> query);

        /// <summary>
        /// 向架构内发送事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void SendEvent<T>() where T : new();
        
        /// <summary>
        /// 向架构内发送事件
        /// </summary>
        /// <param name="e"></param>
        /// <typeparam name="T"></typeparam>
        void SendEvent<T>(T e);

        /// <summary>
        /// 在架构内注册事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IObservable<T> RegisterEvent<T>();
    }
}