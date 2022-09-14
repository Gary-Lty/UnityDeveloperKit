namespace DeveloperKit.Runtime.GameFramework
{
    public abstract class AbstractCommand : ICommand
    {
        public abstract void SetFramework(IFramework framework);
        public abstract IFramework GetFramework();
        public abstract void Execute();
    }
}