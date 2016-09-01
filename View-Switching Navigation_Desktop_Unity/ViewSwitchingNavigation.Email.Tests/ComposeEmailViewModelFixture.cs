

using System;
using Prism.Common;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewSwitchingNavigation.Email.Model;
using ViewSwitchingNavigation.Email.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ViewSwitchingNavigation.Email.Tests
{
    [TestClass]
    public class ComposeEmailViewModelFixture
    {
        [TestMethod]
        public async void WhenSendMessageCommandIsExecuted_ThenSendsMessageThroughService()
        {
            var emailServiceMock = new Mock<IEmailService>();

            var viewModel = new ComposeEmailViewModel(emailServiceMock.Object);
            ((INavigationAware)viewModel).OnNavigatedTo(new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("", UriKind.Relative)));

            viewModel.SendEmailCommand.Execute(null);
            await Task.Delay(500);
            Assert.AreEqual("Sending", viewModel.SendState);

            emailServiceMock.Verify(svc => svc.SendEmailDocumentAsync(viewModel.EmailDocument));
        }

        [TestMethod]
        public void WhenNavigatedToWithAReplyToQueryParameter_ThenRepliesToTheAppropriateMessage()
        {
            var replyToEmail = new EmailDocument { From = "somebody@contoso.com", To = "", Subject = "", Text = "" };

            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock
                .Setup(svc => svc.GetEmailDocument(replyToEmail.Id))
                .Returns(replyToEmail);

            var viewModel = new ComposeEmailViewModel(emailServiceMock.Object);

            var navService = new Mock<IRegionNavigationService>();
            var region = new Mock<IRegion>();
            region.SetupGet(x => x.Context).Returns(null);
            navService.SetupGet(x => x.Region).Returns(region.Object);

            ((INavigationAware)viewModel).OnNavigatedTo(new NavigationContext(navService.Object, new Uri("location?ReplyTo=" + replyToEmail.Id.ToString("N"), UriKind.Relative)));

            Assert.AreEqual("somebody@contoso.com", viewModel.EmailDocument.To);
        }

        [TestMethod]
        public void WhenNavigatedToWithAToQueryParameter_ThenInitializesToField()
        {
            var emailServiceMock = new Mock<IEmailService>();

            var viewModel = new ComposeEmailViewModel(emailServiceMock.Object);

            var navService = new Mock<IRegionNavigationService>();
            var region = new Mock<IRegion>();
            region.SetupGet(x => x.Context).Returns(null);
            navService.SetupGet(x => x.Region).Returns(region.Object);

            ((INavigationAware)viewModel).OnNavigatedTo(new NavigationContext(navService.Object, new Uri("location?To=somebody@contoso.com", UriKind.Relative)));

            Assert.AreEqual("somebody@contoso.com", viewModel.EmailDocument.To);
        }

        [TestMethod]
        public void WhenFinishedSendingMessage_ThenNavigatesBack()
        {
            var sendEmailResultMock = new Mock<IAsyncResult>();

            var emailServiceMock = new Mock<IEmailService>();
            emailServiceMock
                .Setup(svc => svc.SendEmailDocumentAsync(It.IsAny<EmailDocument>()))
                .Returns(Task.FromResult<object>(null));

            var journalMock = new Mock<IRegionNavigationJournal>();
            journalMock.Setup(j => j.GoBack()).Verifiable();

            var navigationServiceMock = new Mock<IRegionNavigationService>();
            navigationServiceMock.SetupGet(svc => svc.Journal).Returns(journalMock.Object);

            var viewModel = new ComposeEmailViewModel(emailServiceMock.Object);
            ((INavigationAware)viewModel).OnNavigatedTo(new NavigationContext(navigationServiceMock.Object, new Uri("", UriKind.Relative)));

            viewModel.SendEmailCommand.Execute(null);

            // The action is performed asynchronously in the view model, so we need to wait until it's completed.
            Thread.Sleep(500);

            Assert.AreEqual("Sent", viewModel.SendState);
            journalMock.VerifyAll();
        }

        [TestMethod]
        public void WhenRequestedForVetoOnNavigationBeforeSubmitting_ThenRaisesInteractionRequest()
        {
            var emailServiceMock = new Mock<IEmailService>();

            var viewModel = new ComposeEmailViewModel(emailServiceMock.Object);

            var navService = new Mock<IRegionNavigationService>();
            var region = new Mock<IRegion>();
            region.SetupGet(x => x.Context).Returns(null);
            navService.SetupGet(x => x.Region).Returns(region.Object);

            ((INavigationAware)viewModel).OnNavigatedTo(new NavigationContext(navService.Object, new Uri("location?To=somebody@contoso.com", UriKind.Relative)));

            InteractionRequestedEventArgs args = null;
            viewModel.ConfirmExitInteractionRequest.Raised += (s, e) => args = e;

            bool? callbackResult = null;
            ((IConfirmNavigationRequest)viewModel).ConfirmNavigationRequest(
                new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("some uri", UriKind.Relative)),
                b => callbackResult = b);

            Assert.IsNotNull(args);
            Assert.IsNull(callbackResult);
        }

        [TestMethod]
        public void WhenRequestedForVetoOnNavigationAfterSubmitting_ThenDoesNotRaiseInteractionRequest()
        {
            var emailServiceMock = new Mock<IEmailService>();

            var viewModel = new ComposeEmailViewModel(emailServiceMock.Object);

            var navService = new Mock<IRegionNavigationService>();
            var region = new Mock<IRegion>();
            region.SetupGet(x => x.Context).Returns(null);
            navService.SetupGet(x => x.Region).Returns(region.Object);

            ((INavigationAware)viewModel).OnNavigatedTo(new NavigationContext(navService.Object, new Uri("location?To=somebody@contoso.com", UriKind.Relative)));

            InteractionRequestedEventArgs args = null;
            viewModel.ConfirmExitInteractionRequest.Raised += (s, e) => args = e;

            viewModel.SendEmailCommand.Execute(null);

            bool? callbackResult = null;
            ((IConfirmNavigationRequest)viewModel).ConfirmNavigationRequest(
                new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("some uri", UriKind.Relative)),
                b => callbackResult = b);

            Assert.IsNull(args);
            Assert.IsTrue(callbackResult.Value);
        }

        [TestMethod]
        public void WhenRejectingConfirmationToNavigateAway_ThenInvokesRequestCallbackWithFalse()
        {
            var emailServiceMock = new Mock<IEmailService>();

            var viewModel = new ComposeEmailViewModel(emailServiceMock.Object);

            var navService = new Mock<IRegionNavigationService>();
            var region = new Mock<IRegion>();
            region.SetupGet(x => x.Context).Returns(null);
            navService.SetupGet(x => x.Region).Returns(region.Object);

            ((INavigationAware)viewModel).OnNavigatedTo(new NavigationContext(navService.Object, new Uri("location?To=somebody@contoso.com", UriKind.Relative)));

            InteractionRequestedEventArgs args = null;
            viewModel.ConfirmExitInteractionRequest.Raised += (s, e) => args = e;

            bool? callbackResult = null;
            ((IConfirmNavigationRequest)viewModel).ConfirmNavigationRequest(
                new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("some uri", UriKind.Relative)),
                b => callbackResult = b);

            var confirmation = args.Context as Confirmation;

            confirmation.Confirmed = false;

            args.Callback();

            Assert.IsFalse(callbackResult.Value);
        }

        [TestMethod]
        public void WhenAcceptingConfirmationToNavigateAway_ThenInvokesRequestCallbackWithTrue()
        {
            var emailServiceMock = new Mock<IEmailService>();

            var viewModel = new ComposeEmailViewModel(emailServiceMock.Object);

            var navService = new Mock<IRegionNavigationService>();
            var region = new Mock<IRegion>();
            region.SetupGet(x => x.Context).Returns(null);
            navService.SetupGet(x => x.Region).Returns(region.Object);

            ((INavigationAware)viewModel).OnNavigatedTo(new NavigationContext(navService.Object, new Uri("location?To=somebody@contoso.com", UriKind.Relative)));

            InteractionRequestedEventArgs args = null;
            viewModel.ConfirmExitInteractionRequest.Raised += (s, e) => args = e;

            bool? callbackResult = null;
            ((IConfirmNavigationRequest)viewModel).ConfirmNavigationRequest(
                new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("some uri", UriKind.Relative)),
                b => callbackResult = b);

            var confirmation = args.Context as Confirmation;

            confirmation.Confirmed = true;

            args.Callback();

            Assert.IsTrue(callbackResult.Value);
        }

        [TestMethod]
        public void WhenCancelling_ThenNavigatesBack()
        {
            var emailServiceMock = new Mock<IEmailService>();

            var viewModel = new ComposeEmailViewModel(emailServiceMock.Object);

            var journalMock = new Mock<IRegionNavigationJournal>();

            var navigationServiceMock = new Mock<IRegionNavigationService>();
            navigationServiceMock.SetupGet(svc => svc.Journal).Returns(journalMock.Object);

            ((INavigationAware)viewModel).OnNavigatedTo(
                new NavigationContext(
                    navigationServiceMock.Object,
                    new Uri("location", UriKind.Relative)));

            viewModel.CancelEmailCommand.Execute(null);

            journalMock.Verify(j => j.GoBack());
        }
    }
}
