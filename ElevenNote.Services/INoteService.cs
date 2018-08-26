using ElevenNote.Models;
using System.Collections.Generic;

namespace ElevenNote.Services
{
    public interface INoteService
    {
        bool CreateNote(NoteCreate model);
        IEnumerable<NoteListItem> GetNotes();
        NoteDetail GetNoteById(int noteId);
        bool UpdateNote(NoteEdit model);
        bool DeleteNote(int noteId);
    }
}