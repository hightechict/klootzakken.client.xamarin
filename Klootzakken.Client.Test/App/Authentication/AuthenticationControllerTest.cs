using Klootzakken.Client.App.Authentication;
using Klootzakken.Client.Domain;
using Moq;
using Xunit;
using FluentAssertions;
using System.Web;
using System;
using System.Threading.Tasks;
using Klootzakken.Client.Utils;
using Klootzakken.Client.Utils.Interfaces;
using Klootzakken.Client.App.Interfaces;

namespace Klootzakken.Client.Test.App.Authentication
{
    public class AuthenticationControllerTest
    {
        private const string _pinCode = "9129";
        private const string _expectedTempAuthToken = "temporaryAuthToken123456789";
        private const string _bearerToken = "bearerToken123456789";
        private const string _brearerTokenKeyName = "bearer_token";

        [Fact]
        public void authenticationController_getPinCode_returnsPinCode()
        {
            //Arrange
            var mockedAuthenticationService = new Mock<IAuthenticationService>();
            var mockedSharedPreference = new Mock<ISharedPreferenceHandler>();
            var mockedTempAuthTokenPoller = new Mock<ITempAuthTokenPoller>();

            mockedAuthenticationService.Setup(auth => auth.GetPinAsync()).ReturnsAsync(_pinCode);

            var sut = new AuthenticationController(mockedAuthenticationService.Object, mockedTempAuthTokenPoller.Object, mockedSharedPreference.Object);

            //Act
            var pinCode = sut.GetPinCodeAsync().Result;

            //Assert
            pinCode.Should().BeAssignableTo<string>().And
                .BeSameAs(_pinCode);
        }
        //TODO: write test for the situatin when no pin code can be acquired and have rensponse error(code)

        [Fact]
        public void brearerTokenNotSavedAsSharedPreference_saveBearerToken_tokenIsSaved()
        {
            //Arrange
            var mockedSharedPreference = new Mock<ISharedPreferenceHandler>();
            var mockedAuthenticationService = new Mock<IAuthenticationService>();
            var mockedTempAuthTokenPoller = new Mock<ITempAuthTokenPoller>();

            mockedSharedPreference.Setup(pref => pref.GetPreference(_brearerTokenKeyName)).Returns<string>(null);
            mockedTempAuthTokenPoller.Setup(auth => auth.poll(_pinCode, It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_expectedTempAuthToken);
            mockedAuthenticationService.Setup(auth => auth.GetBearerTokenAsync(It.IsAny<string>())).ReturnsAsync(_bearerToken);

            var sut = new AuthenticationController(mockedAuthenticationService.Object, mockedTempAuthTokenPoller.Object, mockedSharedPreference.Object);

            //Act
            sut.SaveBearerAuthTokenAsync(It.IsAny<string>());

            //Assert
            mockedSharedPreference.Verify(mock => mock.SavePreference("bearer_token", _bearerToken), Times.Once());
        }

        [Fact]
        public void brearerTokenIsAlreadySavedAsSharedPreference_saveBearerToken_tokenNotSavedAgain()
        {
            //Arrange
            var mockedSharedPreference = new Mock<ISharedPreferenceHandler>();
            var mockedAuthenticationService = new Mock<IAuthenticationService>();
            var mockedTempAuthTokenPoller = new Mock<ITempAuthTokenPoller>();

            mockedSharedPreference.Setup(pref => pref.GetPreference(_brearerTokenKeyName)).Returns(_bearerToken);

            var sut = new AuthenticationController(mockedAuthenticationService.Object, mockedTempAuthTokenPoller.Object, mockedSharedPreference.Object);

            //Act
            sut.SaveBearerAuthTokenAsync(It.IsAny<string>());

            //Assert
            mockedSharedPreference.Verify(mock => mock.SavePreference("bearer_token", _bearerToken), Times.Never);
        }
    }
}
