namespace APBD_ZAD_2;

public class ContainerG(string сontainerType,double massLoad,double height,double massOwn,double depth,double maxLoad, double atmospherePressure) 
    : Сontainer(сontainerType, massLoad, height, massOwn, depth, maxLoad), IHazardNotifier
{
    public double AtmospherePressure { get; set; } = atmospherePressure;
    public void HazardNotice()
    {
        Console.WriteLine("Hazard notice " + Id);
    }

    public override void EmptyingCargo()
    {
        MassLoad = MassLoad / 100 * 5;
    }

    public override void LoadingContainer(double loadmass)
    {
        if (loadmass < MaxLoad)
        {
            MassLoad = loadmass;
        }
        else
        {
            throw new OverfillException("the mass of the cargo is greater than the capacity of the given container");
        }
        
    }
    
}