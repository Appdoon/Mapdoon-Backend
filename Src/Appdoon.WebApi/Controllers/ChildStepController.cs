using Appdoon.Application.Services.ChildSteps.Command.CreateChildStepService;
using Appdoon.Application.Services.ChildSteps.Command.DeleteChildStepService;
using Appdoon.Application.Services.ChildSteps.Command.UpdateChildStepService;
using Appdoon.Application.Services.ChildSteps.Query.GetAllChildStepsService;
using Appdoon.Application.Services.ChildSteps.Query.GetIndividualChildStepService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Appdoon.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildStepController : ControllerBase
    {
        //Get All
        private readonly IGetAllChildStepsService _getAllChildStepsService;
        //Get Individual
        private readonly IGetIndividualChildStepService _getIndividualChildStepService;
        //Create
        private readonly ICreateChildStepService _createChildStepService;
        //Delete
        private readonly IDeleteChildStepService _deleteChildStepService;
        //Update
        private readonly IUpdateChildStepService _updateChildStepService;


        public ChildStepController(IGetAllChildStepsService getAllChildStepsService,
                                  IGetIndividualChildStepService getIndividualChildStepService,
                                  ICreateChildStepService createChildStepService,
                                  IDeleteChildStepService deleteChildStepService,
                                  IUpdateChildStepService updateChildStepService)
        {
            _getAllChildStepsService = getAllChildStepsService;
            _getIndividualChildStepService = getIndividualChildStepService;
            _createChildStepService = createChildStepService;
            _deleteChildStepService = deleteChildStepService;
            _updateChildStepService = updateChildStepService;
        }
        // GET: api/<ChildStepController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _getAllChildStepsService.Execute();
            return Ok(result);
        }

        // GET api/<ChildStepController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _getIndividualChildStepService.Execute(id);
            return Ok(result);
        }

        // POST api/<ChildStepController>
        [HttpPost]
        public IActionResult Post(CreateChildStepDto createChildStepDto)
        {
            var result = _createChildStepService.Execute(createChildStepDto);
            return Ok(result);
        }

        // PUT api/<ChildStepController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateChildStepDto updateChildStepDto)
        {
            var result = _updateChildStepService.Execute(id, updateChildStepDto);
            return Ok(result);
        }

        // DELETE api/<ChildStepController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _deleteChildStepService.Execute(id);
            return Ok(result);
        }
    }
}
