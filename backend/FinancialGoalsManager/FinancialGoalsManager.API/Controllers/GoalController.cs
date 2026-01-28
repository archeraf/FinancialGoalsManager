using FinancialGoalsManager.Application.DTO.InputModels;
using FinancialGoalsManager.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FinancialGoalsManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalController : ControllerBase
    {
        private readonly IGoalsService _goalsService;

        public GoalController(IGoalsService goalsService)
        {
            _goalsService = goalsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGoals()
        {
            try
            {
                var goals = await _goalsService.GetGoalsAsync();
                return Ok(goals);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGoalById(Guid id)
        {
            var goal = await _goalsService.GetGoalByIdAsync(id);
            if (goal is null)
                return NotFound();
            return Ok(goal);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal([FromBody] CreateGoalInputModel createModel)
        {
            if (createModel is null)
                return BadRequest("Create model cannot be null.");

            var created = await _goalsService.CreateGoalAsync(createModel);

            return Created(string.Empty, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateGoal(Guid id, [FromBody] UpdateGoalInputModel updateModel)
        {
            if (updateModel is null)
                return BadRequest("Update model cannot be null.");

            if (id != updateModel.Id)
                return BadRequest("Route id must match payload id.");

            var updated = await _goalsService.UpdateGoalAsync(updateModel);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGoal(Guid id)
        {
            await _goalsService.DeleteGoalAsync(id);
            return NoContent();
        }
    }
}
