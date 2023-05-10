
public class DevTeamRepository

{
    public DevTeamRepository()
    {
        Seed();
    }
    private DeveloperRepository _dRepo = new DeveloperRepository();
    private List<DeveloperTeam> _DevTeamDb = new List<DeveloperTeam>();
    private int _count = 0;
    private object teamInDb;
    private object newDevTeamData;


    // C.R.U.D

    //Create
    public bool AddDeveloperTeam(DeveloperTeam devTeam)
    {
        if (devTeam is null)
        {
            return false;
        }
        else
        {
            _count++;
            devTeam.ID = _count;
            _DevTeamDb.Add(devTeam);
            return true;
        }
    }
    // R -> Read All
    public List<DeveloperTeam> GetDeveloperTeams()
    {
        return _DevTeamDb;

    }
    // R -> Read by Id
    public DeveloperTeam GetDeveloperTeam(int id)
    {
        return _DevTeamDb.FirstOrDefault(w => w.ID == id)!;
    }

    // U -> Update

    public bool UpdateDeveloperTeam(int id, DeveloperTeam newDevTeamData)
    {
       DeveloperTeam oldDevTeamData = GetDeveloperTeam(id);
        if (oldDevTeamData != null)
        {
            oldDevTeamData.TeamName = newDevTeamData.TeamName;
            oldDevTeamData.Developers = newDevTeamData.Developers;
           

            return true;
        }
        return false;
    }

    // delete 
    public bool DeleteDeveloperTeam(int id)
    {
        DeveloperTeam oldDevTeamData = GetDeveloperTeam(id);
        if (oldDevTeamData != null)
        {
            _DevTeamDb.Remove(oldDevTeamData);
            return true;
        }
        return false;
    }

    public bool AddMultiplesDevelopers(int devTeamId, List<Developer> developersToAdd)
    {
        DeveloperTeam teamInDb = GetDeveloperTeam(devTeamId);
        if(teamInDb != null)
        {
            teamInDb.Developers.AddRange(developersToAdd);
            return true;
        }
       return false;
    }

    // seed 
    public void Seed()
    {
        DeveloperTeam Js = new DeveloperTeam()
        {
            TeamName = "Java Script Devs"
        };
        Js.Developers.Add(_dRepo.GetDeveloperByID(3));

        DeveloperTeam cSharp = new DeveloperTeam()
        {
            TeamName = "C# Sharp Devs"
        };
        cSharp.Developers.Add(_dRepo.GetDeveloperByID(1));
        cSharp.Developers.Add(_dRepo.GetDeveloperByID(2));

        DeveloperTeam java = new DeveloperTeam()
        {
            TeamName = "Java Devs"
        };

         AddDeveloperTeam(Js);
        AddDeveloperTeam(cSharp);
        AddDeveloperTeam(java);

    }

   

    

   
}