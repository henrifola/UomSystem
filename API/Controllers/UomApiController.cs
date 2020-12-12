using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Contracts.UnitOfMeasureContracts;
using Data;
using Data.Models;
using EngineeringUnitscore;
using Microsoft.AspNetCore.Mvc;

namespace UomSystem.Controllers
{
    [ApiController]
    [Route("Uom")]
    public class UomAPiController : ControllerBase
    {
        private readonly IRepositoryWrapper _wrapper;

        public UomAPiController(IRepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }
    }
}