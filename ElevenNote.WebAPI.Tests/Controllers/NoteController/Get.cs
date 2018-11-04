using System.Web.Http;
using System.Web.Http.Results;
using ElevenNote.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElevenNote.WebAPI.Tests.Controllers.NoteController
{
    [TestClass]
    public class Get : NoteControllerTestsBase
    {
        private const int ExpectedNoteId = 10;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            NoteService
                .Setup(x => x.GetNoteById(It.IsAny<int>()))
                .Returns(new NoteDetail());
        }

        private IHttpActionResult Act()
        {
            return Controller.Get(ExpectedNoteId);
        }

        [TestMethod]
        public void ReturnsOkNegotiatedContentResult()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<NoteDetail>));
        }

        [TestMethod]
        public void CallsGetNoteById()
        {
            Act();

            NoteService.Verify(
                x => x.GetNoteById(ExpectedNoteId),
                Times.Once);
        }
    }
}