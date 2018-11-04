using ElevenNote.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Mvc;

namespace ElevenNote.Web.Tests.Controllers.NoteControllerTests
{
    [TestClass]
    public class EditPost : NoteControllerTestsBase
    {
        private const int Id = 1;
        private NoteEdit _noteEdit;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _noteEdit = new NoteEdit
            {
                NoteId = Id,
                Content = "content",
                Title = "title"
            };
        }

        private ActionResult Act(int id = Id)
        {
            return Controller.Edit(id, _noteEdit);
        }

        [TestMethod]
        public void ReturnsRedirectToIndex()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual("Index", ((RedirectToRouteResult)result).RouteValues["action"]);
        }

        [TestMethod]
        public void CallsNoteServiceUpdateNoteWithExpectedValue()
        {
            Act();

            Assert.AreEqual(_noteEdit, NoteService.UpdateNoteParam);
        }

        [TestMethod]
        public void SetsTempDataSaveResult()
        {
            Act();

            StringAssert.Contains(
                Controller.TempData["SaveResult"].ToString(),
                "Your note was updated");
        }

        [TestMethod]
        public void ReturnsViewResultWithOriginalModel_GivenInvalidModelState()
        {
            Controller.ModelState.AddModelError("", "error");

            var result = Act();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(_noteEdit, ((ViewResult)result).Model);
        }

        [TestMethod]
        public void ReturnsViewResultWithOriginalModel_GivenNoteIdMismatch()
        {
            var result = Act(55);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(_noteEdit, ((ViewResult)result).Model);
        }

        [TestMethod]
        public void SetsModelStateError_GivenNoteIdMismatch()
        {
            Act(55);

            StringAssert.Contains(
                Controller.ModelState[""].Errors.First().ErrorMessage,
                "Id Mismatch");
        }

        [TestMethod]
        public void ReturnsViewResultWithOriginalModel_GivenUpdateNoteFails()
        {
            NoteService.UpdateNoteResult = false;

            var result = Act();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(_noteEdit, ((ViewResult)result).Model);
        }

        [TestMethod]
        public void SetsModelStateError_GivenUpdateNoteFails()
        {
            NoteService.UpdateNoteResult = false;

            Act();

            StringAssert.Contains(
                Controller.ModelState[""].Errors.First().ErrorMessage,
                "Your note could not be updated");
        }
    }
}
