using ElevenNote.Services;
using ElevenNote.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ElevenNote.Web.Tests.Controllers.NoteControllerTests
{
    /// <summary>
    /// The <see cref="NoteController"/> is incredibly testable when using dependency injection of abstractions.
    /// We can use inheritance to share common setup code among classes that test individual actions.
    /// </summary>
    [TestClass]
    public abstract class NoteControllerTestsBase
    {
        // The class under test.
        protected NoteController Controller;

        // The dependency, but we use a fake one to manipulate its behavior and control controller action outcomes for testing.
        protected FakeNoteService NoteService;

        // This is virtual so inheritors can add additional Arrange code.
        [TestInitialize]
        public virtual void Arrange()
        {
            NoteService = new FakeNoteService();

            // When unit testing, we can test the controller logic in isolation by manually injecting a fake note service rather than use a real one.
            Controller = new NoteController(
                new Lazy<INoteService>(() => NoteService));
        }
    }
}