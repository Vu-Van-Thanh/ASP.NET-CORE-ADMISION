using Admission.Core.ServiceContracts;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

public class EmailService:IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        // Tạo email
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Ban Tuyển Sinh", _configuration["EmailSettings:FromEmail"]));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;

        // Nội dung email
        var builder = new BodyBuilder
        {
            HtmlBody = body,
            TextBody = "Nếu email không hiển thị đúng, vui lòng xem dạng text." 
        };
        email.Body = builder.ToMessageBody();

        // Kết nối SMTP và gửi email
        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync(
                _configuration["EmailSettings:SmtpHost"],
                int.Parse(_configuration["EmailSettings:SmtpPort"]),
                SecureSocketOptions.StartTls // Sử dụng TLS
            );

            await smtp.AuthenticateAsync(
                _configuration["EmailSettings:Username"],
                _configuration["EmailSettings:Password"]
            );

            await smtp.SendAsync(email);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
            throw;
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}
