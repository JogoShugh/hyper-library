using System.Collections.Generic;
using System.Net.Http;
using HyperLibrary.Core.Controllers;
using HyperLibrary.Core.LibraryModel;
using HyperLibrary.Core.Resources;

namespace HyperLibrary.Core.Commands
{
    public class PayFinesCommandHandler
    {
        private readonly IInMemoryFineRepository _fineRepository;
        private readonly IResourceLinker _resourceLinker;

        public PayFinesCommandHandler(IInMemoryFineRepository fineRepository, IResourceLinker resourceLinker)
        {
            _fineRepository = fineRepository;
            _resourceLinker = resourceLinker;
        }

        public ThanksResource Execute()
        {
            _fineRepository.PayFines();
            var thanks = new ThanksResource();
            thanks.Links = new List<Link>();
            thanks.Links.Add(_resourceLinker.GetResourceLink<RootController>(request => request.Get(), "Home", "Home", HttpMethod.Get));
            return thanks;
        }
    }
}