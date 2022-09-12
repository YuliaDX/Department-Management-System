using AutoMapper;
using Core.Domain;
using Core.Interfaces;
using DepartmentManagementSystem.DTO;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<DepartmentShortResponse>> GetDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var departmentModelList = departments.Select(x => _mapper.Map<DepartmentShortResponse>(x));
            return Ok(departmentModelList);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentResponse>> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            DepartmentResponse departmentModel = _mapper.Map<DepartmentResponse>(department);
            return Ok(departmentModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartmentAsync(CreateOrEditDepartmentRequest model)
        {
            var department = _mapper.Map<Department>(model);
            await _departmentRepository.AddAsync(department);

            return CreatedAtAction(
             nameof(GetDepartmentByIdAsync),
             new { id = department.Id },
             null);
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
        [HttpGet("{id}/users")]
        public async Task<ActionResult<UserResponse>> GetUsersByDepartmentAsync(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
                return NotFound();

            var userPerDepartmentList = department.Users.Select(user => _mapper.Map<UserResponse>(user));
            return Ok(userPerDepartmentList);
        }
        [HttpGet("{id}/positions")]
        public async Task<ActionResult<UserResponse>> GetPositionsByDepartmentAsync(int departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);
            if (department == null)
                return NotFound();

            var positionPerDepartmentList = department.Users.Select(user => user.Position).ToList();
            return Ok(positionPerDepartmentList);
        }
    }
}