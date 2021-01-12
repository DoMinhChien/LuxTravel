﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using LuxTravel.Api.Core.Commands;

namespace LuxTravel.Api.Controllers
{
    [Route("api/photo")]
    [ApiController]
    public class PhotoController : ApiControllerBase
    {

        public PhotoController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            
        }
        [HttpPost]
        public async Task<bool> UploadPhoto([FromForm] UploadPhotoCommand fileCommand)
        {
            var result = await SendRequestAsync(fileCommand);
            return result;
        }
    }
}