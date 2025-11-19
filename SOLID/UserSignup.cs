using System;

namespace NotifyExample
{
    public interface INotificationDetails
    {
    }
    public class EmailNotification : INotificationDetails
    {
        public User user;
        public string emailTemplate;
        public string emailBody;
    }
    public class TwilioNotification : INotificationDetails
    {
        public User user;
        public string otp;
    }

    public interface INotificationService
    {
        public void SendNotification();
    }
    public class SmtpMailer : INotificationService
    {
        private readonly EmailNotification _emailNotificationDetails;

        public SmtpMailer(EmailNotification emailNotificationDetails)
        {
            _emailNotificationDetails = emailNotificationDetails;
        }

        public void SendNotification()
        {
            Console.WriteLine($"[SMTP] template={_emailNotificationDetails.emailTemplate} to={_emailNotificationDetails.user.Email} body={_emailNotificationDetails.emailBody}");
        }
    }
    public class TwilioClient : INotificationService
    {
        private readonly TwilioNotification _twilioNotificationDetails;

        public TwilioClient(TwilioNotification twilioNotificationDetails)
        {
            _twilioNotificationDetails = twilioNotificationDetails;
        }

        public void SendNotification()
        {
            Console.WriteLine($"[Twilio] OTP {_twilioNotificationDetails.otp} -> {_twilioNotificationDetails.user.Phone}");
        }
    }

    public class User
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public interface ISignupService
    {
        bool SignUp();
    }

    public class SignUpService : ISignupService
    {
        private User _user;
        public SignUpService(User u)
        {
            _user = u;
        }

        public bool SignUp()
        {
            if (string.IsNullOrEmpty(_user.Email))
                return false;
            // pretend DB save here…
            // hard-coded providers
            return true;
        }
    }

    public interface INotificationDispatcher
    {
        void SendNotification(Dictionary<string, INotificationService> registry);
    }
    public class NotificationDispatcher : INotificationDispatcher
    {
        public void SendNotification(Dictionary<string, INotificationService> registry)
        {
            foreach (var kvp in registry)
            {
                INotificationService notificationService = kvp.Value;
                notificationService.SendNotification();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var user = new User { Email = "user@example.com", Phone = "+15550001111" }
            var svc = new SignUpService(user);
            var isSucces = svc.SignUp();

            if (isSucces)
            {
                var registry = new Dictionary<string, INotificationService>
                {
                    { "smtp", new SmtpMailer(new EmailNotification { user = user, emailTemplate = "welcome", emailBody = "Welcome!" }) },
                    { "twilio", new TwilioClient(new TwilioNotification { user = user, otp = "123456" }) }
                };

                new NotificationDispatcher().SendNotification(registry);
            }
        }
    }
}
