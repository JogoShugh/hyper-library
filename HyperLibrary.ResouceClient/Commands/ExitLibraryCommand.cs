using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyperLibrary.ResouceClient.Commands
{
    internal class ExitLibraryCommand : ICommand
    {
        public string Description { get { return "Leave this place"; }}

        public Task<IEnumerable<ICommand>> Execute()
        {
            return Task.FromResult(Enumerable.Empty<ICommand>());
        }
    }
}