using AutoMapper;
using Core.Domain;
using Core.Interfaces;
using DepartmentManagementSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using DepartmentManagementSystem.Extensions;

namespace DepartmentManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IRepository<Department> departmentRepository, IMapper mapper)
        {
            this._departmentRepository = departmentRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<DepartmentResponse>> GetDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var departmentModelList = departments.Select(x => _mapper.Map<DepartmentResponse>(x));
            return Ok(departmentModelList);

        }
        [HttpGet("{parentId}")]
        public async Task<ActionResult<DepartmentResponse>> GetDepartmentByIdAsync(int parentId)
        {
            Expression<Func<Department, bool>> expression = x => x.ParentDepartmentId == parentId;
            var departmentList = await _departmentRepository.GetByConditionAsync(expression);
            if (departmentList.Count() == 0)
                return NotFound();

            var departmentModelList = departmentList.Select(x => _mapper.Map<DepartmentResponse>(x));
            return Ok(departmentModelList);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartmentAsync(CreateOrEditDepartmentRequest model)
        {
            var department = _mapper.Map<Department>(model);
            await _departmentRepository.AddAsync(department);

            return Ok(department.Id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartmentAsync(int id, CreateOrEditDepartmentRequest model)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                return NotFound();
            
            if (id != department.Id)
            {
                return BadRequest("Incorrect department!");

            }
            department = _mapper.Map<Department>(model);
            await _departmentRepository.UpdateAsync(department);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartmentAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            await _departmentRepository.RemoveAsync(department);
            return NoContent();
        }
        [HttpGet("/summary")]
        public async Task<ActionResult<SummaryResponse>> GetUsersAndPositionsByDepartmentAsync()
        {
            IEnumerable<Department> departmentList = await _departmentRepository.GetAllAsync();
            List<SummaryResponse> summaryResponse = GetSummary(departmentList);

            return Ok(summaryResponse);
        }
        private List<SummaryResponse> GetSummary(IEnumerable<Department> departmentList)
        {
            List<SummaryResponse> summaryResponse = new List<SummaryResponse>();
            foreach (var rootDepartment in departmentList)
            {
                int usersCount = 0;
                int positionsCount = 0;
                rootDepartment.BypassTrees((d) => {
                    usersCount += d.Users.Count;
                    positionsCount += d.Users.Select(u => u.Position).Distinct().Count();
                });
                summaryResponse.Add(new SummaryResponse()
                {
                    DepartmentName = rootDepartment.Name,
                    UsersCount = usersCount,
                    PositionsCount = positionsCount
                }); ;
            }
            return summaryResponse;
        }
        
    }
}