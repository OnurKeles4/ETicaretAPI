using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System;
using ETicaretAPI.Application.ViewModels.Products;
using System.Net;
using ETicaretAPI.Domain.Entites;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _env;
        
        public ProductsController(IProductWriteRepository productWriteService, IProductReadRepository productReadRepository, IWebHostEnvironment env)
        {
            _productWriteRepository = productWriteService;
            _productReadRepository = productReadRepository;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //return Ok(_productReadRepository.GetAll(false).Select(p => new
            //{
            //    p.id,
            //    p.Name,
            //    p.Stock,
            //    p.Price,
            //    p.CreatedDate,
            //    p.UpdatedDate
            //}));
            return Ok(_productReadRepository.GetAll(true));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetbyIDAsync(id, false));                //the tracking is false because there is no additional calculation other than finding the                                                                                   element
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {

            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.saveAsnyc();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetbyIDAsync(model.id);
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;

            await _productWriteRepository.saveAsnyc();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.saveAsnyc();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {   
            
            string uploadPath = Path.Combine(_env.WebRootPath, "resource/product-images" );

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            Random r = new();
            
            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath, $"{r.Next()}{Path.GetExtension(file.FileName)}");

                using FileStream filestream= new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
            await file.CopyToAsync(filestream);
                await filestream.FlushAsync();
            }

            return Ok();
        }
    }
}
