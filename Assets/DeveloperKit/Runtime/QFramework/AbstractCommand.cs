namespace QFramework
{
    /// <summary>
    /// 抽象命令类
    /// </summary>
    public abstract class AbstractCommand : ICommand
    {
        private IArchitecture mArchitecture;

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        void ICommand.Execute()
        {
            OnExecute();
        }

        /// <summary>
        /// 执行命令的回调
        /// </summary>
        protected abstract void OnExecute();
    }
}