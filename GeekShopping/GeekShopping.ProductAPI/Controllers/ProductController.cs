﻿using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
           var products = await _repository.FindAll();
            return Ok(products);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var product = await _repository.FindById(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create([FromBody]ProductVO vo)
        {
            if(vo == null)
            {
                return BadRequest();
            }
            else
            {
                var product = await _repository.Create(vo);
                return Ok(product);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody]ProductVO vo)
        {
            if (vo == null)
            {
                return BadRequest();
            }
            else
            {
                var product = await _repository.Update(vo);
                return Ok(product);
            }
        }

        
        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _repository.Delete(id);
            if (!status)
            {
                return BadRequest();
            }
            else 
            {
                return Ok(status);
            }
        }
    }
}
