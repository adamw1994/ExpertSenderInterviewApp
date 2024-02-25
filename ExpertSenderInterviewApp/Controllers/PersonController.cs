using Application.Interfaces.Person;
using Domain.ViewModels.Person;
using ExpertSenderInterviewApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExpertSenderInterviewApp.Controllers
{
	public class PersonController : Controller
	{
		private readonly string ErrorMessageToUser = "B³¹d serwera. Spróbuj ponownie póŸniej";

		private readonly ILogger<PersonController> logger;
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
			this.logger = logger;
			this.getPersonService = getPersonService;
			this.deletePersonService = deletePersonService;
			this.createPersonService = createPersonService;
			this.editPersonService = editPersonService;
		}

		public IActionResult List()
		{
			try
			{
                var persons = getPersonService.GetAllPersons();
                return View(new AllPersonsViewModel(persons));
            }
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
                ViewBag.ErrorMessage = ErrorMessageToUser;
				return View(new AllPersonsViewModel());
            }
			
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

			try 
			{
				await createPersonService.CreatePerson(viewModel.ConvertToDto());
			}	
			catch (Exception ex)
			{
                logger.LogError(ex.Message);
                ViewBag.ErrorMessage = ErrorMessageToUser;
                return View();
			}

			return RedirectToAction(nameof(List));
		}

		public async Task<IActionResult> Delete(int id)
		{
			try
			{
                await deletePersonService.DeletePerson(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                ViewBag.ErrorMessage = ErrorMessageToUser;
            }

            return RedirectToAction(nameof(List));
		}

		public async Task<IActionResult> Edit(int id)
		{
			try
			{
                var person = await getPersonService.GetPerson(id);
                if (person is not null)
                    return View(new EditPersonViewModel(person));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                ViewBag.ErrorMessage = ErrorMessageToUser;
            }
            
			return RedirectToAction(nameof(List));
		}

		[HttpPost]
		public async Task<IActionResult> Edit(EditPersonViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return View(viewModel);

			try
			{
                await editPersonService.EditPerson(viewModel.ConvertToDto());
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                ViewBag.ErrorMessage = ErrorMessageToUser;
                return View(viewModel);
            }

        }
	}
}
