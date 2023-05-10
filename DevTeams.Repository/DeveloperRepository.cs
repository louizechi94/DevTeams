
public class DeveloperRepository
{
    private List<Developer> _developerDb = new List<Developer>();
    private int _count = 0;
    //C.R.U.D

    //Create
    public DeveloperRepository()
    {
        Seed();
    }
    public bool AddDeveloper(Developer developer)
    {
        if (developer is null)
        {
            return false;
        }
        else
        {
            //increment the _count
            _count++;
            //assign the developer to the _count
            developer.ID = _count;
            //save to the database

            _developerDb.Add(developer);

            return true;

        }


    }
    public List<Developer> GetDevelopers()
    {
        return _developerDb;
    }


    public Developer GetDeveloperByID(int id)
    {
        // loop through all the developers inside of the data base (_developersDb)
        // in order to find a single developer based the ID that the user passes in 

        //1. this is the temprory variable type
        //2. this the temporary variable container name : this represents one in many  
        //3. this represent the collection (the developerDataBase)

        //          1          2             3 
        foreach (Developer developer in _developerDb)
        {
            // if we find a developer with the neede id
            if (developer.ID == id)
            {
                //we will return the developer for future use 
                return developer;
            }
            // otherwise we will return "null or nothing"
        }
        return null;

    }
    //Update
    public bool UpdateDeveloperData(int id, Developer newDeveloperData)
    {
        // find a character in the data base
        Developer oldDeveloperData = GetDeveloperByID(id);
        if (oldDeveloperData != null)
        {
            oldDeveloperData.FirstName = newDeveloperData.FirstName;
            oldDeveloperData.LastName = newDeveloperData.LastName;
            oldDeveloperData.HasPluralsight = newDeveloperData.HasPluralsight;
            return true;
        }
        //
        return false;
    }
    // D -> Delete
    public bool DeleteDeveloperData(int id)
    {
        Developer oldDeveloperData = GetDeveloperByID(id);
        if (oldDeveloperData != null)
        {
            _developerDb.Remove(oldDeveloperData);
            return true;
        }
        return false;
    }

    private void Seed()
    {
        Developer Jack = new Developer
        {

            FirstName = "Jack",
            LastName = "Baxen"
        };

        Developer James = new Developer
        {

            FirstName = "James",
            LastName = "Jonston"
        };

        Developer Charlotte = new Developer
        {

            FirstName = "Charlotte",
            LastName = "Clovert"
        };
        Developer Harper = new Developer
        {

            FirstName = "Harper",
            LastName = "Wave"
        };

        //we need to add them to the database: AddPerson(Person person)
        AddDeveloper(Jack);
        AddDeveloper(James);
        AddDeveloper(Charlotte);
        AddDeveloper(Harper);
    }

    public List<Developer> GetDeveloperWithOutPluralsight()
    {
        var devList = new List<Developer>();
        foreach (Developer d in _developerDb)
        {
            if(d.HasPluralsight == false)
            {
                devList.Add(d);
            }
        }
        return devList;
    }
}





