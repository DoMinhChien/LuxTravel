using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Queries
{
    public class GetMasterDataQuery : IRequest<IEnumerable<SelectedObjectDto>>
    {
        public int Type { get; set; }
    }
}
