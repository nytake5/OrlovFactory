using AutoMapper;
using BLL.Interfaces;
using Entities;
using Factory.WebApi.Views;
using Microsoft.AspNetCore.Mvc;

namespace Factory.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HrDepartmentController : ControllerBase
{
    private readonly IHrDeparmentLogic _hrDepartmentLogic;
    private readonly IMapper _mapper;
    
    public HrDepartmentController(IHrDeparmentLogic hrDepartmentLogic, IMapper mapper)
    {
        _hrDepartmentLogic = hrDepartmentLogic;
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
            return Ok(_hrDepartmentLogic.GetAllEmployees());
        }
        else
        {
            return Ok(_hrDepartmentLogic.GetAllEmployees(post));
        }
    }

    [HttpPost]
    public IActionResult CreateEmployee(EmployeeView employee)
    {
        try
        {
            _hrDepartmentLogic.CreateEmployee(_mapper.Map<Employee>(employee));
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
    /// return BadRequest(KeyNotFoundException)
    /// </summary>
    /// <param name="employee"></param>
    /// <returns></returns>
    [HttpPut]
    public IActionResult UpdateEmployee(EmployeeView employee)
    {
        try
        {
            _hrDepartmentLogic.UpdateEmployee(_mapper.Map<Employee>(employee));
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
        if (_hrDepartmentLogic.DeleteEmployee(passNumber))
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
            return Ok(_hrDepartmentLogic.GetById(passNumber));
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest($"{ex.GetType()}: {ex.Message}");
        }
        catch (Exception)
        {
            IActionResult badRequestObjectResult = BadRequest("Something went wrong!");
            return badRequestObjectResult;
        }
    }

    [HttpGet]
    [Route("ListOfPosts")]
    public IActionResult GetListOfPosts()
    {
        return Ok((Post[])Enum.GetValues(typeof(Post)));
    }
}