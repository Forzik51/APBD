namespace APBD_ZAD_2;

public class OverfillException : System.Exception
{
    public OverfillException()
    {}
    
    public OverfillException(string message)
        :base(message)
    {}
}