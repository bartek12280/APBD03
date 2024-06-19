using APBD03.Model;

namespace APBD03.Repository;

public interface IAnimalsRepository
{
    IEnumerable<Animal> GetAllAnimals(string orderBy);
    Animal GetAnimalById(int id);
    int AddAnimal(Animal animal);
    int UpdateAnimal(int id, Animal animal);
    int DeleteAnimal(int id);

}