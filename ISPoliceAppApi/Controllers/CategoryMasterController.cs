using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ISPoliceAppApi.Data;
using ISPoliceAppApi.Models;
using ISPoliceAppApi.DTOs;
using AutoMapper;

namespace ISPoliceAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryMasterController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
    private readonly IMapper _mapper;

    public CategoryMasterController(ISPoliceAppApiDbContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    // GET: api/CategoryMaster
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryMaster>>> GetCategoryMaster()
    {
      return await _context.CategoryMaster.ToListAsync();
    }

    // GET: api/CategoryMaster/CategoryDropdown
    [HttpGet("CategoryDropdown")]
    public async Task<ActionResult<IEnumerable<CategoryDropdownDTO>>> GetCategoryDropdown()
    {
      var category = await _context.CategoryMaster.Include(x => x.SubCategoryMaster).OrderBy(x=>x.CategoryName).ToListAsync();
      var categoryDto = _mapper.Map<List<CategoryDropdownDTO>>(category);
      return categoryDto;
    }

    // GET: api/CategoryMaster/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryMaster>> GetCategoryMaster(int id)
    {
      var categoryMaster = await _context.CategoryMaster.FindAsync(id);

      if (categoryMaster == null)
      {
        return NotFound();
      }

      return categoryMaster;
    }

    // PUT: api/CategoryMaster/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategoryMaster(int id, CategoryMaster categoryMaster)
    {
      if (id != categoryMaster.CategoryId)
      {
        return BadRequest();
      }

      _context.Entry(categoryMaster).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CategoryMasterExists(id))
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

    // POST: api/CategoryMaster
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<CategoryMaster>> PostCategoryMaster(CategoryMaster categoryMaster)
    {
      _context.CategoryMaster.Add(categoryMaster);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetCategoryMaster", new { id = categoryMaster.CategoryId }, categoryMaster);
    }

    // DELETE: api/CategoryMaster/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<CategoryMaster>> DeleteCategoryMaster(int id)
    {
      var categoryMaster = await _context.CategoryMaster.FindAsync(id);
      if (categoryMaster == null)
      {
        return NotFound();
      }

      _context.CategoryMaster.Remove(categoryMaster);
      await _context.SaveChangesAsync();

      return categoryMaster;
    }

    private bool CategoryMasterExists(int id)
    {
      return _context.CategoryMaster.Any(e => e.CategoryId == id);
    }
        [HttpGet("AllCategoryDropdown")]
        public async Task<ActionResult<IEnumerable<CategoryDropdownDTO>>> GetAllCategoryDropdown()
        {
            var category = await _context.CategoryMaster.OrderBy(x => x.CategoryName).ToListAsync();
            var categoryDto = _mapper.Map<List<CategoryDropdownDTO>>(category);
            return categoryDto;
        }
    }
}
