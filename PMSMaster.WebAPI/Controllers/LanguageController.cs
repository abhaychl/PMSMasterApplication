using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageController(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [HttpGet]
        public IActionResult GetLanguage()
        {
            try
            {
                var clientIndustries = _languageRepository.GetLanguage();
                return Ok(clientIndustries);
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
        public IActionResult GetLanguageById(int Id)
        {
            try
            {
                var language = _languageRepository.GetLanguageById(Id);
                return Ok(language);
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
        public IActionResult AddLanguage(Language language)
        {
            try
            {
                var addCurrency = _languageRepository.Add(language);
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
        public IActionResult UpdateLanguage(Language language)
        {
            try
            {
                var updateCurrency = _languageRepository.Update(language);
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
        public IActionResult DeleteLanguageByID(int Id)
        {
            try
            {
                var model = _languageRepository.GetLanguageById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _languageRepository.Update(model);
                   
                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Language",
                    message = ex.Message
                });
            }
        }

    }
}
