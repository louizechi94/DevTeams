
using System.Linq;

public class ProgramUI
{

    // Globally scoped variable container with the DeveloperRepository Data
    private DeveloperRepository _dRepo = new DeveloperRepository();
    private DevTeamRepository _dtRepo = new DevTeamRepository();



    bool isRunning = true;
    private object userInput;
    private object dev;
    private object intuserInputDevId;
    private IEnumerable<DeveloperTeam> dTeams;
    private DeveloperTeam DeveloperTeam;
    private object team;

    public ProgramUI()
    {


    }
    public void Run()
    {
        RunApplication();
    }

    private void RunApplication()
    {

        while (isRunning)
        {
            Console.Clear();
            System.Console.WriteLine("Welcome To DevTeam \n" +
                                     "1. Show All Developers\n" +
                                     "2. Show Developer By ID\n" +
                                     "3. Add Developer\n" +
                                     "4. Update Developer\n" +
                                     "5. Delete Developer\n" +
                                     "6. Show All DevTeams\n" +
                                     "7. Show DevTeam By ID\n" +
                                     "8. Add DevTeam\n" +
                                     "9. Update DevTeam\n" +
                                     "10. Delete DevTeam\n" +
                                     "11. Developers with Pluralsight\n" +
                                     "12. Add multiple Developers to a Team\n" +
                                     "00. Close Application");
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    ShowAllDevelopers();
                    break;
                case "2":
                    ShowDeveloperByID();
                    break;
                case "3":
                    AddDeveloper();
                    break;
                case "4":
                    UpdateDeveloper();
                    break;
                case "5":
                    DeleteDeveloper();
                    break;
                case "6":
                    ShowAllDevTeams();
                    break;
                case "7":
                    ShowDevTeamByID();
                    break;
                case "8":
                    AddDevTeam();
                    break;
                case "9":
                    UpdateDevTeam();
                    break;
                case "10":
                    DeleteDevTeam();
                    break;
                case "11":
                    DeveloperswithPluralsight();
                    break;
                case "12":
                    AddmultipleDeveloperstoaTeam();
                    break;

                case "00":
                    isRunning = Quit();
                    break;
                default:
                    System.Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }

    private void AddmultipleDeveloperstoaTeam()
    {
       try
       {
        Console.Clear();
        System.Console.WriteLine("==Developer Team Listing==");
        GetDevTeamData();
        List<DeveloperTeam> dTeam = _dtRepo.GetDeveloperTeams();

        if (dTeam.Count() > 0)
        {
            System.Console.WriteLine("Select a Dev Team bu Id");
            int userInputDevTeamId = int.Parse(Console.ReadLine()!);
            DeveloperTeam team = _dtRepo.GetDeveloperTeam(userInputDevTeamId);

            List<Developer> auxDevsInDb = _dRepo.GetDevelopers();
            List<Developer> devsToAdd = new List<Developer>();
            
            if (team != null)
            {
                bool hasFilledPositions = false;

                while (!hasFilledPositions)
                {
                    if(auxDevsInDb.Count() > 0)
                    {
                        DisplayDevelopersInDd(auxDevsInDb);
                        Console.WriteLine("Do you want to add a developer? y/n");
                        string userInputAnyDev = Console.ReadLine()!.ToLower()!;

                        if (userInputAnyDev == "y")
                        {
                            System.Console.WriteLine("Input developer Id");
                            int userInputDevId = int.Parse(Console.ReadLine()!);
                            Developer dev = _dRepo.GetDeveloperByID(userInputDevId);
                            
                            if ( dev != null)
                            {
                                devsToAdd.Add(dev);
                                auxDevsInDb.Remove(dev);

                            }
                            else
                            {
                                System.Console.WriteLine("The Developer Doesn't Exist");
                                PressAnyKey();
                            }
                        }
                        else
                        {
                            hasFilledPositions = true;
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("There are no Developers in the Database");
                        PressAnyKey();
                        break;
                    }
                }
                if(_dtRepo.AddMultiplesDevelopers(team.ID,devsToAdd))
                {
                    System.Console.WriteLine("success");
                }
                else
                {
                    System.Console.WriteLine("fail!");
                }
            }
            else
            {
                System.Console.WriteLine("Sorry invalid DevTeamId");
            }
        }
        PressAnyKey();
       }
       catch (Exception ex)
       {
        System.Console.WriteLine(ex.Message);
        SomethingWentWrong();
       }
    }



    private void UpdateDevTeam()
    {
        Console.Clear();
        System.Console.WriteLine("==Developer Team Listing ==");
        GetDevTeamData();
        List<DeveloperTeam> dTeam = _dtRepo.GetDeveloperTeams();
        if (dTeam.Count() > 0)
        {
            System.Console.WriteLine("Please select a DevTeamId for Update.");
            int userInputDevTeamId = int.Parse(Console.ReadLine()!);
            DeveloperTeam team = _dtRepo.GetDeveloperTeam(userInputDevTeamId);
            if (team != null)
            {
                DeveloperTeam updatedTeamData = InitializeDTeamCreation();
                if
                (_dtRepo.UpdateDeveloperTeam(team.ID, updatedTeamData))
                {
                    System.Console.WriteLine("success");
                }
                else
                {
                    System.Console.WriteLine("Fail");
                }
            }
            else
            {
                System.Console.WriteLine("Sorry you used an invalid id");
            }
        }
        PressAnyKey();
    }










    private void DeveloperswithPluralsight()
    {
        List<Developer> devsWops = _dRepo.GetDeveloperWithOutPluralsight();
        if(devsWops.Count() > 0)
        {
            foreach (Developer dev in devsWops )
            {
                DisplayDevData(dev);
            }
        }
        else
        {
            System.Console.WriteLine("Every Developer has Pluralsight!");
        }
        PressAnyKey();
    }

   

    private void DeleteDevTeam()
    {
        try
        {
            Console.Clear();
            System.Console.WriteLine("==Developer Team Listing ==");
            GetDevTeamData();
            List<DeveloperTeam> dTeam = _dtRepo.GetDeveloperTeams();
            
            if (dTeam.Count () > 0)
            {
                System.Console.WriteLine("Please select a DeveloperTeam by Id dor deletion");
                int userInputDevTeamId =int.Parse(Console.ReadLine()!);
                DeveloperTeam team = _dtRepo.GetDeveloperTeam(userInputDevTeamId);

                if (team != null)
                {
                    if (_dtRepo.DeleteDeveloperTeam(team.ID))
                    {
                        System.Console.WriteLine("Success");
                    }
                    else
                    {
                        System.Console.WriteLine("Fail");
                    }
                }
                else
                {
                    System.Console.WriteLine("There aren't any DevTeams to Delete");
                }
            }
            else
            {
                System.Console.WriteLine("There aren't any available developer Teams.");
            }
            PressAnyKey();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
    }

    private void AddDevTeam()
    {
        Console.Clear();
        DeveloperTeam dTeam = InitializeDTeamCreation();
        if(_dtRepo.AddDeveloperTeam(dTeam))
        {
         System.Console.WriteLine("success");   
        }
        else
        {
            System.Console.WriteLine("Fail");
        }
        PressAnyKey();
    }

    private DeveloperTeam InitializeDTeamCreation()
    {
        try
        {
            DeveloperTeam team = new DeveloperTeam();

            System.Console.WriteLine("Please enter the Team Name.");
            team.TeamName = Console.ReadLine()!;

            bool hasFilledPositions = false;

            List<Developer> auxDevelopers =  _dRepo.GetDevelopers();
            
            while(hasFilledPositions == false)
            {
                System.Console.WriteLine("Does This Team Have any Developers y/n ?");
                string userInputAnyDevs = Console.ReadLine()!.ToLower();

                if (userInputAnyDevs == "y")
                {
                    if (auxDevelopers.Count() > 0)
                    {
                        DisplayDevelopersInDd(auxDevelopers);
                        System.Console.WriteLine("Select a Developer by Id");
                        int userInputDevId = int.Parse(Console.ReadLine()!);

                        Developer selectedDeveloper = _dRepo.GetDeveloperByID(userInputDevId);

                        if (selectedDeveloper != null)
                        {
                            team.Developers.Add(selectedDeveloper);
                            auxDevelopers.Remove(selectedDeveloper);

                        }
                        else
                        {
                            System.Console.WriteLine("Sorry the Developer Doesn't Exist!");

                        }

                    }
                    else
                    {
                        System.Console.WriteLine("There are no Developers in the Database.");
                        PressAnyKey();
                        break;


                    }
                
                }
                else
                {
                    hasFilledPositions = true;
                }
            }
            return team;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            SomethingWentWrong();
        }
        return null;
        
    }

    private void DisplayDevelopersInDd(List<Developer> auxDevelopers)
    {
       foreach (var dev in auxDevelopers )
       {
         System.Console.WriteLine($"{dev.ID}. {dev.FullName}\n");
       }
    }

    private void ShowDevTeamByID()
    {
       Console.Clear();
       System.Console.WriteLine("== Developer Team Listing ==");
       GetDevTeamData();
       List<DeveloperTeam> devTeam = _dtRepo.GetDeveloperTeams();
       if(devTeam.Count() > 0)
       {
         System.Console.WriteLine("select DevTeam By Id");
         int userInputDevTeamId =int.Parse(Console.ReadLine()!);
         validateDevTeamData(userInputDevTeamId);
       }
       PressAnyKey();

    }

    private void validateDevTeamData(int userInputDevTeamId)
    {
        DeveloperTeam dTeams = _dtRepo.GetDeveloperTeam(userInputDevTeamId);
        if(dTeams != null)
        {
            DisplayDeveloperTeamData(dTeams);
        }
        else
        {
            System.Console.WriteLine("Sorry The team Doesn't Exist!");
        }
    }

    private void DisplayDeveloperTeamData(DeveloperTeam team)
    {
      System.Console.WriteLine(team);
    }

    private void ShowAllDevTeams()
    {
        Console.Clear();
        System.Console.WriteLine("==Developwe Team Listing ==");
        GetDevTeamData();
        PressAnyKey();

        
    }
    private void GetDevTeamData()
    {
        List<DeveloperTeam> dTeam = _dtRepo.GetDeveloperTeams();
        if(dTeam.Count() > 0)
        {
            foreach (DeveloperTeam team in dTeam)
            {
                DisplayDeveloperTeamData(team);
            }
        }
    }

    

  

    private void PressAnyKry()
    {
        throw new NotImplementedException();
    }

   

    

    private void ShowAllDevelopers()
    {
        Console.Clear();
        System.Console.WriteLine($"Developer Listing");
        // method that calls 
        var developers = _dRepo.GetDevelopers();
        foreach (var dev in developers)
        {
            System.Console.WriteLine($"DeveloperID: {dev.ID}\n" +
                                      $"DeveloperName:{dev.FullName}\n"
                                      + $"HasPluralSight:{dev.HasPluralsight}\n");
        }
        Console.ReadKey();
    }

    private void ShowDeveloperByID()
    {
        Console.Clear();
        System.Console.WriteLine($"Developer info");
        System.Console.WriteLine("please enter the developer ID.");
        int userInput = int.Parse(Console.ReadLine());

        // method that calls 
        var developer = _dRepo.GetDeveloperByID(userInput);


        System.Console.WriteLine($"DeveloperID: {developer.ID}\n" +
                                  $"DeveloperName:{developer.FullName}\n"
                                  + $"HasPluralSight:{developer.HasPluralsight}\n");

        Console.ReadKey();
    }


    private void AddDeveloper()
    {
        Console.Clear();

        Developer developerForm = new Developer();

        System.Console.WriteLine($"Please add a Developer First Name:");
        string userInputFirstName = Console.ReadLine()!;
        developerForm.FirstName = userInputFirstName;

        System.Console.WriteLine($"Please add a Developer Last Name:");
        string userInputLastName = Console.ReadLine()!;
        developerForm.LastName = userInputLastName;

        if (_dRepo.AddDeveloper(developerForm))
        {
            System.Console.WriteLine("Succes!");
        }
        else
        {
            System.Console.WriteLine("Fail!");
        }


        // method that calls 
        //var developer = _dRepo.AddDeveloper(userInput);


        Console.ReadKey();
    }





    private void UpdateDeveloper()
    {
        try
        {
            Console.Clear();
            System.Console.WriteLine("Please Enter a Devloper Id:");
            int userInputDevId = int.Parse(Console.ReadLine()!);
            Developer selectedDeveloper = _dRepo.GetDeveloperByID(userInputDevId);

            if (selectedDeveloper != null)
            {

                Developer updatedDevData = new Developer();
                System.Console.WriteLine("Please enter the  Developer First Name:");

                string userInputFirstName = Console.ReadLine()!;
                updatedDevData.FirstName = userInputFirstName;

                System.Console.WriteLine("What is the developers Last Name?");
                string userInputLastName = Console.ReadLine()!;
                updatedDevData.LastName = userInputLastName;

                System.Console.WriteLine("does thiS developer have Pluralsight y/n?");
                string userInputYesorNo = Console.ReadLine()!.ToLower();
                if (userInputYesorNo == "y")
                {
                    updatedDevData.HasPluralsight = true;
                }
                else
                {
                    updatedDevData.HasPluralsight = false;
                }

                if (_dRepo.UpdateDeveloperData(selectedDeveloper.ID, updatedDevData))
                {
                    System.Console.WriteLine("Success!");
                }
                else
                {
                    System.Console.WriteLine("Fail!");
                }
            }
            else
            {
                Console.ReadKey();
            }
        }

        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }

    }


    private void DeleteDeveloper()
    {
        Console.Clear();
        System.Console.WriteLine("============/n");
        try
        {

            System.Console.WriteLine("select Devloper by ID.");
            int userInputDevId = int.Parse(Console.ReadLine()!);
            var isValidated = ValidateDeveloperInDatabase(userInputDevId);
            if (isValidated)
            {
                System.Console.WriteLine("Do you want to delete this Developer? y/n");
                string userInputDeletedev = Console.ReadLine()!.ToLower();
                if (userInputDeletedev == "y")
                {
                    if (_dRepo.DeleteDeveloperData(userInputDevId))
                    {
                        System.Console.WriteLine($"The Developer was Deleted");

                    }
                    else
                    {
                        System.Console.WriteLine($"The Developer was not Deleted");
                    }
                }
                else
                {
                   System.Console.WriteLine($"The Developer was not Deleted");  
                }
            }
            else
            {
                System.Console.WriteLine($"The Developer with the Id: {userInputDevId} dosen't Exist!");

            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            SomethingWentWrong();
        }
        PressAnyKey();

    }

    private void SomethingWentWrong()
    {
        System.Console.WriteLine("Something Went Wrong");
        PressAnyKey();
    
    }

    private bool ValidateDeveloperInDatabase(int userInputDevId)
    {
        Developer dev = GetDeveloperInDatabase(userInputDevId);
        if (dev != null)
        {
            Console.Clear();
            DisplayDevData(dev);
            return true;
        }
        else
        {
            System.Console.WriteLine($"The Developer with the Id: {userInputDevId} dosen't Exist!");

            return false;
        }

    }

    private void DisplayDevData(Developer dev)
    {
        System.Console.WriteLine(dev);
    }

    private Developer GetDeveloperInDatabase(int intuserInputDevId)
    {
        return _dRepo.GetDeveloperByID(intuserInputDevId);
    }

    private void PressAnyKey()
    {
       System.Console.WriteLine("Press Any Key to continue");
       Console.ReadKey();
    }

  

    private bool Quit()
    {
        System.Console.WriteLine("Thank you see you soon.");
        Console.ReadKey();
        return false;
    }



}










