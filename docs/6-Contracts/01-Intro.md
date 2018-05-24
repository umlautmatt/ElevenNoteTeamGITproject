# CONTRACTS INTRO
--- 
In this module we'll introduce establishing contracts in a .NET application.

<hr />

### Interface
When creating interfaces we step back and take a high level look at what we'll need for our methods. Let's think this through.

###Step 1: What methods will we need?
Let's just list out the methods that we'd need:
CreateNote
ReadNote
UpdateNote
DeleteNote
GetNotes

### Step 2: What will those methods need to return?
bool CreateNote(); -- bool because it will return true when we save.
IEnumerable<NoteListItem> GetNotes(); -- Our List View shows a list, so we need to return an IEnumerable from the DB.
NoteDetail GetNoteById(); -- We will need to create a detail model still, but we want to get details for a note.
bool UpdateNote(); - Same as CreateNote(); We want to know if it has updated. 
bool DeleteNote(); - Same as Create and Update.


### Step 3: What will those methods be taking in as parameters?
bool CreateNote(NoteCreate model); - We'll be getting this object from the Controller.
GetNotes() - No params. The method implementation will query the db.
NoteDetail GetNoteById(int id); -- We will want to get an id from the view. When a user clicks on the details, this will be passed.


