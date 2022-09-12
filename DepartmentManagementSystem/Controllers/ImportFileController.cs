using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using DataAccess.Data;
using Core.Interfaces;
using Core.Domain;
using DataAccess;
using DataAccess.Repositories;

namespace DepartmentManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportFileController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<Department> _departmentRepository;

        public ImportFileController(IWebHostEnvironment webHostEnvironment, IRepository<Department> departmentRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            this._departmentRepository = departmentRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Import()
        {
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string docPath = Path.Combine(contentRootPath, "App_Data\\docs");
            string fileName = "test data.xlsx";
            string result = Path.Combine(docPath, fileName);
            ExcelDocumentReader reader = new ExcelDocumentReader();
            await reader.ReadAsync(result);

            await _departmentRepository.AddRangeAsync(DataFactory.Departments);
            return Ok();
        }
    }
}
