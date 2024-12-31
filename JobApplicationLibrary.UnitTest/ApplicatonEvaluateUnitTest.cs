
using JobApplicationLibrary.Models;
using JobApplicationLibrary.Services;
using Moq;

namespace JobApplicationLibrary.UnitTest
{
    public class ApplicatonEvaluateUnitTest
    {
        //UnitOfWork_Condition_ExpectedBehavior name convention için kullanýlýr.
        [Test]
        public void Application_WithUnderAge_TransferredToAutoRejected()
        {
            //Arrange
            var evaluator = new ApplicationEvaluator(null);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 17 },
            };


            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            Assert.AreEqual(ApplicatonResult.AutoReject, appResult);

        }

        [Test]
        public void Application_WithNoTechStack_TransferredToAutoRejected()
        {
            //Arrange

            var mockValidator = new Mock<IIdentityValidator>();
            //herturlu stringi kabul edecek
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);




            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 19, IdentityNumber = "685" },
                TechStackList = new List<string>() { "" }
            };


            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            Assert.AreEqual(ApplicatonResult.AutoReject, appResult);

        }

        [Test]
        public void Application_WithTechStackover75_TransferredToAutoAccepted()
        {
            //Arrange

            var mockValidator = new Mock<IIdentityValidator>();
            //herturlu stringi kabul edecek
            mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 19 },
                TechStackList = new List<string>() { "C#", "RabbitMQ", "MicroService", "ReactJS" },
                YearsOfExperience = 17,

            };

            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            Assert.AreEqual(ApplicatonResult.AutoAccept, appResult);

        }

        [Test]
        //TC Yoksa HR'a yönlendir gibi..
        public void Application_WithInvalidIdentityNumber_TransferredToHR()
        {
            //Arrange
            //Strict olursa IsValid metodu çaðrýlmazsa hata verir.
            //Loose olursa IsValid metodu çaðrýlmazsa hata vermez.
            //Strictte Interface içerisindeki tüm metodlar testlerde kullanýlmasý gerekmektedir
            //var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Strict);
            var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Loose);
            //herturlu stringi kabul edecek false dönecek
            //mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 19 },
            };

            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            Assert.AreEqual(ApplicatonResult.TransferredToHR, appResult);

        }




        //[Test]
        //public void Application_WithOfficeLocation_TransferredToCTO()
        //{

        //    var mockValidator = new Mock<IIdentityValidator>();


        //    var evaluator = new ApplicationEvaluator(mockValidator.Object);
        //    var form = new JobApplication()
        //    {
        //        Applicant = new Applicant() { Age = 19 },
        //    };

        //    var appResult = evaluator.Evaluate(form);

        //    Assert.AreEqual(ApplicatonResult.TransferredToCTO, appResult);

        //}




        [Test]
        public void Application_WithOver30_ValidationModelToDetailed()
        {
            var mockValidator = new Mock<IIdentityValidator>();
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 31 },
            };
            var appresult = evaluator.Evaluate(form);
            Assert.AreEqual(ValidationStatus.Detailed, mockValidator.Object.ValidationStatus);
        }






    }

}


