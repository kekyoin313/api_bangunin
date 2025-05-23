using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praktimupaa2.Models.Anggaran;

namespace praktimupaa2.Controllers
{
    [Authorize]
    [Route("bangunin/v1/[controller]")]
    public class AnggaranController : ControllerBase
    {
        private readonly string _consStr;

        public AnggaranController(IConfiguration configuration)
        {
            _consStr = configuration.GetConnectionString("DefaultConnection");
        }


        [HttpGet]
        public ActionResult<Anggaran> GetAnggarans()
        {
            AnggaranContext context = new AnggaranContext(_consStr);
            List<Anggaran> anggarans = context.GetAllAnggarans();
            return Ok(anggarans);
        }

        [HttpGet("{id}")]
        public ActionResult<Anggaran> GetAnggaranById(int id)
        {
            AnggaranContext context = new AnggaranContext(_consStr);
            Anggaran anggaran = context.GetAnggaranById(id);
            if (anggaran == null)
                return NotFound();
            return Ok(anggaran);
        }

        [HttpPost]
        public ActionResult CreateAnggaran([FromBody] Anggaran anggaran)
        {
            AnggaranContext context = new AnggaranContext(_consStr);
            bool success = context.AddAnggaran(anggaran);
            if (success)
                return Ok(anggaran);
            return BadRequest("Gagal membuat anggaran");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAnggaran(int id, [FromBody] Anggaran anggaran)
        {
            if (id != anggaran.id_anggaran)
                return BadRequest("ID tidak ditemukan");

            AnggaranContext context = new AnggaranContext(_consStr);
            bool success = context.UpdateAnggaran(anggaran);
            if (success)
                return Ok(new { message = $"Update anggaran dengan id {id} berhasil" });
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAnggaran(int id)
        {
            AnggaranContext context = new AnggaranContext(_consStr);
            bool success = context.DeleteAnggaran(id);
            if (success)
                return Ok(new { message = $"Menghapus anggaran dengan id {id} berhasil" });
            return NotFound();
        }
    }
}
