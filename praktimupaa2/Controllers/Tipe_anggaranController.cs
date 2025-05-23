using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praktimupaa2.Models.Tipe_anggaran;

namespace praktimupaa2.Controllers
{
    [Authorize]
    [Route("bangunin/v1/[controller]")]
    public class Tipe_anggaranController : ControllerBase
    {
        private readonly string _consStr;

        public Tipe_anggaranController(IConfiguration configuration)
        {
            _consStr = configuration.GetConnectionString("DefaultConnection");
        }


        [HttpGet]
        public ActionResult<Tipe_anggaran> GetTipe_anggarans()
        {
            Tipe_anggaranContext context = new Tipe_anggaranContext(_consStr);
            List<Tipe_anggaran> tipe_anggarans = context.GetAllTipe_anggarans();
            return Ok(tipe_anggarans);
        }


        [HttpPost]
        public ActionResult CreateTipe_anggaran([FromBody] Tipe_anggaran tipe_anggaran)
        {
            Tipe_anggaranContext context = new Tipe_anggaranContext(_consStr);
            bool success = context.AddTipe_anggaran(tipe_anggaran);
            if (success)
                return Ok(tipe_anggaran);
            return BadRequest("Gagal membuat tipe anggaran");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTipe_anggaran(int id, [FromBody] Tipe_anggaran tipe_anggaran)
        {
            if (id != tipe_anggaran.id_tipe_anggaran)
                return BadRequest("ID tidak ada");

            Tipe_anggaranContext context = new Tipe_anggaranContext(_consStr);
            bool success = context.UpdateTipe_anggaran(tipe_anggaran);
            if (success)
                return Ok(new { message = $"Update tipe_anggaran dengan id {id} berhasil" });
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTipe_anggaran(int id)
        {
            Tipe_anggaranContext context = new Tipe_anggaranContext(_consStr);
            bool success = context.DeleteTipe_anggaran(id);
            if (success)
                return Ok(new { message = $"Menghapus Tipe_anggaran dengan id {id} berhasil" });
            return NotFound();
        }
    }
}

