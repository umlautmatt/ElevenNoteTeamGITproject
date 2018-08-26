using System.Collections.Generic;
using System.Web.Mvc;
using ElevenNote.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevenNote.Web.Tests.Controllers.NoteControllerTests
{
    [TestClass]
    public class IndexGet : NoteControllerTestsBase
    {
        private readonly List<NoteListItem> _expectedNotes = new List<NoteListItem>();
            
        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();
            
            NoteService.GetNotesResult = _expectedNotes;
            _expectedNotes.Add(new NoteListItem { NoteId = 1 });
            _expectedNotes.Add(new NoteListItem { NoteId = 2 });
        }

        private ActionResult Act()
        {
            return Controller.Index();
        }

        [TestMethod]
        public void ReturnsViewResult()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ReturnsExpectedModel()
        {
            var result = (ViewResult) Act();

            Assert.IsInstanceOfType(result.Model, typeof(List<NoteListItem>));
            Assert.AreEqual(_expectedNotes, result.Model);
        }
    }
}