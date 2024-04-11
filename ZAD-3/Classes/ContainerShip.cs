using System.ComponentModel;

namespace APBD_ZAD_2.Classes;

public class ContainerShip(double maxSpeed, int maxContainer, double maxWeight)
{
    public List<Container> ContainerList { get; set; } = [];
    public double MaxSpeed { get; set; } = maxSpeed;
    public int MaxContainer { get; set; } = maxContainer;
    public double MaxWeight { get; set; } = maxWeight;
    private double _totalWeight = 0;

    public void AddContainer(Container container)
    {
        if(ContainerList.Count < MaxContainer)
            ContainerList.Add(container);
    }
    
    public void AddContainer(List<Container> containerList)
    {
        foreach (var item in containerList)
        {
            if(ContainerList.Count < MaxContainer)
                ContainerList.Add(item);
        }
    }

    public void RemoveContainer(string id)
    {
        
    }

    public void PrintList()
    {
        foreach (var item in ContainerList)
        {
            Console.WriteLine(item);
        }
    }
    
    public override string ToString()
    {
        return $"[{GetType().Name}] -- Max Speed: {MaxSpeed}, Max Container: {MaxWeight}";
    }
    
}