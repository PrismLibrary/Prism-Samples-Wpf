

using System;
using Prism.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewSwitchingNavigation.Email.Model;
using ViewSwitchingNavigation.Email.ViewModels;

namespace ViewSwitchingNavigation.Email.Tests
{
    [TestClass]
    public class EmailViewModelFixture
    {
        [TestMethod]
        public void WhenNavigatedTo_ThenRequestsEmailFromService()
        {
            var email = new EmailDocument();

            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock
                .Setup(svc => svc.GetEmailDocument(email.Id))
               .Returns(email)
               .Verifiable();

            var viewModel = new EmailViewModel(emailServiceMock.Object);

            var notified = false;
            viewModel.PropertyChanged += (s, o) => notified = o.PropertyName == "Email";

            NavigationContext context = new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("location", UriKind.Relative));
            context.Parameters.Add("EmailId", email.Id);

            ((INavigationAware)viewModel).OnNavigatedTo(context);

            Assert.IsTrue(notified);
            emailServiceMock.VerifyAll();
        }

        [TestMethod]
        public void WhenAskedCanNavigateForSameQuery_ThenReturnsTrue()
        {
            var email = new EmailDocument();

            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock
                .Setup(svc => svc.GetEmailDocument(email.Id))
               .Returns(email)
               .Verifiable();

            var viewModel = new EmailViewModel(emailServiceMock.Object);

            NavigationContext context = new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("location", UriKind.Relative));
            context.Parameters.Add("EmailId", email.Id);

            ((INavigationAware)viewModel).OnNavigatedTo(context);

            bool canNavigate =
                ((INavigationAware)viewModel).IsNavigationTarget(context);

            Assert.IsTrue(canNavigate);
        }

        [TestMethod]
        public void WhenAskedCanNavigateForDifferentQuery_ThenReturnsFalse()
        {
            var email = new EmailDocument();

            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock
                .Setup(svc => svc.GetEmailDocument(email.Id))
               .Returns(email)
               .Verifiable();

            var viewModel = new EmailViewModel(emailServiceMock.Object);

            NavigationContext context = new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("location", UriKind.Relative));
            context.Parameters.Add("EmailId", email.Id);
            
            ((INavigationAware)viewModel).OnNavigatedTo(context);

            context = new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("location", UriKind.Relative));
            context.Parameters.Add("EmailId", new Guid());

            bool canNavigate =
                ((INavigationAware)viewModel).IsNavigationTarget(context);

            Assert.IsFalse(canNavigate);
        }

        [TestMethod]
        public void WhenGoingBack_ThenNavigatesBack()
        {
            var emailServiceMock = new Mock<IEmailService>();

            var viewModel = new EmailViewModel(emailServiceMock.Object);

            var journalMock = new Mock<IRegionNavigationJournal>();

            var navigationServiceMock = new Mock<IRegionNavigationService>();
            navigationServiceMock.SetupGet(svc => svc.Journal).Returns(journalMock.Object);

            NavigationContext context = new NavigationContext(navigationServiceMock.Object, new Uri("location", UriKind.Relative));
            context.Parameters.Add("EmailId", Guid.NewGuid());

            ((INavigationAware)viewModel).OnNavigatedTo(context);

            viewModel.GoBackCommand.Execute(null);

            journalMock.Verify(j => j.GoBack());
        }
    }
}
