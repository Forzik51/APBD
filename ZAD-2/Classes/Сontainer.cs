using System.ComponentModel;

namespace APBD_ZAD_2;

public abstract class Сontainer(string сontainerType,double massLoad,double height,double massOwn,double depth,double maxLoad) : IContainer
{
    private static int _nextId = 0;
    
    public string Id { get; set; } = "KON-"+сontainerType+"-"+_nextId++;
    public double MassLoad { get; set; } = massLoad;
    public double Height { get; set; } = height;
    public double MassOwn { get; set; } = massOwn;
    public double Depth { get; set; } = depth;
    public double MaxLoad { get; set; } = maxLoad;

    public string GetId()
    {
        return Id;
    }

    public virtual void EmptyingCargo() {}

    public virtual void LoadingContainer(double loadmass) {}
    
    public override string ToString()
    {
        return $"[{GetType().Name}] -- ID: {Id}, Mass Load: {MassLoad}, Height: {Height}, Max Load: {MaxLoad}";
    }

    
}