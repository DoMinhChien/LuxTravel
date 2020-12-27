﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonFunctionality.Helper;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Queries
{
    public class GetAllCitiesQuery : IRequest<IEnumerable<SelectedObjectDto>>
    {
    }
}