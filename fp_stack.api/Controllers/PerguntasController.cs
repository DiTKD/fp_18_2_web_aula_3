using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fp_stack.core.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace fp_stack.api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    //[EnableCors("Default")]
    public class PerguntasController : ControllerBase
    {
        private Context _context;


        public PerguntasController(Context context)
        {
            _context = context;
        }

        //// GET: Perguntas
        //[HttpGet("PerguntasExemplo1")]
        //public List<Pergunta> PerguntasExemplo1()
        //{
        //    return  _context.Perguntas.ToList();
        //}


        //[HttpGet("PerguntasExemplo2")]
        //[ProducesResponseType(200, Type = typeof(List<Pergunta>))]
        //[ProducesResponseType(400)]
        //public IActionResult PerguntasExemplo2()
        //{
        //    var perguntass = _context.Perguntas.ToList();
        //    if (perguntass == null || perguntass.Count() == 0)
        //        return BadRequest();
        //    return  Ok(perguntass);
        //}

        //[HttpGet("PerguntasExemplo3")]
        //public ActionResult<List<Pergunta>> PerguntasExemplo3()
        //{
        //    var perguntass = _context.Perguntas.ToList();
        //    if (perguntass == null || perguntass.Count() == 0)
        //        return BadRequest();

        //    return perguntass;
        //}



        //[Route("")]
        [HttpGet]
        public ActionResult<List<Pergunta>> Get()
        {
            var perguntass = _context.Perguntas.ToList();
            if (perguntass == null || perguntass.Count() == 0)
                return BadRequest();

            return perguntass;
        }


        //[Route("")]
        [HttpPost]
        public IActionResult Post([FromBody] Pergunta pergunta)
        {
            //var perguntass = _context.Perguntas.ToList();
            //if (perguntass == null || perguntass.Count() == 0)
            //    return BadRequest();
            if (ModelState.IsValid)
            {
                _context.Perguntas.Add(pergunta);
                _context.SaveChanges();
                return Created($"/api/perguntas/{pergunta.Id}",pergunta);
            }

            return BadRequest(ModelState);
        }

       
        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody]Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                _context.Attach(pergunta);
                _context.Entry(pergunta).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();
                return Ok(pergunta);
            }

            return BadRequest(ModelState);
        }
    }
}