using Lib.Logging.Interfaces;

namespace Lib.Logging.Factories;

public interface ILogFactory
{
    ILog Create();
}