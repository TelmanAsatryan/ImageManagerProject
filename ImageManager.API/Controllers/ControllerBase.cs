using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageManager.API.Controllers.Core;
using ImageManager.DAL.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageManager.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ControllerBase : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        //protected readonly IConfigurationService configuration;


        protected ControllerBase(
            // IConfigurationService configuration,
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            // this.configuration = configuration;
        }


        protected ApiContext ApiContext { get; private set; }
        protected void Save()
        {
            unitOfWork.Save();
        }
        protected async Task SaveAsync()
        {
            await unitOfWork.SaveAsync();
        }
    }
}
