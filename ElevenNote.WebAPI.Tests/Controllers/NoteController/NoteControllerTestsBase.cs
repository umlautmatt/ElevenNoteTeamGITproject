using System;
using ElevenNote.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElevenNote.WebAPI.Tests.Controllers.NoteController
{
    [TestClass]
    public abstract class NoteControllerTestsBase
    {
        protected WebAPI.Controllers.NoteController Controller;

        protected Mock<INoteService> NoteService;

        [TestInitialize]
        public virtual void Arrange()
        {
            NoteService = new Mock<INoteService>();

            Controller = new WebAPI.Controllers.NoteController(
                new Lazy<INoteService>(() => NoteService.Object));
        }
    }
}