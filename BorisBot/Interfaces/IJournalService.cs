using System.Reflection.Metadata;
using BorisBot.Models;

namespace BorisBot.Interfaces;

public interface IJournalService
{
    Task<JournalModel[]> GetAll();
    Task Update(Guid id, string newName);
    Task Delete(Guid id); 
    Task Create(string name);
}