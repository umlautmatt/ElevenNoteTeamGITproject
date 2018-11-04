using System.Web.Http;
using System.Web.Http.Results;
using ElevenNote.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElevenNote.WebAPI.Tests.Controllers.NoteController
{
    [TestClass]
    public class Post : NoteControllerTestsBase
    {
        private bool _createNoteSuccess = true;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            NoteService
                .Setup(x => x.CreateNote(It.IsAny<NoteCreate>()))
                .Returns(() => _createNoteSuccess);
        }

        private IHttpActionResult Act()
        {
            return Controller.Post(new NoteCreate());
        }

        [TestMethod]
        public void ReturnsOkResult()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void CallsCreateNote()
        {
            Act();

            NoteService.Verify(
                x => x.CreateNote(It.IsAny<NoteCreate>()),
                Times.Once);
        }

        [TestMethod]
        public void ReturnsInvalidModelStateResult_GivenInvalidModelState()
        {
            Controller.ModelState.AddModelError("", "some error");

            var result = Act();

            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void ReturnsInternalServerErrorResult_GivenCreateNoteFails()
        {
            _createNoteSuccess = false;

            var result = Act();

            Assert.IsInstanceOfType(result, typeof(InternalServerErrorResult));
        }
    }
}