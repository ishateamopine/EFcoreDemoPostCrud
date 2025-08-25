using EFcoreDemo.CQRS.Employees.Command.Delete;
using EFcoreDemo.CQRS.Employees.Command.Insert;
using EFcoreDemo.CQRS.Employees.Command.Update;
using EFcoreDemo.CQRS.Employees.Query.GetAll;
using EFcoreDemo.Models.DataContext;
using EFcoreDemo.Models.Domain;
using EFcoreDemo.Repositories.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EFcoreDemo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DataContext _context;
        private readonly IMediator _mediator;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(DataContext context,IMediator mediator,IEmployeeRepository employeeRepository) 
        {
            _context = context;
            _mediator = mediator;
            _employeeRepository = employeeRepository;
        }
        #region
        /// <summary>
        // Retrieves all employees.
        /// </summary>
        public async Task<IActionResult> Index(CancellationToken cancellationToken)   
        {
            var employees = await _mediator.Send(new GetAllEmployeesCommand(), cancellationToken);
            return View(employees);
        }
        #endregion

        #region
        /// <summary>
        // Creates a new employee.
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee, CancellationToken cancellationToken)
        {
            await _mediator.Send(new InsertEmployeeCommand(employee), cancellationToken);
            return RedirectToAction("Index");
        }
        #endregion
        #region
        /// <summary>
        // Retrieves the details of an employee by ID.
        /// </summary>
        public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Employee employee,CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateEmployeeCommand(employee.EmpId,employee.EmpName, employee.EmpEmail, employee.EmpSalary), cancellationToken);
            return RedirectToAction("Index");
        }
        #endregion
        #region
        /// <summary>
        // Deletes an employee by ID.
        /// </summary>
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var Employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return View(Employee);
        }
        [HttpPost] 
        public async Task<IActionResult> Delete(Employee employee , CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteEmployeeCommand(employee.EmpId), cancellationToken);
            return RedirectToAction("Index");
        }
        #endregion
        #region
        /// <summary>
        // Selects an employee by ID.
        /// </summary>
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var emplpoyee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return View(emplpoyee);
        }
        #endregion
    }
}
