using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VinhGroupLearnWebAPI.Models;

namespace VinhGroupLearnWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {

        public static List<HangHoa> hangHoas = new List<HangHoa>();
        
        
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);
        }
        [HttpGet("{id}")]
        public IActionResult GetById (string id)
        {
            try
            {
                // sử dụng LINQ check mã hh
                var hangHoa = hangHoas.SingleOrDefault(c => c.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                return Ok(hangHoa);
            }
            catch 
            {

                return BadRequest();
            }
        }
        
        [HttpPost]    
        public IActionResult Create(HangHoaVM hangHoaVM)
        {
            var hangHoa = new HangHoa
            {

                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hangHoaVM.TenHangHoa,
                DonGia = hangHoaVM.DonGia
            };

            hangHoas.Add(hangHoa);
            return Ok(new
            {
                Success = true, Data = hangHoa
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, HangHoa hangHoaEdit)
        {
            try
            {
                // sử dụng LINQ check mã hh
                var hangHoa = hangHoas.SingleOrDefault(c => c.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                if (id != hangHoa.MaHangHoa.ToString())
                {
                    return BadRequest();
                }
              // tìm đc thì Update
              hangHoa.TenHangHoa = hangHoaEdit.TenHangHoa;
                hangHoa.DonGia = hangHoaEdit.DonGia;
                return Ok();
            }
            catch
            {

                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {

            try
            {
                // sử dụng LINQ check mã hh
                var hangHoa = hangHoas.SingleOrDefault(c => c.MaHangHoa == Guid.Parse(id));
                if (hangHoa == null)
                {
                    return NotFound();
                }
                // tìm đc thì Xoa
                hangHoas.Remove(hangHoa);
                return Ok();
            }
            catch
            {

                return BadRequest();
            }
        }
        
    }
}
