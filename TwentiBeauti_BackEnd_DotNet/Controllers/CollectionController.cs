using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;
using System.Dynamic;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/collection")]

    public class CollectionController : ControllerBase
    {
        private readonly Context dbContext;
        public CollectionController(Context dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet("test")]
        public IActionResult GetNewtonsoftJson() =>
            BadRequest(            new JsonResult(new
            {
                x = "hello",
                y = "world"
            }));



        //public CollectionController DbContext { get; set; }

        [HttpGet("get")]
        public async Task<IActionResult> GetCollections()
        {
            return Ok(await dbContext.Collection.ToListAsync());

        }

        [HttpGet]
        [Route("show/{IDCollection:int}")]
        public async Task<IActionResult> GetCollection([FromRoute] int IDCollection)
        {
            var collection = await dbContext.Collection.FindAsync(IDCollection);
            if (collection == null)
            {
                return NotFound();
            }
            var json = JsonConvert.SerializeObject(collection);
            dynamic a = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            var productsInCol = dbContext.CollectionProduct.Where(c => c.IDCollection == IDCollection);
            List<Object> products = new List<Object>();
            foreach (var product in productsInCol)
            {


            }
            a.Products = products;


            return Ok(a);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddCollection(Collection addCollectionRequest)
        {
            var collection = new Collection()
            {
                IDCollection = addCollectionRequest.IDCollection,
                NameCollection = addCollectionRequest.NameCollection,
                RoutePath = addCollectionRequest.RoutePath,
                CreatedOn = addCollectionRequest.CreatedOn,
                Description = addCollectionRequest.Description,
                LogoImagePath = addCollectionRequest.LogoImagePath,
                WallPaperPath = addCollectionRequest.WallPaperPath,
                StartOn = addCollectionRequest.StartOn,
                EndOn = addCollectionRequest.EndOn,
                CoverImagePath = addCollectionRequest.CoverImagePath,


            };
            await dbContext.Collection.AddAsync(collection);
            await dbContext.SaveChangesAsync();
            return Ok(collection);
        }

        [HttpPut]
        [Route("update/{IDCollection:int}")]
        public async Task<IActionResult> UpdateCollection([FromRoute] int IDCollection, Collection updateCollectionRequest)
        {
            var collection = await dbContext.Collection.FindAsync(IDCollection);

            if (collection != null)
            {



                collection.NameCollection = updateCollectionRequest.NameCollection;

                collection.Description = updateCollectionRequest.Description;
                collection.LogoImagePath = updateCollectionRequest.LogoImagePath;
                collection.WallPaperPath = updateCollectionRequest.WallPaperPath;

                collection.CoverImagePath = updateCollectionRequest.CoverImagePath;



                await dbContext.SaveChangesAsync();
                return Ok(collection);

            }
            return NotFound();

        }

        [HttpDelete]
        [Route("delete/{IDCollection:int}")]
        public async Task<IActionResult> DeleteCollection([FromRoute] int IDCollection)
        {
            var collection = await dbContext.Collection.FindAsync(IDCollection);

            if (collection != null)
            {
                dbContext.Remove(collection);
                await dbContext.SaveChangesAsync();
                return Ok(collection);

            }
            return NotFound();
        }

    }
}
