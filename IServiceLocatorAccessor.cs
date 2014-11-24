namespace MFramework.Infrastructure.ServiceLocator
{
    public interface IServiceLocatorAccessor
    {
        IServiceLocator ServiceLocator { get; }
    }
}