namespace APBD_ZAD_2;

public class ContainerC(string сontainerType,double massLoad,double height,double massOwn,double depth,double maxLoad, string productType, double containerTemperature) 
    : Сontainer(сontainerType, massLoad, height, massOwn, depth, maxLoad)
{
    public string ProductType { get; set; } = productType;
    public double ContainerTemperature { get; set; } = containerTemperature;
   
    public override void EmptyingCargo()
    {
        MassLoad = 0;
        throw new NotImplementedException();
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