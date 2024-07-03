using System.Threading.Tasks;
using BibliothecaManager.Domain.Common;

namespace BibliothecaManager.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
