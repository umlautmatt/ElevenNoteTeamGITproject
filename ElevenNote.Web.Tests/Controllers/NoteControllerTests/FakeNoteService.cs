using ElevenNote.Models;
using ElevenNote.Services;
using System.Collections.Generic;

namespace ElevenNote.Web.Tests.Controllers.NoteControllerTests
{
    /// <summary>
    /// The FakeNoteService implements the <see cref="INoteService"/> along with some other useful testing behavior.
    /// </summary>
    public class FakeNoteService : INoteService
    {
        // Since the fake note service has extra properties, the regions help us organize our related public members.
        #region [CreateNote()]

        // Allowing yourself to configure the output of the fake note service lets you test different scenarios.
        public bool CreateNoteResult { private get; set; } = true; // We default this property to true for happy path, but we can change this to test failures.

        // Capturing parameters lets you interrogate them in your assertions.
        public NoteCreate CreateNoteParam { get; private set; }

        // Interface method implementation that doesn't hit any database, since this is all fake behavior for testing purposes.
        public bool CreateNote(NoteCreate model)
        {
            // Capture param
            CreateNoteParam = model;

            // Return configured output
            return CreateNoteResult;
        }

        #endregion [CreateNote()]

        #region [GetNotes()]

        public List<NoteListItem> GetNotesResult { private get; set; }

        public IEnumerable<NoteListItem> GetNotes()
        {
            return GetNotesResult;
        }

        #endregion [GetNotes()]

        #region [GetNoteById()]

        public int GetNoteByIdParam { get; private set; }

        public NoteDetail GetNoteByIdResult { private get; set; }

        public NoteDetail GetNoteById(int noteId)
        {
            GetNoteByIdParam = noteId;

            return GetNoteByIdResult;
        }

        #endregion [GetNoteById()]

        #region [UpdateNote()]

        public bool UpdateNoteResult { private get; set; } = true;

        public NoteEdit UpdateNoteParam { get; private set; }

        public bool UpdateNote(NoteEdit model)
        {
            UpdateNoteParam = model;

            return UpdateNoteResult;
        }

        #endregion [UpdateNote()]

        #region [DeleteNote()]

        public int DeleteNoteParam { get; private set; }

        public bool DeleteNote(int noteId)
        {
            DeleteNoteParam = noteId;

            return true;
        }

        #endregion [DeleteNote()]
    }
}