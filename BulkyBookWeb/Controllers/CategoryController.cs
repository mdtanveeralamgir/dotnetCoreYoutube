using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _context.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("customError", "The Name and DisplayOrder need to be different");
                //with key name the msg will be displayed under name input. 
                //It will not replace the default error msg of name input
                //ModelState.AddModelError("name", "The Name and DisplayOrder need to be different");
            }
            if(ModelState.IsValid)
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
                //return RedirectToAction("Index", "differentControllerName");
            }

            return View(obj);

        }

        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }

            var CategoryToBeEdited = _context.Categories.SingleOrDefault(x=>x.Id == Id); 
            if(CategoryToBeEdited == null)
            {
                return NotFound();
            }

            return View(CategoryToBeEdited);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("customError", "The Name and DisplayOrder need to be different");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);

        }   
        
        public IActionResult Delete(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }

            var CategoryToBeEdited = _context.Categories.SingleOrDefault(x=>x.Id == Id); 
            if(CategoryToBeEdited == null)
            {
                return NotFound();
            }

            return View(CategoryToBeEdited);
        }

        [HttpPost]
        //Explicitly telling the action name to use in form as action
        //[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.Categories.Find(id);

            if(obj == null)
            {
                return NotFound(); 
            }
       
            _context.Categories.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");

            return View(obj);

        }
    }
}
