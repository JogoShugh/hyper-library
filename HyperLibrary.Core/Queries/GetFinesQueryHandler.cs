using System.Collections.Generic;
using System.Net.Http;
using HyperLibrary.Core.Controllers;
using HyperLibrary.Core.LibraryModel;
using HyperLibrary.Core.Resources;

namespace HyperLibrary.Core.Queries
{
    public class GetFinesQueryHandler
    {
        private readonly IInMemoryFineRepository _fineRepository;
        private readonly IResourceLinker _resourceLinker;

        public GetFinesQueryHandler(IInMemoryFineRepository fineRepository,IResourceLinker resourceLinker)
        {
            _fineRepository = fineRepository;
            _resourceLinker = resourceLinker;
        }

        public FinesResource Query()
        {
            if (_fineRepository.HasFines())
            {
                var fines = new FinesResource();
                fines.Links = new List<Link>();
                fines.Links.Add(_resourceLinker.GetResourceLink<FinesController>(request => request.Post(), "Fee", "Pay your Fines!!", HttpMethod.Post));
                return fines;
            }
            return null;
        }
    }
}