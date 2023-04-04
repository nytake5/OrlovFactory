using AutoMapper;
using BLL.Interfaces;
using Entities;
using Entities.Exceptions;
using Factory.WebApi.Views;
using Microsoft.AspNetCore.Mvc;

namespace Factory.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckpointController : ControllerBase
    {
        private readonly ICheckpointLogic _checkpointLogic;
        private readonly IMapper _mapper;

        public CheckpointController(
            ICheckpointLogic checkpointLogic,
            IMapper mapper)
        {
            _checkpointLogic = checkpointLogic;
            _mapper = mapper;
        }

        /// <summary>
        /// Starting shift for employee
        /// Maybe return ShiftException, KeyNotFoundException
        /// If last shift don't closed
        /// </summary>
        /// <param name="workingShift"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StartShift")]
        public IActionResult StartShift(StartWorkingShiftView workingShift)
        {
            try
            {
                var shift = _mapper.Map<WorkingShift>(workingShift);
                
                _checkpointLogic.StartShift(shift);
                
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest($"{ex.GetType()}: {ex.Message}");
            }
            catch (ShiftException ex)
            {
                return BadRequest($"{ex.GetType()}: {ex.Message}");
            }
        }

        /// <summary>
        /// Ending shift for employee
        /// Maybe return ShiftException, KeyNotFoundException
        /// If last shift don't opened
        /// </summary>
        /// <param name="workingShift"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EndShift")]
        public IActionResult EndShift(EndWorkingShiftView workingShift)
        {
            try
            {
                _checkpointLogic.EndShift(_mapper.Map<WorkingShift>(workingShift));
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest($"{ex.GetType()}: {ex.Message}");
            }
            catch (ShiftException ex)
            {
                return BadRequest($"{ex.GetType()}: {ex.Message}");
            }
        }
    }
}
