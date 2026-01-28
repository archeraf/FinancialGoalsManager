using FinancialGoalsManager.Application.DTO.InputModels;
using FinancialGoalsManager.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FinancialGoalsManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            try
            {
                var transactions = await _transactionService.GetTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            try
            {
                var transaction = await _transactionService.GetTransactionsById(id);
                if (transaction is null)
                    return NotFound(new { message = "Transaction not found." });

                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTranscationInputModel createModel)
        {
            try
            {
                if (createModel is null)
                    return BadRequest(new { message = "Create model cannot be null." });

                var created = await _transactionService.CreateTransaction(createModel);
                return Created(string.Empty, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] UpdateTranscationInputModel updateModel)
        {
            try
            {
                if (updateModel is null)
                    return BadRequest(new { message = "Update model cannot be null." });

                if (id != updateModel.Id)
                    return BadRequest(new { message = "Route id must match payload id." });

                var updated = await _transactionService.UpdateTransaction(updateModel);
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            try
            {
                await _transactionService.DeleteTransaction(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
