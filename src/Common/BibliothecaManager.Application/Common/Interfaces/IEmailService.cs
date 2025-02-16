using System.Threading.Tasks;
using BibliothecaManager.Application.Common.Models;

namespace BibliothecaManager.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendAsync(EmailRequest request);
}
