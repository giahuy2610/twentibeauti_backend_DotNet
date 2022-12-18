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

        [HttpGet("get")]
        public async Task<IActionResult> GetCollections()
        {
            return Ok(await dbContext.Collection.ToListAsync());

        }

        [HttpGet]//done
        [Route("show/{IDCollection:int?}")]
        public async Task<IActionResult> GetCollection(int IDCollection=0)
        {
            if (IDCollection == 0) return Ok(JsonConvert.SerializeObject(dbContext.Collection.ToList()));
            var collection = dbContext.Collection.Find(IDCollection);
            if (collection == null)
            {
                return NotFound();
            }
            var json = JsonConvert.SerializeObject(collection);
            dynamic data = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            var productsInCol = dbContext.CollectionProduct.Where(c => c.IDCollection == IDCollection).ToList();
            List<dynamic> products = new List<dynamic>();
            foreach (var product in productsInCol)
            {
                var productDetail = await new ProductController(dbContext).show(product.IDProduct);
                products.Add(productDetail);
            }
            data.Products = products;
            return Ok(JsonConvert.SerializeObject(data));
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddCollection(dynamic addCollectionRequest)
        {
            try
            {
                var collection = new Collection()
                {
                    NameCollection = addCollectionRequest.NameCollection,
                    Description = addCollectionRequest.Description,
                    LogoImagePath = addCollectionRequest.LogoImagePath,
                    WallPaperPath = addCollectionRequest.WallPaperPath,
                    StartOn = addCollectionRequest.StartOn,
                    EndOn = addCollectionRequest.EndOn,
                    CoverImagePath = addCollectionRequest.CoverImagePath,
                    CreatedOn = DateTime.Now
                };
                await dbContext.Collection.AddAsync(collection);
                await dbContext.SaveChangesAsync();

                
                if (addCollectionRequest["Products"].Count > 0) 


                foreach (var product in addCollectionRequest["Products"])
                {
                    var productCol = new CollectionProduct()
                    {
                        IDCollection = collection.IDCollection,
                        IDProduct = product
                    };
                    await dbContext.CollectionProduct.AddAsync(productCol);
                    await dbContext.SaveChangesAsync();
                }
                var json = JsonConvert.SerializeObject(collection);
                dynamic data = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
                data.Products = dbContext.CollectionProduct.Where(c => c.IDCollection == collection.IDCollection).ToList();
                return Ok(addCollectionRequest["Products"]);
            }
            catch (FileNotFoundException e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        [Route("update/{IDCollection:int}")]
        public async Task<IActionResult> UpdateCollection([FromRoute] int IDCollection, dynamic updateCollectionRequest)
        {
            var collection = await dbContext.Collection.FindAsync(IDCollection);

            if (collection != null)
            {
                collection.NameCollection = updateCollectionRequest.NameCollection;
                collection.Description = updateCollectionRequest.Description;
                collection.LogoImagePath = updateCollectionRequest.LogoImagePath;
                collection.WallPaperPath = updateCollectionRequest.WallPaperPath;
                collection.CoverImagePath = updateCollectionRequest.CoverImagePath;
                collection.StartOn = updateCollectionRequest.StartOn;
                collection.EndOn = updateCollectionRequest.EndOn;
                await dbContext.SaveChangesAsync();

                dbContext.CollectionProduct.RemoveRange(dbContext.CollectionProduct.Where(p => p.IDCollection == IDCollection).ToList());
                if (updateCollectionRequest["Products"].Count > 0)
                    foreach (var product in updateCollectionRequest["Products"])
                        {
                            var productCol = new CollectionProduct()
                            {
                                IDCollection = collection.IDCollection,
                                IDProduct = product
                            };
                            await dbContext.CollectionProduct.AddAsync(productCol);
                            await dbContext.SaveChangesAsync();
                        }

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
