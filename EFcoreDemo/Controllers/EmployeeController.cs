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
        public async Task<IActionResult> Update(string empName, string EmpEmail, int EmpSalary,CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateEmployeeCommand(empName, EmpEmail, EmpSalary), cancellationToken);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
