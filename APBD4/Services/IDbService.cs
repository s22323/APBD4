using APBD4.Models;

namespace APBD4.Services
{
    public interface IDbService
    {
        IEnumerable<Animal> GetAnimals(string orderBy);
        Animal AddAnimal(Animal animal);
        Animal UpdateAnimal(int id, Animal animal);
        void DeleteAnimal(int id);
        bool AnimalExists(int id);

    }
}
