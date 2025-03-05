using AssociateApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssociateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociatesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AssociatesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAssociate>>> GetAssociates()
        {
            return await _context.TblAssociates.Include(a => a.Location).Include(a => a.Occupation).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TblAssociate>> GetAssociate(int id)
        {
            var associate = await _context.TblAssociates.FindAsync(id);
            if (associate == null)
            {
                return NotFound();
            }
            return associate;
        }

        [HttpPost]
        public async Task<ActionResult<TblAssociate>> CreateAssociate(TblAssociate associate)
        {
            if (await _context.TblAssociates.AnyAsync(a => a.Name == associate.Name))
            {
                return BadRequest("Name must be unique.");
            }

            _context.TblAssociates.Add(associate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAssociates), new { id = associate.AssociateId }, associate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssociate(int id, TblAssociate associate)
        {
            if (id != associate.AssociateId)
            {
                return BadRequest();
            }

            _context.Entry(associate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TblAssociates.Any(e => e.AssociateId == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssociate(int id)
        {
            var associate = await _context.TblAssociates.FindAsync(id);
            if (associate == null)
            {
                return NotFound();
            }

            _context.TblAssociates.Remove(associate);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
