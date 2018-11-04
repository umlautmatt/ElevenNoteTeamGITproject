using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using ElevenNote.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElevenNote.WebAPI.Tests.Controllers.NoteController
{
    [TestClass]
    public class GetAll : NoteControllerTestsBase
    {
        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            NoteService
                .Setup(x => x.GetNotes())
                .Returns(new List<NoteListItem>
                {
                    new NoteListItem(),
                    new NoteListItem(),
                    new NoteListItem()
                });
        }

        private IHttpActionResult Act()
        {
            return Controller.GetAll();
        }

        [TestMethod]
        public void ReturnsOkNegotiatedContentResult()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<IEnumerable<NoteListItem>>));
        }

        [TestMethod]
        public void CallsGetNotes()
        {
            Act();

            NoteService.Verify(
                x => x.GetNotes(),
                Times.Once);
        }
    }
}