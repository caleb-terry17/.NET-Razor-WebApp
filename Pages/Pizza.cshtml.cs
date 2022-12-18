using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesPizza.Models;
using RazorPagesPizza.Services;

namespace RazorPagesPizza.Pages
{
    public class PizzaModel : PageModel
    {
        public List<Pizza> pizzas = new();
        [BindProperty]
        public Pizza NewPizza { get; set; } = new();

        public string GlutenFreeText(Pizza pizza) 
        {
            return pizza.IsGlutenFree ? "Gluten Free" : "Not Gluten Free";
        }

        public void OnGet()
        {
            pizzas = PizzaService.GetAll();
        }

        public IActionResult OnPost() 
        {
            // uses the model to check if a pizza is valid
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // if the new pizza being posted is valid
            // add it to the list and then redirect to GET request
            PizzaService.Add(NewPizza);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id) 
        {
            PizzaService.Delete(id);
            return RedirectToAction("Get");
        }
    }
}
