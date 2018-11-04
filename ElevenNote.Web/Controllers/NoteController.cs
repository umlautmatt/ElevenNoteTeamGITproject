using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace ElevenNote.Web.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        // not every action needs a note service, so Lazy allows us to defer instantiation until we actually need one.
        private readonly Lazy<INoteService> _noteService;

        /// <summary>
        /// Production constructor. Creates a real NoteService.
        /// </summary>
        public NoteController()
        {
            _noteService = new Lazy<INoteService>(CreateNoteService);
        }

        /// <summary>
        /// Testing constructor. Lets us inject a mocked INoteService.
        /// </summary>
        public NoteController(Lazy<INoteService> noteService)
        {
            _noteService = noteService;
        }

        public ActionResult Index()
        {
            var model = _noteService.Value.GetNotes();

            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (_noteService.Value.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var model = _noteService.Value.GetNoteById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var detail = _noteService.Value.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            if (_noteService.Value.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var model = _noteService.Value.GetNoteById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            // TODO: Handle failure
            _noteService.Value.DeleteNote(id);

            TempData["SaveResult"] = "Your note was deleted!";

            return RedirectToAction("Index");
        }

        private INoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
    }
}