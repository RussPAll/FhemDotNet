using Nancy.ViewEngines;
using Moq;
using Nancy.Responses;

namespace FhemDotNet.UI.Tests
{
    public abstract class NancyModuleTestBase
    {
        protected Mock<IViewFactory> ViewFactoryMock;

        public void SetUp()
        {
            ViewFactoryMock = new Mock<IViewFactory>();
            ViewFactoryMock
                .Setup(x => x.RenderView(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<ViewLocationContext>()))
                .Returns(new HtmlResponse());
        }
    }
}
