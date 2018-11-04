using System.Linq;
using System.Web.Mvc;
using ElevenNote.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevenNote.Web.Tests.Controllers.NoteControllerTests
{
    [TestClass]
    public class CreatePost : NoteControllerTestsBase
    {
        private NoteCreate _noteCreate;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _noteCreate = new NoteCreate
            {
                Content = "test content",
                Title = "test title"
            };
        }

        private ActionResult Act()
        {
            return Controller.Create(_noteCreate);
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResult()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual("Index", ((RedirectToRouteResult)result).RouteValues["action"]);
        }

        [TestMethod]
        public void ReturnsViewResultWithOriginalModel_GivenModelStateInvalid()
        {
            Controller.ModelState.AddModelError("test", "fail");

            var result = Act();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(_noteCreate, ((ViewResult)result).Model);
        }

        [TestMethod]
        public void CallsNoteServiceCreateNote()
        {
            Act();

            Assert.AreEqual(_noteCreate, NoteService.CreateNoteParam);
        }

        [TestMethod]
        public void SetsTempDataSaveResult()
        {
            Act();

            Assert.AreEqual("Your note was created", Controller.TempData["SaveResult"]);
        }

        [TestMethod]
        public void SetsModelStateError_GivenNoteCreateReturnsFalse()
        {
            NoteService.CreateNoteResult = false;

            Act();

            StringAssert.Contains(
                Controller.ModelState[""].Errors.First().ErrorMessage,
                "Your note could not be created");
        }

        [TestMethod]
        public void ReturnsViewResultWithOriginalModel_GivenNoteCreateReturnsFalse()
        {
            NoteService.CreateNoteResult = false;

            var result = Act();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(_noteCreate, ((ViewResult)result).Model);
        }
    }
}