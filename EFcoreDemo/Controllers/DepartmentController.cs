using EFcoreDemo.CQRS.Departments.Command.Create;
using EFcoreDemo.CQRS.Departments.Command.Delete;
using EFcoreDemo.CQRS.Departments.Command.Update;
using EFcoreDemo.CQRS.Departments.Queries.GetAll;
using EFcoreDemo.Models.DataContext;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFcoreDemo.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DataContext _context;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMediator _mediator;
        public DepartmentController(DataContext context, IDepartmentRepository departmentRepository,IMediator mediator)
        {
            _context = context;
            _departmentRepository = departmentRepository;
            _mediator = mediator;
        }
        #region
        /// <summary>
        // Retrieves all departments.
        /// </summary>
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var department = await _mediator.Send(new GetAllDepartmentCommand(), cancellationToken);
            return View(department);
        }
        #endregion
        #region
        /// <summary>
        // Creates a new department.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department, CancellationToken cancellationToken)
        {
            var dept = await _mediator.Send(new CreateDepartmentCommand(department), cancellationToken);
            return RedirectToAction("Index");

        }
        #endregion
        #region
        /// <summary>
        // Retrieves the details of a department by ID.
        /// </summary>
        public async Task<IActionResult> Update(int id, CancellationToken cancellation)
        {
            var dept = await _departmentRepository.GetDepartmentByIdAsync(id);
            return View(dept);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Department department, CancellationToken cancellationToken)
        {
            var dept = await _mediator.Send(new UpdateDepartmentCommand(department.DeptId,department.DeptName), cancellationToken);
            return RedirectToAction("Index");
        }
        #endregion
        #region
        /// <summary>
        // Deletes a department by ID.
        /// </summary>
        public async Task<IActionResult> Delete(int id , CancellationToken cancellationToken)
        {
            var dept = await _departmentRepository.GetDepartmentByIdAsync(id);
            return View(dept);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int deptId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteDepartmentCommand(deptId), cancellationToken);
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region
        /// <summary>
        // Details of a department by ID.
        /// </summary>
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var dept = await _departmentRepository.GetDepartmentByIdAsync(id);
            return View(dept);
        }
        #endregion
    }
}
