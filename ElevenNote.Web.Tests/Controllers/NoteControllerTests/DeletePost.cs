using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace ElevenNote.Web.Tests.Controllers.NoteControllerTests
{
    [TestClass]
    public class DeletePost : NoteControllerTestsBase
    {
        private const int Id = 1;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();
        }

        private ActionResult Act()
        {
            return Controller.DeletePost(Id);
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResult()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual("Index", ((RedirectToRouteResult)result).RouteValues["action"]);
        }

        [TestMethod]
        public void CallsNoteServiceDeleteNoteWithExpectedId()
        {
            Act();

            Assert.AreEqual(Id, NoteService.DeleteNoteParam);
        }

        [TestMethod]
        public void SetsTempDataSaveResult()
        {
            Act();

            StringAssert.Contains(Controller.TempData["SaveResult"].ToString(), "Your note was deleted!");
        }
    }
}
