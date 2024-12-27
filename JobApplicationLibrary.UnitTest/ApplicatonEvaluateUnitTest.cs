
using JobApplicationLibrary.Models;

namespace JobApplicationLibrary.UnitTest
{
    public class ApplicatonEvaluateUnitTest
    {
        //UnitOfWork_Condition_ExpectedBehavior name convention için kullanýlýr.
        [Test]
        public void Application_WithUnderAge_TransferredToAutoRejected()
        {
            //Arrange
          var evaluator = new ApplicationEvaluator();
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 17 },
            };


            //Action
            var appResult=evaluator.Evaluate(form);

            //Assert
            Assert.AreEqual(ApplicatonResult.AutoReject, appResult);

        }



        [Test]
        public void Application_WithNoTechStack_TransferredToAutoRejected()
        {
            //Arrange
            var evaluator = new ApplicationEvaluator();
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age=19},
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
            var evaluator = new ApplicationEvaluator();
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age=19},
                TechStackList = new List<string>() { "C#", "RabbitMQ", "MicroService", "ReactJS" },
                YearsOfExperience=17,

            };


            //Action
            var appResult = evaluator.Evaluate(form);

            //Assert
            Assert.AreEqual(ApplicatonResult.AutoAccept, appResult);

        }
    }



 




}


