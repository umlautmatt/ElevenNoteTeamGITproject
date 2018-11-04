using System.Collections.Generic;
using System.Web.Mvc;
using ElevenNote.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevenNote.Web.Tests.Controllers.NoteControllerTests
{
    [TestClass]
    public class IndexGet : NoteControllerTestsBase
    {
        private List<NoteListItem> _expectedNotes;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _expectedNotes = new List<NoteListItem>
            {
                new NoteListItem { NoteId = 1 },
                new NoteListItem { NoteId = 2 }
            };
            NoteService.GetNotesResult = _expectedNotes;
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
            var result = (ViewResult)Act();

            Assert.IsInstanceOfType(result.Model, typeof(List<NoteListItem>));
            Assert.AreEqual(_expectedNotes, result.Model);
        }
    }
}