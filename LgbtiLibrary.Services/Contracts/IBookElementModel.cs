using System;

namespace LgbtiLibrary.Services.Contracts
{
    public interface IBookElementModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}