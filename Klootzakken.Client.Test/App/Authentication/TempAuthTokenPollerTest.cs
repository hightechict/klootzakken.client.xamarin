using Klootzakken.Client.Domain;
using Moq;
using Xunit;
using FluentAssertions;
using Klootzakken.Client.App.Authentication;
using System;
using System.Threading.Tasks;

namespace Klootzakken.Client.Test.App.Authentication
{
    public class TempAuthTokenPollerTest
    {
        private const string _pinCode = "7788";
        private const string _tempAuthToken = "1234567890";

        private readonly PinAuthenticationException exepctedPinAuthenticationException = new PinAuthenticationException("404");

        [Fact]
        public void getTemporaryAuthTokenThrowsPinAuthenticationException_pollinForTemporaryAuthToken_throwsPinAuthException()
        {
            //ARRANGE
            var mockedAuthService = new Mock<IAuthenticationService>();
            mockedAuthService.Setup(auth => auth.GetTemporaryAuthTokenAsync(It.IsAny<string>())).Throws(exepctedPinAuthenticationException);

            var sut = new TempAuthenticationTokenPoller(mockedAuthService.Object);

            //ACT
            Func<Task> pollForTemporaryAuthTokenFunction = async () => await sut.poll(_pinCode, 1, 0);

            //ASSERT
            pollForTemporaryAuthTokenFunction.ShouldThrow<PinAuthenticationException>();
        }

        [Fact]
        public void getTemporaryAuthTokenReturnsTheToken_pollingForTemporaryAuthToken_returnTemporaryAuthToken()
        {
            //ARRANGE
            var mockedAuthenticationService = new Mock<IAuthenticationService>();
            mockedAuthenticationService.Setup(auth => auth.GetTemporaryAuthTokenAsync(It.IsAny<string>())).ReturnsAsync(_tempAuthToken);

            var sut = new TempAuthenticationTokenPoller(mockedAuthenticationService.Object);

            //ACT
            var tempAuthToken = sut.poll(_pinCode, 1, 0).Result;

            //ASSERT
            tempAuthToken.Should().BeAssignableTo<string>().And
                .BeSameAs(_tempAuthToken);
        }

        [Fact]
        public void getTemporaryAuthTokenFirstThrowServiceExceptionThenReturnsTheTempToken_pollingForTempAuthToken_returnTempAuthToken()
        {
            //ARRANGE
            var mockedAuthenticationService = new Mock<IAuthenticationService>();
            mockedAuthenticationService.SetupSequence(auth => auth.GetTemporaryAuthTokenAsync(It.IsAny<string>()))
                .Throws(exepctedPinAuthenticationException)
                .ReturnsAsync(_tempAuthToken);

            var sut = new TempAuthenticationTokenPoller(mockedAuthenticationService.Object);

            //ACT
            var tempAuthToken = sut.poll(_pinCode, 2, 0).Result;

            //ASSERT
            tempAuthToken.Should().BeAssignableTo<string>().And
                  .BeSameAs(_tempAuthToken);
        }

        [Fact]
        public void getTemporaryAuthTokenThrowsExceptionsTillReachMaxNumberOfAttempts_pollingForTempAuthToken_returnsPinAuthenticationException()
        {
            //ARRANGE
            var mockedAuthenticationService = new Mock<IAuthenticationService>();
            mockedAuthenticationService.SetupSequence(auth => auth.GetTemporaryAuthTokenAsync(It.IsAny<string>()))
                .Throws(exepctedPinAuthenticationException)
                .Throws(exepctedPinAuthenticationException)
                .Throws(exepctedPinAuthenticationException);

            var sut = new TempAuthenticationTokenPoller(mockedAuthenticationService.Object);

            //ACT
            Func<Task> pollForTemporaryAuthTokenFunction = async () => await sut.poll(_pinCode, 3, 0);

            //ASSERT
            pollForTemporaryAuthTokenFunction.ShouldThrow<PinAuthenticationException>();
            //TODO: return another exception
        }

    }
}
