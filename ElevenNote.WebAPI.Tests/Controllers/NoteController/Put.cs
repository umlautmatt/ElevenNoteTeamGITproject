using System.Web.Http;
using System.Web.Http.Results;
using ElevenNote.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElevenNote.WebAPI.Tests.Controllers.NoteController
{
    [TestClass]
    public class Put : NoteControllerTestsBase
    {
        private bool _updateNoteSuccess = true;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            NoteService
                .Setup(x => x.UpdateNote(It.IsAny<NoteEdit>()))
                .Returns(() => _updateNoteSuccess);
        }

        private IHttpActionResult Act()
        {
            return Controller.Put(new NoteEdit());
        }

        [TestMethod]
        public void ReturnsOkResult()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void CallsUpdateNote()
        {
            Act();

            NoteService.Verify(
                x => x.UpdateNote(It.IsAny<NoteEdit>()),
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
            _updateNoteSuccess = false;

            var result = Act();

            Assert.IsInstanceOfType(result, typeof(InternalServerErrorResult));
        }
    }
}