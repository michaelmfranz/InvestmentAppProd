using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvestmentApp.Persistence;

namespace InvestmentAppProd.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InvestmentController : Controller
    {
       
        private IGenericRepository<Investment> genericRepository = null;
        private InvestmentRespository investmentRepository = null;
 

        public InvestmentController(GenericRepository<Investment> _genericRepository)
        {
            genericRepository = _genericRepository;
            investmentRepository = new InvestmentRespository(_genericRepository.context);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Investment>> FetchInvestment()
        {
            try
            {
                return Ok(genericRepository.GetAll());
            }
            catch (Exception e)
            {
                return NotFound(e.ToString());
            }
        }

        [HttpGet("name")]
        public ActionResult<Investment> FetchInvestment([FromQuery] string name)
        {
            try
            {
                var investment = investmentRepository.Find(name);
                if (investment == null)
                    return NotFound();

                return Ok(investment);
            }
            catch (Exception e)
            {
                return NotFound(e.ToString());
            }
        }


        [HttpPost]
        public ActionResult<Investment> AddInvestment([FromBody] Investment investment)
        {
            try
            {
                if (investment.StartDate > DateTime.Now)
                    return BadRequest("Investment Start Date cannot be in the future.");

                investment.CalculateValue();

                genericRepository.Insert(investment);
                genericRepository.Save();

                return CreatedAtAction("AddInvestment", investment.Name, investment);
            }
            catch (DbUpdateException dbE)
            {
                return Conflict(dbE.ToString());
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPut("name")]
        public ActionResult UpdateInvestment([FromQuery] string name, [FromBody] Investment investment)
        {
            try
            {
                if (name != investment.Name)
                    return BadRequest("Name does not match the Investment you are trying to update.");

                if (investment.StartDate > DateTime.Now)
                    return BadRequest("Investment Start Date cannot be in the future.");

                investment.CalculateValue();

                genericRepository.Update(investment);

                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.ToString());
            }
        }

        [HttpDelete("name")]
        public ActionResult DeleteInvestment([FromQuery] string name)
        {
            try
            {
                var investment = investmentRepository.Find(name);
                if (investment == null)
                {
                    return NotFound();
                }


                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }

        }
    }
}
