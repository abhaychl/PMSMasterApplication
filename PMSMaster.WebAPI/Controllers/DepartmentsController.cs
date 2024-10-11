using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsRepository _departmentsRepository;

        public DepartmentsController(IDepartmentsRepository departmentsRepository)
        {
            _departmentsRepository = departmentsRepository;
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            try
            {
                var clientIndustries = _departmentsRepository.GetDepartments();
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
        public IActionResult GetDepartmentsById(int Id)
        {
            try
            {
                var clientIndustries = _departmentsRepository.GetDepartmentsById(Id);
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
        public IActionResult AddDepartments(Departments departments)
        {
            try
            {
                var addDepartments = _departmentsRepository.Add(departments);
                return Ok(addDepartments);
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
        public IActionResult UpdateDepartments(Departments departments)
        {
            try
            {
                var updateDepartments = _departmentsRepository.Update(departments);
                return Ok(updateDepartments);
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
        public IActionResult DeleteDepartmentsByID(int Id)
        {
            try
            {
                var model = _departmentsRepository.GetDepartmentsById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _departmentsRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Departments",
                    message = ex.Message
                });
            }
        }

    }
}
