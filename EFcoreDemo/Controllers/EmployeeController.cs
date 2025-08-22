using EFcoreDemo.CQRS.Employees.Query.GetAll;
using EFcoreDemo.Models.DataContext;
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
    }
}
