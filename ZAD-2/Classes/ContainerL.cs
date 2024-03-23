namespace APBD_ZAD_2;

public class ContainerL(string сontainerType,double massLoad,double height,double massOwn,double depth,double maxLoad, bool dangerousCargo) 
    : Сontainer(сontainerType, massLoad, height, massOwn, depth, maxLoad), IHazardNotifier
{
    private bool IfCargoDangerous { get; set; } = dangerousCargo;
    public void HazardNotice()
    {
        Console.WriteLine("Hazard notice " + Id);
        
    }

    public override void LoadingContainer(double loadMass)
    {
        
        if (IfCargoDangerous == true && loadMass < MaxLoad / 2)
        {
            MassLoad = loadMass;
        }
        else if (IfCargoDangerous == false && loadMass < MaxLoad / 10 * 9)
        {
            MassLoad = loadMass;
        }
        else
        {
            HazardNotice();
        }
    }
    
    public override void EmptyingCargo()
    {
        MassLoad = 0;
        throw new NotImplementedException();
    }
    
}