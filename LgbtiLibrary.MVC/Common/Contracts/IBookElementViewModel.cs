using System;

namespace LgbtiLibrary.MVC.Common.Contracts
{
    public interface IBookElementViewModel
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}