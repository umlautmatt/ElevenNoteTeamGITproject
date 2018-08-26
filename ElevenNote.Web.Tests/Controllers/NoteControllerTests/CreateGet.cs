using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace ElevenNote.Web.Tests.Controllers.NoteControllerTests
{
    [TestClass]
    public class CreateGet : NoteControllerTestsBase
    {
        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();
        }

        private ActionResult Act()
        {
            return Controller.Create();
        }

        [TestMethod]
        public void ReturnsViewResult()
        {
            var result = Act();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
