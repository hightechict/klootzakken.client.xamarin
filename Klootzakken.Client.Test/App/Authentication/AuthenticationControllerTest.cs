using Klootzakken.Client.App.Authentication;
using Klootzakken.Client.Domain;
using Moq;
using Xunit;
using FluentAssertions;
using System.Web;
using System;
using System.Threading.Tasks;

namespace Klootzakken.Client.Test.App.Authentication
{
    public class AuthenticationControllerTest
    {
        private const string _pinCode = "9129";
        private const string _expectedAuthToken = "temporaryAuthToken123456789";

        private readonly PinAuthenticationException exepctedPinAuthenticationException = new PinAuthenticationException("404");

        [Fact]
        public void authenticationController_getPinCode_returnsPinCode()
        {
            //ARRANGE
            var mockedAuthenticationService = new Mock<IAuthenticationService>();
            mockedAuthenticationService.Setup(auth => auth.GetPinAsync()).ReturnsAsync(_pinCode);

            var sut = new AuthenticationController(mockedAuthenticationService.Object);

            //ACT
            var pinCode = sut.GetPinCodeAsync().Result;

            //ASSERT
            pinCode.Should().BeAssignableTo<string>().And
                .BeSameAs(_pinCode);
        }
        //TODO: write test for the situatin when no pin code can be acquired and have rensponse error(code)

        [Fact]
        public void getTemporaryAuthTokenThrowsPinAuthenticationException_pollinForTemporaryAuthToken_throwsPinAuthException()
        {
            //ARRANGE
            var mockedAuthenticationService = new Mock<IAuthenticationService>();
            mockedAuthenticationService.Setup(auth => auth.GetTemporaryAuthTokenAsync(It.IsAny<string>())).Throws(exepctedPinAuthenticationException);

            var sut = new AuthenticationController(mockedAuthenticationService.Object);

            //ACT
            Func<Task> pollForTemporaryAuthTokenFunction = async () => await sut.pollingForTemporaryAuthToken(_pinCode, 1, 0);

            //ASSERT
            pollForTemporaryAuthTokenFunction.ShouldThrow<PinAuthenticationException>();
        }

        [Fact]
        public void getTemporaryAuthTokenReturnsTheToken_pollingForTemporaryAuthToken_returnTemporaryAuthToken()
        {
            //ARRANGE
            var mockedAuthenticationService = new Mock<IAuthenticationService>();
            mockedAuthenticationService.Setup(auth => auth.GetTemporaryAuthTokenAsync(It.IsAny<string>())).ReturnsAsync(_expectedAuthToken);

            var sut = new AuthenticationController(mockedAuthenticationService.Object);

            //ACT
            var tempAuthToken = sut.pollingForTemporaryAuthToken(_pinCode, 1, 0).Result;

            //ASSERT
            tempAuthToken.Should().BeAssignableTo<string>().And
                .BeSameAs(_expectedAuthToken);
        }

        [Fact]
        public void getTemporaryAuthTokenFirstThrowServiceExceptionThenReturnsTheTempToken_pollingForTempAuthToken_returnTempAuthToken()
        {
            //ARRANGE
            var mockedAuthenticationService = new Mock<IAuthenticationService>();
            mockedAuthenticationService.SetupSequence(auth => auth.GetTemporaryAuthTokenAsync(It.IsAny<string>()))
                .Throws(exepctedPinAuthenticationException)
                .ReturnsAsync(_expectedAuthToken);

            var sut = new AuthenticationController(mockedAuthenticationService.Object);

            //ACT
            var tempAuthToken = sut.pollingForTemporaryAuthToken(_pinCode, 2, 0).Result;

            //ASSERT
            tempAuthToken.Should().BeAssignableTo<string>().And
                  .BeSameAs(_expectedAuthToken);
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

            var sut = new AuthenticationController(mockedAuthenticationService.Object);

            //ACT
            Func<Task> pollForTemporaryAuthTokenFunction = async () => await sut.pollingForTemporaryAuthToken(_pinCode, 3, 0);

            //ASSERT
            pollForTemporaryAuthTokenFunction.ShouldThrow<PinAuthenticationException>();
            //TODO: return another exception
        }

        [Fact]
        public void authenticationController_getBearerAuthToken_returnsBearerAuthToken()
        {
            //ARRANGE
            var mockedAuthenticationService = new Mock<IAuthenticationService>();
            mockedAuthenticationService.SetupSequence(auth => auth.GetBearerTokenAsync(It.IsAny<string>())).ReturnsAsync(_expectedAuthToken);

            var sut = new AuthenticationController(mockedAuthenticationService.Object);

            //ACT
            var bearerToken = sut.GetBearerAuthToken(It.IsAny<string>()).Result;

            //ASSERT
            bearerToken.Should().BeAssignableTo<string>().And
                .BeSameAs(_expectedAuthToken);
        }
    }
}
