using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElevenNote.WebAPI.Tests.Controllers.NoteController
{
    [TestClass]
    public class Delete : NoteControllerTestsBase
    {
        private const int ExpectedNoteId = 10;
        private bool _updateNoteSuccess = true;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            NoteService
                .Setup(x => x.DeleteNote(It.IsAny<int>()))
                .Returns(() => _updateNoteSuccess);
        }

        private IHttpActionResult Act()
        {
            return Controller.Delete(ExpectedNoteId);
        }

        [TestMethod]
        public void ReturnsOkResult()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void CallsDeleteNote()
        {
            Act();

            NoteService.Verify(
                x => x.DeleteNote(ExpectedNoteId),
                Times.Once);
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