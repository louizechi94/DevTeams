

public class Developer
{
   public Developer()
   {
    
   }
   public Developer(string firstName, string lastName,bool hasPluralsight)
   {
    FirstName = firstName;
    LastName = lastName;
    HasPluralsight = hasPluralsight;
   }



   // We need a primary key
   public int ID { get; set;}
   public string FirstName { get; set; } = string.Empty;
   public string LastName { get; set;} =string.Empty;
   public bool HasPluralsight { get; set; }
   public string FullName 
   {
    get {
        return $" {FirstName} {LastName}";
    }

   }
    

    public override string ToString()
    {
        var str = $"ID: {ID}\n"+
                  $"Full Name: {FullName}" +
                  $"Has Pluralsight Access: {HasPluralsight}\n" +
                  "============================================\n";
        return str;          
        }
    }
