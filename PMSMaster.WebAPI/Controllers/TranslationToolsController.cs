using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class TranslationToolsController : ControllerBase
    {
        private readonly ITranslationToolsRepository _translationToolsRepository;

        public TranslationToolsController(ITranslationToolsRepository translationToolsRepository)
        {
            _translationToolsRepository = translationToolsRepository;
        }

        [HttpGet]
        public IActionResult GetTranslationTools()
        {
            try
            {
                var translationTools = _translationToolsRepository.GetTranslationTools();
                return Ok(translationTools);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "ServerError",
                    message = ex.Message
                });
            }
        }
        
        [HttpGet]
        public IActionResult GetTranslationToolsById(int Id)
        {
            try
            {
                var clientIndustries = _translationToolsRepository.GetTranslationToolsById(Id);
                return Ok(clientIndustries);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    error = "InvalidInput",
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "ServerError",
                    message = ex.Message
                });
            }
        }
        
        [HttpPost]
        public IActionResult AddTranslationTools(TranslationTools translationTools)
        {
            try
            {
                var addCurrency = _translationToolsRepository.Add(translationTools);
                return Ok(addCurrency);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "ServerError",
                    message = ex.Message
                });
            }
        }
        
        [HttpPost]
        public IActionResult UpdateTranslationTools(TranslationTools translationTools)
        {
            try
            {
                var updateCurrency = _translationToolsRepository.Update(translationTools);
                return Ok(updateCurrency);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "ServerError",
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult DeleteTranslationToolsByID(int Id)
        {
            try
            {
                var model = _translationToolsRepository.GetTranslationToolsById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _translationToolsRepository.Update(model);
                   
                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Translator Tools",
                    message = ex.Message
                });
            }
        }

    }
}
