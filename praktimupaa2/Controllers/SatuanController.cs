using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praktimupaa2.Models.Satuan;

namespace praktimupaa2.Controllers
{
    [Authorize]
    [Route("bangunin/v1/[controller]")]
    public class SatuanController : ControllerBase
    {
        private readonly string _consStr;

        public SatuanController(IConfiguration configuration)
        {
            _consStr = configuration.GetConnectionString("DefaultConnection");
        }


        [HttpGet]
        public ActionResult<Satuan> GetSatuans()
        {
            SatuanContext context = new SatuanContext(_consStr);
            List<Satuan> satuans = context.GetAllSatuans();
            return Ok(satuans);
        }

        [HttpPost]
        public ActionResult CreateSatuan([FromBody] Satuan satuan)
        {
            SatuanContext context = new SatuanContext(_consStr);
            bool success = context.AddSatuan(satuan);

            if (success)
                return Ok("Berhasil menambahkan satuan");

            // KIRIM errorMessage agar kita bisa tahu kenapa gagal
            return BadRequest("Gagal membuat satuan: " + context.ErrorMessage);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateSatuan(int id, [FromBody] Satuan satuan)
        {
            if (id != satuan.id_satuan)
                return BadRequest("ID tidak ada");

            SatuanContext context = new SatuanContext(_consStr);
            bool success = context.UpdateSatuan(satuan);
            if (success)
                return Ok(new { message = $"Update satuan dengan id {id} berhasil" });
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSatuan(int id)
        {
            SatuanContext context = new SatuanContext(_consStr);
            bool success = context.DeleteSatuan(id);
            if (success)
                return Ok(new { message = $"Menghapus Satuan dengan id {id} berhasil" });
            return NotFound();
        }
    }
}

