using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    [Authorize]
    public class NoteController : ApiController
    {
        // not every action path needs a note service, so Lazy allows us to defer instantiation until we actually need one.
        private readonly Lazy<INoteService> _noteService;

        /// <summary>
        /// Production constructor. Creates a real NoteService.
        /// </summary>
        public NoteController()
        {
            _noteService = new Lazy<INoteService>(() =>
                new NoteService(Guid.Parse(User.Identity.GetUserId())));
        }

        /// <summary>
        /// Testing constructor. Lets us inject a mocked INoteService.
        /// </summary>
        public NoteController(Lazy<INoteService> noteService)
        {
            _noteService = noteService;
        }

        public IHttpActionResult GetAll()
        {
            var notes = _noteService.Value.GetNotes();
            return Ok(notes);
        }

        public IHttpActionResult Get(int id)
        {
            var note = _noteService.Value.GetNoteById(id);
            return Ok(note);
        }

        public IHttpActionResult Post(NoteCreate note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_noteService.Value.CreateNote(note))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Put(NoteEdit note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_noteService.Value.UpdateNote(note))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (!_noteService.Value.DeleteNote(id))
                return InternalServerError();

            return Ok();
        }
    }
}
