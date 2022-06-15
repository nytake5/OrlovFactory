using AutoMapper;
using BLL_Interface;
using Debtus.TestTask.Entities;
using Debtus.TestTask.Model;
using Microsoft.AspNetCore.Mvc;

namespace Debtus.TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRDepartmentController : ControllerBase
    {
        private readonly IHRDeparmentLogic _hrDeparmentLogic;
        private readonly IMapper _mapper;

        public HRDepartmentController(IHRDeparmentLogic hrDeparmentLogic, IMapper mapper)
        {
            _hrDeparmentLogic = hrDeparmentLogic;
            _mapper = mapper;
        }

        /// <summary>
        /// Return list of Employee
        /// Post - optional parameter
        /// default value: 0
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllEmployees(Post post = 0)
        {
            if (post == 0)
            {
                return Ok(_hrDeparmentLogic.GetAllEmployees());
            }
            else
            {
                return Ok(_hrDeparmentLogic.GetAllEmployees(post));
            }
        }

        [HttpPost]
        public IActionResult CreateEmployee(EmployeeView employee)
        {
            try
            {
                _hrDeparmentLogic.CreateEmployee(_mapper.Map<Employee>(employee));
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"{ex.GetType()}: {ex.Message}");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Update Employee
        /// If passNumber no such exist
        /// return BadReques(KeyNotFoundException)
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateEmployee(EmployeeView employee)
        {
            try
            {
                _hrDeparmentLogic.UpdateEmployee(_mapper.Map<Employee>(employee));
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest($"{ex.GetType()}: {ex.Message}");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        /// <summary>
        /// Delete employee
        /// If passNumber no such exist
        /// return Object not found,
        /// because usually there is no error in deleting, if I didn't delete it
        /// so I did this
        /// </summary>
        /// <param name="passNumber"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteEmployee(int passNumber)
        {
            if (_hrDeparmentLogic.DeleteEmployee(passNumber))
            {
                return Ok();
            }
            else
            {
                return BadRequest("ObjectNotFound");
            }
        }

        /// <summary>
        /// Get employee by pass number
        /// if passnumber no such exist
        /// return BadReques(KeyNotFoundException)
        /// </summary>
        /// <param name="passNumber"></param>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetEmployeeByPassNumber(int passNumber)
        {
            try
            {
                return Ok(_hrDeparmentLogic.GetById(passNumber));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest($"{ex.GetType()}: {ex.Message}");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet]
        [Route("ListOfPosts")]
        public IActionResult GetListOfPosts()
        {
            return Ok((Post[])Enum.GetValues(typeof(Post)));
        }
    }
}
