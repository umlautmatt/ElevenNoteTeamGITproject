using System.Collections.Generic;
using ElevenNote.Models;
using ElevenNote.Services;

namespace ElevenNote.Web.Tests.Controllers.NoteControllerTests
{
    public class FakeNoteService : INoteService
    {
        public bool CreateNote(NoteCreate model)
        {
            return true;
        }

        #region [GetNotes()]

        public List<NoteListItem> GetNotesResult { get; set; }

        public IEnumerable<NoteListItem> GetNotes()
        {
            return GetNotesResult;
        }

        #endregion [GetNotes()]

        public NoteDetail GetNoteById(int noteId)
        {
            return new NoteDetail
            {
                NoteId = noteId
            };
        }

        public bool UpdateNote(NoteEdit model)
        {
            return true;
        }

        public bool DeleteNote(int noteId)
        {
            return true;
        }
    }
}