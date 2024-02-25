using Application.Interfaces.Person;
using Domain.ViewModels.Person;
using ExpertSenderInterviewApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpertSenderInterviewApp.Controllers
{
	public class PersonController : Controller
	{
		private readonly ILogger<PersonController> _logger;
		private readonly IGetPersonService getPersonService;
		private readonly IDeletePersonService deletePersonService;
		private readonly ICreatePersonService createPersonService;
		private readonly IEditPersonService editPersonService;

		public PersonController(
			ILogger<PersonController> logger,
			IGetPersonService getPersonService,
			IDeletePersonService deletePersonService,
			ICreatePersonService createPersonService,
			IEditPersonService editPersonService)
		{
			_logger = logger;
			this.getPersonService = getPersonService;
			this.deletePersonService = deletePersonService;
			this.createPersonService = createPersonService;
			this.editPersonService = editPersonService;
		}

		public IActionResult List()
		{
			var persons = getPersonService.GetAllPersons();
			return View(new AllPersonsViewModel(persons));
		}

		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreatePersonViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			await createPersonService.CreatePerson(viewModel.ConvertToDto());
			return RedirectToAction(nameof(List));
		}

		public async Task<IActionResult> Delete(int id)
		{
			await deletePersonService.DeletePerson(id);
			return RedirectToAction(nameof(List));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var person = await getPersonService.GetPerson(id);

			if (person is not null)
				return View(new EditPersonViewModel(person));
			return RedirectToAction(nameof(List));
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditPersonViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			await editPersonService.EditPerson(viewModel.ConvertToDto());
			return RedirectToAction(nameof(List));
		}
	}
}
