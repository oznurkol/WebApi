using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiProje.DataAccess;
using WebApiProje.Entities;

namespace WebApiProje.Controllers
{

    //attribte olarak route tanımlanır statup.cs deki appMVC içerisndeki yerine bu şekilde tanımlama daha yaygın
    //api/products url i gelen bütünistekler buraya demek
    [Route("api/products")]
    public class ProductsController : Controller
    {
        IProductDal _productDal;
        public ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }
        //burada sadece api/products yolu ile yani herhangi bi parametresi olmayan get isteklerinde çalışır
        [HttpGet("")]
        public IActionResult Get()
        {
            var products = _productDal.GetList();
            return Ok(products); //Ok http cevabıdır
        }
        //routing işlemi yaptık url için aşağıdaki fonk parametres ile aynı isimde olmalı
        [HttpGet("{productId}")]
        public IActionResult Get(int productId)
        {
            try
            {
                var product = _productDal.Get(p => p.ProductId == productId);
                if(product==null)
                {
                    //$ işareti string içerisnde parametre geçirmeye yarar
                    return NotFound($"There is no product with Id = {productId}");
                }
                return Ok(product); //Ok http cevabıdır
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        //[FromBody] kelmimesi postman den veya herhangi bir erden gelen json 
        //formatındaki verileri okumak çin parametrenin başına eklenir
        // public IActionResult Post ([FromBody]Product product)
        //Post yeni kayıt ekleme
        public IActionResult Post (Product product)
        {
            try
            {
                //201 created demek
                _productDal.Add(product);
                return new StatusCodeResult(201);
            }
            catch (Exception)
            {

                throw;
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put(Product product)
        {
            try
            {
                _productDal.Update(product);
                return Ok(product);
            }
            catch (Exception)
            {

                throw;
            }
            return BadRequest();
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            try
            {   
                _productDal.Delete(new Product { ProductId=productId}); //bizim repositrymiz product nesnesi ile çalışıyor bundan olayı int olan product id yi bir produt nesnesi olarak gnderiyoruz
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
            return BadRequest();
        }
        [HttpGet("GetProductDetails")]
        public IActionResult GetProductsWithDetails()
        {
            try
            {
                var result = _productDal.GetProductsWithDetails();
                return Ok(result);
            }
            catch 
            {
            }
            return BadRequest();
        }
    }
}
