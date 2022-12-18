﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TwentiBeauti_BackEnd_DotNet.Data;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/brand")]
    public class BrandController : Controller
    {
        private readonly Context dbContext;
        public BrandController(Context dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("index")]
        public async Task<IActionResult> index()
        {
            return Ok(JsonConvert.SerializeObject(await dbContext.Brand.ToListAsync()));
        }
    }
}
