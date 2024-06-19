using APBD03.Model;

namespace APBD03.Service;

public interface IAnimalsService
{
    IEnumerable<Animal> GetAnimals(string orderBy);
    int CreateAnimal(Animal animal);
    int UpdateAnimal(int idAnimal, Animal animal);
    int DeleteAnimal(int idAnimal);
}