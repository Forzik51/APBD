namespace ZAD_5;

public interface IMockDb
{
    public ICollection<Animal> GetAllAnimals();
    public Animal? GetById(int id);
    public void AddAnimal(Animal animal);
    public void Edit(int id, Animal animal);
    public void Delete(int id);
    
    public ICollection<Visit> GetAllVisits();
    public void AddVisits(Visit visit);
}