using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (IsFirstNameValid(firstName) || IsLastNameValid(lastName))
            {
                return false;
            }

            if (IsEmailValid(email))
            {
                return false;
            }

            var age = CalculateAgeUsingBirthday(dateOfBirth);

            if (UserAgeLessThenAllowed(age))
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (UserHasCreditLimitLessThanFiveHundred(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private static bool UserAgeLessThenAllowed(int age)
        {
            return age < 21;
        }

        private static bool UserHasCreditLimitLessThanFiveHundred(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }

        private static int CalculateAgeUsingBirthday(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            bool ageMonthBirthdayNotYetArrived = now.Month < dateOfBirth.Month;
            bool userBirthdayNotYetArrived = now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day;
            if (ageMonthBirthdayNotYetArrived || userBirthdayNotYetArrived) age--;
            return age;
        }

        private static bool IsEmailValid(string email)
        {
            return !email.Contains("@") && !email.Contains(".");
        }

        private static bool IsLastNameValid(string lastName)
        {
            return string.IsNullOrEmpty(lastName);
        }

        private static bool IsFirstNameValid(string firstName)
        {
            return string.IsNullOrEmpty(firstName);
        }
    }
}
