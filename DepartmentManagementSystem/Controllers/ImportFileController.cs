using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using DataAccess.Data;

namespace DepartmentManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportFileController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImportFileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Import()
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string docPath = Path.Combine(contentRootPath, "App_Data\\docs");
            string fileName = "test data.xlsx";
            string result = Path.Combine(docPath, fileName);
            ExcelDocumentReader.Read(result);
            return Ok();
        }
    }
}
