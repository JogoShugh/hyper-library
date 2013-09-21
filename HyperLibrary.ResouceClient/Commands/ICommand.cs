using System.Collections.Generic;
using System.Threading.Tasks;

namespace HyperLibrary.ResouceClient.Commands
{
    internal interface ICommand
    {
        string Description { get; }
        Task<IEnumerable<ICommand>> Execute();
    }
}