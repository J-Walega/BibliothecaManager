using System;
using BibliothecaManager.Domain.Entities.LibraryEntities;

namespace BibliothecaManager.Application.Dto.LibraryDto;
public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public float Score { get; set; }
    public string FullName { get; set; }
}
