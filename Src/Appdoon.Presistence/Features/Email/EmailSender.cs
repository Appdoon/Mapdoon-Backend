using Appdoon.Application.Services.Users.Command.ForgetPasswordUserService;
using FluentEmail.Core;
using System.Threading.Tasks;

namespace Mapdoon.Presistence.Features.Email
{
	public class EmailSender : IEmailSender
	{
		private readonly IFluentEmail _fluentEmail;

		public EmailSender(IFluentEmail fluentEmail)
		{
			_fluentEmail = fluentEmail;

		}
		public async Task Send(UserEmailOptions userEmailOptions)
		{
			await _fluentEmail.To(userEmailOptions.ToEmail)
							  .Subject(userEmailOptions.Subject)
							  .Body(userEmailOptions.Body)
							  .SendAsync();
		}
	}

	public class EmailSettings
	{
        public string DefaultFromEmail { get; set; }
        public string Host { get; set; }
		public int Port { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
    }
}
