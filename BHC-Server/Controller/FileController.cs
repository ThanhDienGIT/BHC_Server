using BHC_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BookingHealthCare_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly DB_BHCContext _Context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public FileController(DB_BHCContext context,IWebHostEnvironment hostEnvironment)
        {
            _Context = context;
            _hostEnvironment = hostEnvironment;
        }

            [HttpPost]
            public async Task<IActionResult> OnPostUploadAsync(IFormFile files)
            {

                    if (files != null)
                    {
                        using (var stream = System.IO.File.Create("D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\XacThucImg\\" + files.FileName))
                        {
                            await files.CopyToAsync(stream);      
                            stream.Flush();
                            return Ok(files.FileName);
                        }
                
               }
            return BadRequest("failed save");
            }

        [HttpPost("avatar/{idnguoidung}")]
        public async Task<IActionResult> avatar(IFormFile files,int idnguoidung)
        {

            if (files != null)
            {
                using (var stream = System.IO.File.Create("D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\UserImg\\" + idnguoidung + files.FileName))
                {
                    await files.CopyToAsync(stream);
                    stream.Flush();
                    return Ok("Sucess");
                }

            }
            return BadRequest("failed save");
        }

        [HttpPost("MultifileChuyenKhoa/{idchuyenkhoa}")]
        public async Task<IActionResult> MultifileChuyenKhoa(List<IFormFile> files,int idchuyenkhoa)
        {
            if (files.Count == 0)
            {
                return BadRequest("Failed");
            }

            foreach (var formFile in files)
            {
                if (formFile != null)
                {
                    if(idchuyenkhoa == 0)
                    {
                        using (var stream = System.IO.File.Create("D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\ChuyenKhoaImg\\"  + formFile.FileName))
                        {
                            await formFile.CopyToAsync(stream);
                            stream.Flush();
                        }
                    }
                    else
                    {
                        using (var stream = System.IO.File.Create("D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\ChuyenKhoaImg\\" + idchuyenkhoa + formFile.FileName))
                        {
                            await formFile.CopyToAsync(stream);
                            stream.Flush();
                        }
                    }
                    
                }
            }

            if (files.Count != 0)
            {
                return Ok("Success");
            }
            return BadRequest("Failed");
            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.
        }


        [HttpPost("Multifile/{idnguoidung}")]
            public async Task<IActionResult> OnPostUploadMultiAsync(List<IFormFile> files,int idnguoidung)
            {
                if(files.Count == 0)
                {
                    return BadRequest("Failed");
            }

                foreach (var formFile in files)
                {
                    if (formFile != null)
                    {
                        using (var stream = System.IO.File.Create("D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\XacThucImg\\" + idnguoidung + formFile.FileName))
                        {
                            await formFile.CopyToAsync(stream);
                            stream.Flush();
                        }
                    }
                }

                if(files.Count != 0)
                {
                    return Ok("Success");
                }
                    return BadRequest("Failed");
                // Process uploaded files
                // Don't rely on or trust the FileName property without validation.
            }


        [HttpPost("Multifiledoctor/{iddoctor}")]
        public async Task<IActionResult> Multifiledoctor(List<IFormFile> files, string iddoctor)
        {
            if (files.Count == 0)
            {
                return BadRequest("Failed");
            }

            foreach (var formFile in files)
            {
                if (formFile != null)
                {
                    using (var stream = System.IO.File.Create("D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\CoSoYTeImg\\" + iddoctor + formFile.FileName))
                    {
                        await formFile.CopyToAsync(stream);
                        stream.Flush();
                    }
                }
            }

            if (files.Count != 0)
            {
                return Ok("Success");
            }
            return BadRequest("Failed");
            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.
        }

        [HttpPost("Multifileadmin/{idadmin}")]
        public async Task<IActionResult> Multifileadmin(List<IFormFile> files, string idadmin)
        {
            if (files.Count == 0)
            {
                return BadRequest("Failed");
            }

            foreach (var formFile in files)
            {
                if (formFile != null)
                {
                    using (var stream = System.IO.File.Create("D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\AdminImg\\" + idadmin + formFile.FileName))
                    {
                        await formFile.CopyToAsync(stream);
                        stream.Flush();
                    }
                }
            }

            if (files.Count != 0)
            {
                return Ok("Success");
            }
            return BadRequest("Failed");
            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.
        }




            [HttpGet("{fileName}")]
            public  IActionResult GetImage([FromRoute] string fileName)
            {
                string path = "D:\\Build project\\Server\\BookingHealthCare-Server\\BookingHealthCare-Server\\Image\\XacThucImg\\";
                var filepath = path + fileName + ".jpg";
                if (System.IO.File.Exists(filepath))
                {
                  byte[] b = System.IO.File.ReadAllBytes(filepath);
                  return File(b, "image/png");
                }
                return BadRequest("No Image");
            }

            [HttpGet("ChuyenKhoa/{fileName}")]
            public IActionResult ChuyenKhoa([FromRoute] string fileName)
            {
                string path = "D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\ChuyenKhoaImg\\";
                var filepath = path + fileName ;
                if (System.IO.File.Exists(filepath))
                {
                    byte[] b = System.IO.File.ReadAllBytes(filepath);
                    return File(b, "image/png");
                }
                return BadRequest("No Image");
            }

        [HttpGet("Doctor/{fileName}")]
            public  IActionResult GetImageDoctor([FromRoute] string fileName)
            {
                string path = "D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\CoSoYTeImg\\";
                var filepath = path + fileName ;
                if (System.IO.File.Exists(filepath))
                {
                    byte[] b = System.IO.File.ReadAllBytes(filepath);
                    return File(b, "image/png");
                }
                return Ok("anhmacdinh");
            }


            [HttpGet("/XacThuc/{fileName}")]
            public  IActionResult GetImageXacthuc([FromRoute] string fileName)
            {
                string path = "D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\XacThucImg\\";
                var filepath = path + fileName ;
                if (System.IO.File.Exists(filepath))
                {
                    byte[] b = System.IO.File.ReadAllBytes(filepath);
                    return File(b, "image/png");
                }
                return BadRequest("No Image");
            }

        [HttpGet("/Admin/{fileName}")]
        public IActionResult Admin([FromRoute] string fileName)
        {
            string path = "D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\AdminImg\\";
            var filepath = path + fileName;
            if (System.IO.File.Exists(filepath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filepath);
                return File(b, "image/png");
            }
            return BadRequest("No Image");
        }



        [HttpGet("/AvatarNguoiDung/{fileName}")]
        public IActionResult GetAvatarNguoiDung([FromRoute] string fileName)
        {
            string path = "D:\\Build project\\Server\\BHC-Server\\BHC-Server\\Img\\UserImg\\";
            var filepath = path + fileName;
            if (System.IO.File.Exists(filepath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filepath);
                return File(b, "image/png");
            }
            return BadRequest("No Image");
        }


    }
}




