using System;

namespace LgbtiLibrary.Data.Models
{
    public interface IBookElement
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}