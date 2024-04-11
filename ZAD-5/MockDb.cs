namespace ZAD_5;

public class MockDb : IMockDb
{
    private ICollection<Animal> _animals;
    private ICollection<Visit> _visits;

    public MockDb()
    {
        _animals = new List<Animal>
        {
            new Animal()
            {
                Id = 1,
                Name = "fajek",
                Kategory = "Bober",
                Weight = 5.52,
                Color = "white"
            },
            new Animal()
            {
                Id = 2,
                Name = "marek",
                Kategory = "Kot",
                Weight = 10.2,
                Color = "red"
            }
        };

        _visits = new List<Visit>
        {
            new Visit()
            {
                Id = 1,
                Data = "11.04.2024",
                IdAnimal = 1,
                Opis = "ok",
                Cena = 200
            },
            new Visit()
            {
                Id = 2,
                Data = "18.01.2020",
                IdAnimal = 2,
                Opis = "ok",
                Cena = 250
            },
        };
    }
    
    public ICollection<Animal> GetAllAnimals()
    {
        return _animals;
    }

    public Animal? GetById(int id)
    {
        return _animals.FirstOrDefault(animal => animal.Id == id);
    }

    public void AddAnimal(Animal animal)
    {
        _animals.Add(animal);
    }

    public void Edit(int id, Animal animal)
    {
        if (id == animal.Id)
        {
            Delete(animal.Id);
            AddAnimal(animal);
        }
    }

    public void Delete(int id)
    {
        var animal = GetById(id);
        if (animal != null) _animals.Remove(animal);
    }

    public ICollection<Visit> GetAllVisits()
    {
        return _visits;
    }

    public void AddVisits(Visit visit)
    {
        _visits.Add(visit);
    }
}