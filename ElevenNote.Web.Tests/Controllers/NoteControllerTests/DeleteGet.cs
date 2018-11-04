using ElevenNote.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace ElevenNote.Web.Tests.Controllers.NoteControllerTests
{
    [TestClass]
    public class DeleteGet : NoteControllerTestsBase
    {
        private const int Id = 1;
        private NoteDetail _noteDetail;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _noteDetail = new NoteDetail
            {
                NoteId = Id
            };
            NoteService.GetNoteByIdResult = _noteDetail;
        }

        private ActionResult Act()
        {
            return Controller.Delete(Id);
        }

        [TestMethod]
        public void ReturnsViewResultWithExpectedModel()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(_noteDetail, ((ViewResult)result).Model);
        }

        [TestMethod]
        public void CallsNoteServiceGetNoteByIdWithExpectedId()
        {
            Act();

            Assert.AreEqual(Id, NoteService.GetNoteByIdParam);
        }
    }
}
