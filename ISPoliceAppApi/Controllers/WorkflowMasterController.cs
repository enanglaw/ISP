using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ISPoliceAppApi.Data;
using ISPoliceAppApi.Models;

namespace ISPoliceAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class WorkflowMasterController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;

    public WorkflowMasterController(ISPoliceAppApiDbContext context)
    {
      _context = context;
    }

    // GET: api/WorkflowMaster
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkflowMaster>>> GetWorkflowMaster()
    {
      return await _context.WorkflowMaster.ToListAsync();
    }

    // GET: api/WorkflowMaster/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkflowMaster>> GetWorkflowMaster(int id)
    {
      var workflowMaster = await _context.WorkflowMaster.FindAsync(id);

      if (workflowMaster == null)
      {
        return NotFound();
      }

      return workflowMaster;
    }

    // PUT: api/WorkflowMaster/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorkflowMaster(int id, [FromBody] WorkflowMaster workflowMaster)
    {
      workflowMaster.WorkflowId=id;
      if (id != workflowMaster.WorkflowId)
      {
        return BadRequest();
      }

      _context.Entry(workflowMaster).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!WorkflowMasterExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/WorkflowMaster
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<WorkflowMaster>> PostWorkflowMaster([FromBody] WorkflowMaster workflowMaster)
    {
      _context.WorkflowMaster.Add(workflowMaster);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetWorkflowMaster", new { id = workflowMaster.WorkflowId }, workflowMaster);
    }

    // DELETE: api/WorkflowMaster/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<WorkflowMaster>> DeleteWorkflowMaster(int id)
    {
      var workflowMaster = await _context.WorkflowMaster.FindAsync(id);
      if (workflowMaster == null)
      {
        return NotFound();
      }

      //_context.WorkflowMaster.Remove(workflowMaster);
      workflowMaster.IsActive = false;
      _context.Entry(workflowMaster).State = EntityState.Modified;

      await _context.SaveChangesAsync();
      return workflowMaster;
    }

    private bool WorkflowMasterExists(int id)
    {
      return _context.WorkflowMaster.Any(e => e.WorkflowId == id);
    }
  }
}
