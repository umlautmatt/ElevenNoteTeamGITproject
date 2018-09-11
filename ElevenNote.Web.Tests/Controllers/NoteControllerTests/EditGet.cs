using ElevenNote.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace ElevenNote.Web.Tests.Controllers.NoteControllerTests
{
    [TestClass]
    public class EditGet : NoteControllerTestsBase
    {
        private const int Id = 1;
        private NoteDetail _noteDetail;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _noteDetail = new NoteDetail
            {
                NoteId = Id,
                Content = "content",
                Title = "title"
            };
            NoteService.GetNoteByIdResult = _noteDetail;
        }

        private ActionResult Act()
        {
            return Controller.Edit(Id);
        }

        [TestMethod]
        public void ReturnsViewResultWithExpectedModelValues()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var model = (NoteEdit)((ViewResult)result).Model;
            Assert.AreEqual(_noteDetail.NoteId, model.NoteId);
            Assert.AreEqual(_noteDetail.Content, model.Content);
            Assert.AreEqual(_noteDetail.Title, model.Title);
        }

        [TestMethod]
        public void CallsNoteServiceGetNoteByIdWithExpectedId()
        {
            Act();

            Assert.AreEqual(Id, NoteService.GetNoteByIdParam);
        }
    }
}
